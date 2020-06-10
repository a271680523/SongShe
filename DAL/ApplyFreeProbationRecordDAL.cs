///////////////////////////////////////////////////////////////////
//CreateTime	2018-2-8 14:49:08
//CreateBy 		唐翔
//Content       申请免费试读课程记录数据处理类
//////////////////////////////////////////////////////////////////

using System;
using System.Linq;
using Model;
using Common;

namespace DAL
{
    /// <summary>
    /// 申请免费试读课程记录数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class ApplyFreeProbationRecordDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取免费试读课程申请记录集合
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public IQueryable<V_ApplyFreeProbationRecordModel> GetList_v(int studentId = 0)
        {
            var data = from a in db.ApplyFreeProbationRecordList
                       join s in db.Student on a.StudentId equals s.ID into t1
                       from st in t1.DefaultIfEmpty()
                       join o in db.Manager on a.OperateId equals o.ID into t2
                       from ot in t2.DefaultIfEmpty()
                       orderby a.Status, a.AddTime
                       select new V_ApplyFreeProbationRecordModel
                       {
                           Id = a.Id,
                           AddTime = a.AddTime,
                           StudentId = a.StudentId,
                           StudentLoginName = st.LoginName,
                           CourseTime = a.CourseTime,
                           LearningNeeds = a.LearningNeeds,
                           Remark = a.Remark,
                           Status = a.Status,
                           OperateId = a.OperateId,
                           OperateName = ot.ManagerName,
                           HandlerRemark = a.HandlerRemark,
                           EditTime = a.EditTime
                       };
            if (studentId > 0)
                data = data.Where(d => d.StudentId.Equals(studentId));
            return data;
        }
        /// <summary>
        /// 获取免费试读课程申请记录视图
        /// </summary>
        /// <param name="id">申请记录ID</param>
        /// <returns></returns>
        public V_ApplyFreeProbationRecordModel GetModel_v(int id)
        {
            return GetList_v().FirstOrDefault(d => d.Id.Equals(id));
        }
        /// <summary>
        /// 获取免费试读课程申请记录实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplyFreeProbationRecordModel GetModel(int id)
        {
            return db.ApplyFreeProbationRecordList.FirstOrDefault(d => d.Id == id);
        }
        /// <summary>
        /// 获取免费试读课程申请记录实体
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public ApplyFreeProbationRecordModel GetModelByStudentId(int studentId)
        {
            return db.ApplyFreeProbationRecordList.OrderByDescending(d => d.AddTime).FirstOrDefault(d => d.StudentId.Equals(studentId));
        }
        /// <summary>
        /// 添加或修改申请记录
        /// </summary>
        /// <param name="id">申请记录ID</param>
        /// <param name="studentId">学生ID</param>
        /// <param name="courseTime">上课时间</param>
        /// <param name="learningNeeds">学习需求</param>
        /// <param name="remark">申请备注</param>
        /// <returns></returns>
        public Tuple<int, string> EditApply(int id, int studentId, string courseTime, string learningNeeds,
            string remark)
        {
            ApplyFreeProbationRecordModel model = new ApplyFreeProbationRecordModel();
            if (id > 0)
            {
                model = GetModel(id);
                if (model == null || model.StudentId != studentId)
                    return new Tuple<int, string>(1, "You application does not exist, please reapply.");//申请记录不存在，请重新申请
                if (model.Status != Keys.HandleStatus.Handling)
                    return new Tuple<int, string>(1, "您的该条申请记录已处理，请查看处理结果");
            }
            if (courseTime.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "Your preferred lesson time.");//请填写您的可上课时间
            if (learningNeeds.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "Your learning goal.");//请填写您的学习需求
            model.StudentId = studentId;
            model.CourseTime = courseTime;
            model.LearningNeeds = learningNeeds;
            model.Remark = remark;
            model.AddTime = DateTime.UtcNow;
            db.Entry(model).State = model.Id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return new Tuple<int, string>(0, "试读课程申请记录保存成功");
        }
        /// <summary>
        /// 处理申请记录
        /// </summary>
        /// <param name="id">申请记录ID</param>
        /// <param name="handlerRemark">处理结果情况</param>
        /// <param name="status">处理状态</param>
        /// <param name="operateId">操作人ID</param>
        /// <returns></returns>
        public Tuple<int, string> HandlerApplyRecord(int id, string handlerRemark,
            Keys.HandleStatus status, int operateId)
        {
            ApplyFreeProbationRecordModel model = GetModel(id);
            if (model == null || model.Status != Keys.HandleStatus.Handling)
                return new Tuple<int, string>(1, "申请记录不存在或已处理，请获取最新数据");
            if (handlerRemark.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请填写处理结果信息");
            if (status == Keys.HandleStatus.Handling)
                return new Tuple<int, string>(1, "请选择正确的处理结果状态");
            model.HandlerRemark = handlerRemark;
            model.Status = status;
            model.OperateId = operateId;
            model.EditTime = DateTime.UtcNow;
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new Tuple<int, string>(0, "申请记录处理成功");
        }
        /// <summary>
        /// 删除未处理的申请记录
        /// </summary>
        /// <param name="id">申请记录ID</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteModel(int id, int studentId)
        {
            ApplyFreeProbationRecordModel model = GetModel(id);
            if (model == null || model.StudentId != studentId)
                return new Tuple<int, string>(0, "申请记录不存在或已删除");
            if (model.Status != Keys.HandleStatus.Handling)
                return new Tuple<int, string>(1, "该条申请记录已处理，仅未处理的申请记录可删除");
            db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
            return new Tuple<int, string>(0, "该条申请记录已删除");
        }
    }
}
