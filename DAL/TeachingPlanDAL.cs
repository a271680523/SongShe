//////////////////////////////////////////////////////////////////
//CreateTime	2018-1-29 16:35:25
//CreateBy 		唐翔
//Content       教案数据处理类
//////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;
using Common;

namespace DAL
{
    /// <summary>
    /// 教案数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class TeachingPlanDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取教案列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<TeachingPlanModel> GetList(int managerId = 0, bool? isDelete = false)
        {
            var data = from tp in db.TeachingPlanList select tp;
            if (managerId > 0)
                data = data.Where(d => d.ManagerId.Equals(managerId));
            if (isDelete != null)
                data = data.Where(d => d.IsDelete == isDelete);
            return data;
        }
        /// <summary>
        /// 获取教案视图列表
        /// </summary>
        /// <param name="managerId">所属管理员ID</param>
        /// <param name="isDelete">是否已删除</param>
        /// <returns></returns>
        public IQueryable<v_TeachingPlanModel> GetList_v(int managerId = 0, bool? isDelete = false)
        {
            var data = from tp in db.TeachingPlanList
                       join b in db.Book on tp.BookId equals b.ID into t1
                       from bt in t1.DefaultIfEmpty()
                       join m in db.Manager on tp.ManagerId equals m.ID into t2
                       from mt in t2.DefaultIfEmpty()
                       orderby tp.AddTime descending
                       select new v_TeachingPlanModel
                       {
                           Id = tp.Id,
                           AddTime = tp.AddTime,
                           BookId = tp.BookId,
                           BookName = bt.Name,
                           CourseCount = tp.CourseCount,
                           Name = tp.Name,
                           Detail = tp.Detail,
                           IsDelete = tp.IsDelete,
                           ManagerId = tp.ManagerId,
                           ManagerName = mt.ManagerName,
                           Number = tp.Number,
                           BeforeId = tp.BeforeId,
                           Version = tp.Version,
                           Chapter = tp.Chapter,
                           TeachingPlanTypeId = tp.TeachingPlanTypeId,
                           TeachingPlanTypeName = db.ExtendType.FirstOrDefault(d => d.Id.Equals(tp.TeachingPlanTypeId)&&d.TypeId==Keys.ExtendTypeId.教案类型).Name,
                           CourseContentList = db.TeachingPlanCourseList.Where(d => d.TeachingPlanId.Equals(tp.Id)).OrderBy(d => d.Number).ToList()
                       };
            if (managerId > 0)
                data = data.Where(d => d.ManagerId.Equals(managerId));
            if (isDelete != null)
                data = data.Where(d => d.IsDelete == isDelete);
            return data;
        }
        /// <summary>
        /// 获取教案课时内容
        /// </summary>
        /// <param name="teachingPlanId">教案ID</param>
        /// <returns></returns>
        public IQueryable<TeachingPlanCourseModel> GetCourseList(int teachingPlanId)
        {
            var data = from c in db.TeachingPlanCourseList
                       where c.TeachingPlanId.Equals(teachingPlanId)
                       select c;
            return data;
        }
        /// <summary>
        /// 获取教案视图信息
        /// </summary>
        /// <param name="number">教案编号</param>
        /// <returns></returns>
        public TeachingPlanModel GetModel(int number)
        {
            return GetList().FirstOrDefault(d => d.Number.Equals(number));
        }
        /// <summary>
        /// 获取教案视图信息
        /// </summary>
        /// <param name="number">教案编号</param>
        /// <returns></returns>
        public v_TeachingPlanModel GetModel_v(int number)
        {
            return GetList_v().FirstOrDefault(d => d.Number.Equals(number));
        }
        /// <summary>
        /// 获取教案视图信息
        /// </summary>
        /// <param name="id">教案ID</param>
        /// <returns></returns>
        public v_TeachingPlanModel GetModelById_v(int id)
        {
            return GetList_v().FirstOrDefault(d => d.Id.Equals(id));
        }


