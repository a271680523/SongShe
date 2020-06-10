///////////////////////////////////////////////////////////////////
//CreateTime	2018-3-8 17:59:35
//CreateBy 		唐翔
//Content       备忘录记录数据处理类
//////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using Model;
using Common;

namespace DAL
{
    /// <summary>
    /// 备忘录记录数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class MemorandumRecordDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;

        /// <summary>
        /// 获取备忘录视图信息列表
        /// </summary>
        /// <param name="memorandumType">备忘录类型 1管理员给学生备注 2课程顾问给学生备注</param>
        /// <param name="studentId">学生ID</param>
        /// <param name="managerId">管理员ID</param>
        /// <returns></returns>
        public IQueryable<v_MemorandumRecordModel> GetList_v(int memorandumType = 0, int studentId = 0, int managerId = 0)
        {
            var data = from mr in db.MemorandumRecordList
                       join s in db.Student on mr.StudentId equals s.ID into t1
                       from st in t1.DefaultIfEmpty()
                       join m in db.Manager on mr.ManagerId equals m.ID into t2
                       from mt in t2.DefaultIfEmpty()
                       orderby mr.AddTime descending
                       select new v_MemorandumRecordModel
                       {
                           Id = mr.Id,
                           AddTime = mr.AddTime,
                           MemorandumType = mr.MemorandumType,
                           StudentId = mr.StudentId,
                           ManagerId = mr.ManagerId,
                           Content = mr.Content,
                           ManagerName = mt.ManagerName,
                           StudentLoginName = st.LoginName
                       };
            if (memorandumType > 0)
                data = data.Where(d => d.MemorandumType.Equals(memorandumType));
            if (studentId > 0)
                data = data.Where(d => d.StudentId.Equals(studentId));
            if (managerId > 0)
                data = data.Where(d => d.ManagerId.Equals(managerId));
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取备忘录实体信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public MemorandumRecordModel GetModel(int id)
        {
            return db.MemorandumRecordList.FirstOrDefault(d => d.Id == id);
        }
        /// <summary>
        /// 添加或修改备忘录信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentId"></param>
        /// <param name="managerId"></param>
        /// <param name="memorandumType"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Tuple<int, string> AeModel(int id, int studentId, int managerId, int memorandumType, string content)
        {
            MemorandumRecordModel model = new MemorandumRecordModel();
            if (id > 0)
            {
                model = GetModel(id);
                if (model == null || model.ManagerId != managerId || model.StudentId != studentId)
                    return new Tuple<int, string>(1, "备忘记录不存在");
            }
            if (content.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请输入备忘信息");
            model.StudentId = studentId;
            model.ManagerId = managerId;
            model.MemorandumType = memorandumType;
            model.Content = content;
            model.AddTime = DateTime.UtcNow;

            db.Entry(model).State = id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return new Tuple<int, string>(0, "备忘记录" + (id > 0 ? "修改" : "添加") + "成功");
        }
        /// <summary>
        /// 删除备忘录信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public Tuple<int, string> DeleteModel(int id, int studentId = 0, int managerId = 0)
        {
            MemorandumRecordModel model = GetModel(id);
            if (model == null || (studentId > 0 && model.StudentId != studentId) || (managerId > 0 && model.ManagerId != managerId))
            {
                return new Tuple<int, string>(1, "备忘记录信息不存在或已删除");
            }
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return new Tuple<int, string>(0, "备忘记录已删除");
        }
    }
}
