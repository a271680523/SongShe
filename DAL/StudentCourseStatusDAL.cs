using Model;
using System;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// 学生课程状态数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class StudentCourseStatusDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取学生课程状态信息
        /// </summary>
        public StudentCourseStatusMOD GetStudentCourseStatus(int statusId) => db.StudentCourseStatus.FirstOrDefault(d => d.StatusID == statusId);
        /// <summary>
        /// 获取学生课程状态集合
        /// </summary>
        public IQueryable<StudentCourseStatusMOD> GetStudentCourseStatusList()
        {
            var data = from b in db.StudentCourseStatus
                       orderby b.StatusID >= 0 descending, Math.Abs(b.StatusID) ascending
                       select b;
            return data;
        }
        /// <summary>
        /// 修改学生课程状态值参数
        /// </summary>
        /// <param name="model">学生课程状态实体值</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns></returns>
        public Tuple<int, string> EditStudentCourseStatus(StudentCourseStatusMOD model, int operateId)
        {
            StudentCourseStatusMOD studentCourseStatus = GetStudentCourseStatus(model.StatusID);
            if (studentCourseStatus == null)
                return new Tuple<int, string>(1, "学生课程状态值参数不存在");
            if (!studentCourseStatus.IsFixedValue)//非固定值才能修改
            {
                studentCourseStatus.IsEffective = model.IsEffective;
            }
            studentCourseStatus.ChinaName = model.ChinaName;
            studentCourseStatus.EnglishName = model.EnglishName;
            studentCourseStatus.AddTime = DateTime.UtcNow;
            db.Entry(studentCourseStatus).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("修改学生课程状态值参数", "参数ID：" + studentCourseStatus.StatusID + ",中文名:" + studentCourseStatus.ChinaName + ",英文名:" + studentCourseStatus.EnglishName + ",是否计算课时：" + studentCourseStatus.IsEffective, ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "修改学生课程状态值参数成功");
        }
    }
}
