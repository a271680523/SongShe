///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-13 11:06:21
//CreateBy 		唐翔
//Content       反馈记录数据处理类
//////////////////////////////////////////////////////////////////
using System;
using System.Data.Entity;
using System.Linq;
using Common;
using Model;

namespace DAL
{
    /// <summary>
    /// 反馈记录数据处理类
    /// </summary>
    public class FeedbackRecordDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取反馈记录视图集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_FeedbackRecordModel> GetList_v()
        {
            var data = from fr in db.FeedbackRecordList
                       join s in db.Student on fr.StudentId equals s.ID into t1
                       from st in t1.DefaultIfEmpty()
                       join m in db.Manager on fr.OperateId equals m.ID into t2
                       from mt in t2.DefaultIfEmpty()
                       orderby fr.AddTime descending
                       select new v_FeedbackRecordModel()
                       {
                           Id = fr.Id,
                           AddTime = fr.AddTime,
                           StudentId = fr.StudentId,
                           Title = fr.Title,
                           Content = fr.Content,
                           FeedbackType = fr.FeedbackType,
                           HandlerRemark = fr.HandlerRemark,
                           Status = fr.Status,
                           HandlerTime = fr.HandlerTime,
                           OperateId = fr.OperateId,
                           OperateManagerName = mt.ManagerName,
                           StudentLoginName = st.LoginName,
                           FeedbackTypeName=db.ExtendType.FirstOrDefault(d=>d.Id.Equals(fr.FeedbackType)).Name
                       };
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取反馈记录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FeedbackRecordModel GetModel(int id)
        {
            return db.FeedbackRecordList.FirstOrDefault(d => d.Id == id);
        }
        /// <summary>
        /// 获取反馈记录信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public v_FeedbackRecordModel GetModel_v(int id)
        {
            return GetList_v().FirstOrDefault(d => d.Id.Equals(id));
        }
        /// <summary>
        /// 添加或修改学生反馈信息
        /// </summary>
        /// <param name="id">反馈记录ID</param>
        /// <param name="studentId">学生ID</param>
        /// <param name="content">反馈内容</param>
        /// <returns></returns>
        public Tuple<int, string> EditModel(int id, int studentId, string content)
        {
            FeedbackRecordModel model = new FeedbackRecordModel();
            if (id > 0)
            {
                model = GetModel(id);
                if (model == null || model.StudentId != studentId)
                    return new Tuple<int, string>(1, "This feedback does not exit or has been deleted。");//反馈记录不存在或已删除
                if (model.Status != 0)
                    return new Tuple<int, string>(1, "该反馈记录已处理，请查询最新的处理结果");
            }
            //if (title.IsNullOrEmpty())
            //    return new Tuple<int, string>(1, "反馈标题不能为空");
            //model.Title = title;
            if (content.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "Please provide some feedback or suggestions before submitting.");//反馈内容不能为空
            model.Content = content;
            //if (feedbackType != 1 && feedbackType != 2)
            //    feedbackType = 0;
            //model.FeedbackType = feedbackType;
            if (id > 0)
            {
                db.Entry(model).State = EntityState.Modified;
            }
            else
            {
                model.AddTime = DateTime.UtcNow;
                model.StudentId = studentId;
                db.Entry(model).State = EntityState.Added;
            }
            db.SaveChanges();
            return new Tuple<int, string>(0, "Thank you for your feedback! We will process it as soon as possible.");//反馈信息已提交保存
        }
        /// <summary>
        /// 删除学生反馈信息
        /// </summary>
        /// <param name="id">反馈记录ID</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteModel(int id, int studentId)
        {
            var model = GetModel(id);
            if (model == null || model.StudentId != studentId)
                return new Tuple<int, string>(1, "反馈记录不存在或已删除");
            if (model.Status != 0)
                return new Tuple<int, string>(1, "该反馈记录已处理");
            db.Entry(model).State = EntityState.Deleted;
            db.SaveChanges();
            return new Tuple<int, string>(0, "反馈信息已删除");
        }

        /// <summary>
        /// 处理反馈信息
        /// </summary>
        /// <param name="id">反馈信息ID</param>
        /// <param name="status">处理状态</param>
        /// <param name="feedbackType">反馈信息类型</param>
        /// <param name="handlerRemark">处理备注</param>
        /// <param name="operateId">操作员</param>
        /// <returns></returns>
        public Tuple<int, string> HandlerFeedbackRecord(int id, Keys.HandleStatus status, int feedbackType, string handlerRemark, int operateId)
        {
            var model = GetModel(id);
            if (model == null)
                return new Tuple<int, string>(1, "反馈记录不存在或已删除");
            //if (status == Keys.HandleStatus.Handling)
            //    return new Tuple<int, string>(1, "请选择处理结果");
            if (feedbackType <= 0)
                return new Tuple<int, string>(1, "请选择反馈类型");
            ExtendTypeModel type = new ExtendTypeDAL().GetModel(feedbackType);
            if (type == null || type.TypeId != Keys.ExtendTypeId.反馈记录类型)
                return new Tuple<int, string>(1, "选择的反馈类型不存在");
            if (handlerRemark.IsNullOrWhiteSpace())
                handlerRemark = "";
            model.HandlerRemark = handlerRemark;
            model.FeedbackType = feedbackType;
            model.Status = status;
            model.HandlerTime = DateTime.UtcNow;
            model.OperateId = operateId;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return new Tuple<int, string>(0, "反馈信息处理成功");
        }
    }
}