        /// <summary>
        /// 添加或修改教案信息
        /// </summary>
        /// <param name="number">教案编号</param>
        /// <param name="managerId">所属管理员</param>
        /// <param name="name">教案名称</param>
        /// <param name="detail">教案详情</param>
        /// <param name="bookId">教材ID</param>
        /// <param name="chapter">章节</param>
        /// <param name="teachingPlanTypeId">教案课型</param>
        /// <param name="courseContentList">课时内容集合</param>
        /// <returns></returns>
        public Tuple<int, string> EditModel(int number, int managerId, string name, string detail, int bookId,
            string chapter, int teachingPlanTypeId, List<string> courseContentList)
        {
            DateTime nowUtcTime = DateTime.UtcNow;
            TeachingPlanModel model = new TeachingPlanModel();//每次修改实际都是新插入数据
            TeachingPlanModel beforeModel = null;
            if (number > 0)
            {
                beforeModel = GetModel(number);
                if (beforeModel == null || beforeModel.ManagerId != managerId)
                    return new Tuple<int, string>(1, "教案信息不存在");
                beforeModel.IsDelete = true;
                model.Number = beforeModel.Number;
                model.Version = beforeModel.Version + 1;
                model.BeforeId = beforeModel.Id;
            }
            if (managerId <= 0 && (beforeModel == null || beforeModel.ManagerId <= 0))
                return new Tuple<int, string>(1, "管理员信息不存在");
            if (name.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请输入教案名称");
            if (detail.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请输入教案详细内容");
            BookMOD book = new BookDAL().GetModel(bookId);
            if (book == null)
                return new Tuple<int, string>(1, "请选择该教案所使用的教材");
            if (chapter.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请输入该教案的教学内容");
            ExtendTypeModel teachingPlanType = new ExtendTypeDAL().GetModel(teachingPlanTypeId,Keys.ExtendTypeId.教案类型);
            if (teachingPlanType == null || teachingPlanType.IsEnable != true)
                return new Tuple<int, string>(1, "请选择该教案所属的类型");
            var courseList = new List<TeachingPlanCourseModel>();
            foreach (var content in courseContentList)
            {
                if (content.IsNullOrWhiteSpace())
                    return new Tuple<int, string>(1, "第" + (courseList.Count + 1) + "课时内容还未填写，请填写该课时内容");
                var teachingPlanCourse = new TeachingPlanCourseModel
                {
                    AddTime = nowUtcTime,
                    Content = content,
                    ManagerId = managerId,
                    Number = courseList.Count + 1,
                    TeachingPlanId = model.Id
                };
                courseList.Add(teachingPlanCourse);
            }
            if (courseList.Count <= 0)
                return new Tuple<int, string>(1, "每个教案至少有一个课时内容");
            model.AddTime = nowUtcTime;
            model.BookId = bookId;
            model.Chapter = chapter;
            model.CourseCount = courseList.Count;
            model.Detail = detail;
            model.ManagerId = managerId;
            model.Name = name;
            model.TeachingPlanTypeId = teachingPlanTypeId;
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    if (beforeModel != null)
                    {
                        beforeModel.IsDelete = true;
                        db.Entry(beforeModel).State = EntityState.Modified;
                    }
                    if (model.Number <= 0)
                    {
                        model.Number = GetList().Count() + 1;
                        model.Version = 1;
                    }
                    db.Entry(model).State = EntityState.Added;
                    db.SaveChanges();
                    foreach (var course in courseList)
                    {
                        course.TeachingPlanId = model.Id;
                        db.Entry(course).State = EntityState.Added;
                    }
                    db.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    return new Tuple<int, string>(1, "数据保存时发生异常，" + ex.Message);
                }
            }
            return new Tuple<int, string>(0, "教案信息保存成功");
        }
        /// <summary>
        /// 伪删除教案信息
        /// </summary>
        /// <param name="number">教案编号</param>
        /// <param name="managerId">所属管理员ID</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteModelByNumber(int number,int managerId)
        {
            TeachingPlanModel beforeModel = GetModel(number);
            if (beforeModel == null || beforeModel.IsDelete || beforeModel.ManagerId != managerId)
                return new Tuple<int, string>(1, "教案不存在或已删除");
            beforeModel.IsDelete = true;
            db.Entry(beforeModel).State = EntityState.Modified;
            db.SaveChanges();
            return new Tuple<int, string>(0, "教案删除成功");
        }
    }
}
