///////////////////////////////////////////////////////////////////
//CreateTime	2018-2-8 14:49:08
//CreateBy 		唐翔
//Content       推荐记录数据处理类
//////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using Model;
using Common;

namespace DAL
{
    /// <summary>
    /// 推荐记录数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class RecommendRecordDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取推荐记录集合
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public IQueryable<v_RecommendRecordModel> GetList_v(int studentId = 0)
        {
            var data = from a in db.RecommendRecordList
                       join s in db.Student on a.StudentId equals s.ID into t1
                       from st in t1.DefaultIfEmpty()
                       join o in db.Manager on a.OperateId equals o.ID into t2
                       from ot in t2.DefaultIfEmpty()
                       orderby a.Status, a.AddTime
                       select new v_RecommendRecordModel
                       {
                           Id = a.Id,
                           AddTime = a.AddTime,
                           StudentId = a.StudentId,
                           StudentLoginName = st.LoginName,
                           RecommendName = a.RecommendName,
                           RecommendPhone = a.RecommendPhone,
                           RecommendEmail = a.RecommendEmail,
                           Remark = a.Remark,
                           Status = a.Status,
                           OperateId = a.OperateId,
                           OperateName = ot.ManagerName,
                           HandlerRemark = a.HandlerRemark,
                           HandlerTime = a.HandlerTime
                       };
            if (studentId > 0)
                data = data.Where(d => d.StudentId.Equals(studentId));
            return data;
        }
        /// <summary>
        /// 获取推荐记录视图
        /// </summary>
        /// <param name="id">申请记录ID</param>
        /// <returns></returns>
        public v_RecommendRecordModel GetModel_v(int id)
        {
            return GetList_v().FirstOrDefault(d => d.Id.Equals(id));
        }
        /// <summary>
        /// 获取推荐记录实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RecommendRecordModel GetModel(int id)
        {
            return db.RecommendRecordList.FirstOrDefault(d => d.Id == id);
        }

        /// <summary>
        /// 添加或修改推荐记录
        /// </summary>
        /// <param name="id">申请记录ID</param>
        /// <param name="studentId">学生ID</param>
        /// <param name="recommendEmail"></param>
        /// <param name="remark">申请备注</param>
        /// <param name="recommendName"></param>
        /// <param name="recommendPhone"></param>
        /// <returns></returns>
        public Tuple<int, string> EditRecommendRecord(int id, int studentId, string recommendName, string recommendPhone, string recommendEmail, string remark)
        {
            RecommendRecordModel model = new RecommendRecordModel();
            if (id > 0)
            {
                model = GetModel(id);
                if (model == null || model.StudentId != studentId)
                    return new Tuple<int, string>(1, "推荐记录不存在，请重新推荐");
                if (model.Status != Keys.HandleStatus.Handling)
                    return new Tuple<int, string>(1, "您的该条推荐记录已处理，请查看处理结果");
            }
            if (recommendName.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请填写您推荐的学生姓名");
            if (recommendPhone.IsNullOrWhiteSpace() && recommendEmail.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请填写您推荐的学生联系方式，电话或邮箱二选一");
            model.StudentId = studentId;
            model.RecommendName = recommendName;
            model.RecommendPhone = recommendPhone;
            model.RecommendEmail = recommendEmail;
            model.Remark = remark;
            model.AddTime = DateTime.UtcNow;
            db.Entry(model).State = model.Id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return new Tuple<int, string>(0, "推荐信息保存成功");
        }
        /// <summary>
        /// 处理推荐记录
        /// </summary>
        /// <param name="id">申请记录ID</param>
        /// <param name="handlerRemark">处理结果情况</param>
        /// <param name="status">处理状态</param>
        /// <param name="operateId">操作人ID</param>
        /// <returns></returns>
        public Tuple<int, string> HandlerApplyRecord(int id, string handlerRemark,
            Keys.HandleStatus status, int operateId)
        {
            RecommendRecordModel model = GetModel(id);
            if (model == null || model.Status != Keys.HandleStatus.Handling)
                return new Tuple<int, string>(1, "推荐记录不存在或已处理，请获取最新数据");
            if (handlerRemark.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请填写处理结果信息");
            if (status == Keys.HandleStatus.Handling)
                return new Tuple<int, string>(1, "请选择正确的处理结果状态");
            model.HandlerRemark = handlerRemark;
            model.Status = status;
            model.OperateId = operateId;
            model.HandlerTime = DateTime.UtcNow;
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new Tuple<int, string>(0, "推荐记录处理成功");
        }
        /// <summary>
        /// 删除未处理的推荐记录
        /// </summary>
        /// <param name="id">申请记录ID</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteModel(int id, int studentId)
        {
            RecommendRecordModel model = GetModel(id);
            if (model == null || model.StudentId != studentId)
                return new Tuple<int, string>(0, "推荐记录不存在或已删除");
            if (model.Status != Keys.HandleStatus.Handling)
                return new Tuple<int, string>(1, "该条推荐记录已处理，仅未处理的推荐记录可删除");
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return new Tuple<int, string>(0, "该条推荐记录已删除");
        }
    }
}
