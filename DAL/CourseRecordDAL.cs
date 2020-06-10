using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common;

namespace DAL
{
    // ReSharper disable once InconsistentNaming
    public class CourseRecordDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取课程记录列表视图
        /// </summary>
        /// <param name="courseStatus">课程状态</param>
        /// <returns></returns>
        public IQueryable<v_CourseRecordMOD> GetCouseRecordList(int? courseStatus = null)
        {
            DateTime nowUtcTime = DateTime.UtcNow;
            var data = from cr in db.CourseRecord
                       join m in db.Manager on cr.CourseManagerID equals m.ID into t1
                       from mt in t1
                       join m1 in db.Manager on cr.ManagerID equals m1.ID into t2
                       from m1T in t2.DefaultIfEmpty()
                       join s in db.Student on cr.StudentID equals s.ID into t3
                       from st in t3.DefaultIfEmpty()
                       where (cr.StartTime >= nowUtcTime && cr.CourseStatus == 0) || (cr.CourseStatus > 0)
                       orderby cr.StartTime descending
                       select new v_CourseRecordMOD { ID = cr.ID, CourseManagerID = cr.CourseManagerID, CourseManagerName = mt.ManagerName, StartTime = cr.StartTime, EndTime = cr.EndTime, CourseCount = cr.CourseCount, CourseStatus = cr.CourseStatus, ManagerID = cr.ManagerID, ManagerName = m1T.ManagerName, AddTime = cr.AddTime, StudentID = cr.StudentID, StudentLoginName = st.LoginName };

            if (courseStatus != null)
                data = data.Where(c => c.CourseStatus.Equals((int)courseStatus));
            return data.AsQueryable();
        }
        /// <summary>
        /// 根据学生ID获取发布课程信息
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public v_CourseRecordMOD GetCourseRecordByStudentId(int courseId, int studentId)
        {
            var data = from cr in db.CourseRecord
                       join m in db.Manager on cr.CourseManagerID equals m.ID into t1
                       from mt in t1.DefaultIfEmpty()
                       where cr.ID.Equals(courseId)
                       select new v_CourseRecordMOD()
                       {
                           ID = cr.ID,
                           CourseManagerID = cr.CourseManagerID,
                           StartTime = cr.StartTime,
                           EndTime = cr.EndTime,
                           CourseStatus = cr.CourseStatus,
                           StudentID = cr.StudentID,
                           IsMe = cr.StudentID == studentId,
                           CourseManagerName = mt.ManagerName,
                           IsSupervisor = db.Student.FirstOrDefault(s => s.ID == studentId).Supervisor == cr.CourseManagerID
                       };
            return data.FirstOrDefault();
        }
        /// <summary>
        /// 获取可选课列表
        /// </summary>
        /// <param name="startUtcTime">开始时间</param>
        /// <param name="endUtcTime">结束时间</param>
        /// <param name="studentCourseRecord">学生已选课程</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public IQueryable<v_SelectCourseRecordMOD> GetSelectCourseRecordList(DateTime startUtcTime, DateTime endUtcTime, int studentId, StudentCourseRecordModel studentCourseRecord = null)
        {
            DateTime nowUtcTime = DateTime.UtcNow;
            StudentMOD student = new StudentDal().GetMod(studentId);
            if (student == null)
            {
                return null;
            }
            //判断是否有逾期未付款且超过限制选课时间的
            if (new StudentProductDAL().IsOverdue(studentId))
                return null;
            int changeStudentCourseRecordMinHours = SystemParameDAL.GetSystemParameValue(Keys.SystemParameId.ChangeStudentCourseRecordId);
            //只能选当前学生课程任课老师的最小时间
            DateTime changeStudentCourseRecordMinTime = DateTime.UtcNow.AddHours(changeStudentCourseRecordMinHours);
            int addStudentCourseRecordMinHours = SystemParameDAL.GetSystemParameValue(Keys.SystemParameId.AddStudentCourseRecordId);
            //可以选所有老师的最小时间
            DateTime addStudentCourseRecordMinTime = DateTime.UtcNow.AddHours(addStudentCourseRecordMinHours);

            int courseManagerId = 0;
            if (studentCourseRecord != null && studentCourseRecord.StudentCourseStatus == 0 && studentCourseRecord.StartTime >= DateTime.UtcNow.AddHours(changeStudentCourseRecordMinHours))//判断是否可以更改课程，未上课状态和超过可更换课程最小时间的可以选课
            {
                //如果当前课程超过可选所有老师的时间，则执行新选课流程
                if (studentCourseRecord.StartTime > addStudentCourseRecordMinTime)
                {
                    changeStudentCourseRecordMinTime = addStudentCourseRecordMinTime;
                }
                else//如果当前课程未超过可选所有老师的时间，则从当前课程的开始时间开始增加可选所有老师课程的小时数，该期间只能选择当前老师的课程，超过该时间可以选所有老师的
                {
                    courseManagerId = studentCourseRecord.CourseManagerID;
                    changeStudentCourseRecordMinTime = studentCourseRecord.StartTime;
                    addStudentCourseRecordMinTime = changeStudentCourseRecordMinTime.AddHours(addStudentCourseRecordMinHours);
                }
            }
            //查询背景颜色和字体颜色
            string courseSupervisorBackgroundColor = SystemParameDAL.GetSystemParameStrValue(Keys.SystemParameId.CourseSupervisorBackgroundColorId);
            string courseManagerBackgroundColor = SystemParameDAL.GetSystemParameStrValue(Keys.SystemParameId.CourseManagerBackgroundColorId);
            string courseStudentBackgroundColor = SystemParameDAL.GetSystemParameStrValue(Keys.SystemParameId.CourseStudentBackgroundColorId);
            string courseSupervisorTextColor = SystemParameDAL.GetSystemParameStrValue(Keys.SystemParameId.CourseSupervisorTextColorId);
            string courseManagerTextColor = SystemParameDAL.GetSystemParameStrValue(Keys.SystemParameId.CourseManagerTextColorId);
            string courseStudentTextColor = SystemParameDAL.GetSystemParameStrValue(Keys.SystemParameId.CourseStudentTextColorId);
            string courseSupervisorBorderColor = SystemParameDAL.GetSystemParameStrValue(Keys.SystemParameId.CourseSupervisorBorderColorId);
            string courseManagerBorderColor = SystemParameDAL.GetSystemParameStrValue(Keys.SystemParameId.CourseManagerBorderColorId);
            string courseStudentBorderColor = SystemParameDAL.GetSystemParameStrValue(Keys.SystemParameId.CourseStudentBorderColorId);
            var data = from cr in db.CourseRecord
                       join m in db.Manager on cr.CourseManagerID equals m.ID
                       where cr.StartTime >= startUtcTime && cr.StartTime <= endUtcTime && (cr.CourseStatus == 0 || cr.StudentID == studentId) && (cr.StartTime >= addStudentCourseRecordMinTime || (cr.CourseManagerID.Equals(courseManagerId) && cr.StartTime >= changeStudentCourseRecordMinTime))
                       select new v_SelectCourseRecordMOD
                       {
                           id = cr.ID,
                           CourseManagerID = cr.CourseManagerID,
                           start = cr.StartTime,
                           end = cr.EndTime,
                           title = cr.StudentID == studentId || student.Supervisor == m.ID || (db.CourseRecord.Count(d => d.StartTime == cr.StartTime && d.CourseStatus == 0) <= 1) ? m.ManagerName : "Substitute Teacher",//代课老师
                           CourseStatus = cr.CourseStatus,
                           StudentID = cr.StudentID,
                           IsMe = cr.StudentID == studentId,
                           IsSupervisor = student.Supervisor == m.ID,

                           backgroundColor = cr.StudentID == studentId ? courseStudentBackgroundColor : student.Supervisor == m.ID ? courseSupervisorBackgroundColor : courseManagerBackgroundColor,
                           textColor = cr.StudentID == studentId ? courseStudentTextColor : student.Supervisor == m.ID ? courseSupervisorTextColor : courseManagerTextColor,
                           borderColor = cr.StudentID == studentId ? courseStudentBorderColor : student.Supervisor == m.ID ? courseSupervisorBorderColor : courseManagerBorderColor
                       };
            //取消休学期间的课程展示
            List<StudentSuspendSchoolingRecordMOD> studentSuspendSchoolingList =
                db.StudentSuspendSchoolingRecord.Where(s => s.EndTime >= nowUtcTime && s.StudentID.Equals(studentId)).ToList();
            foreach (var model in studentSuspendSchoolingList)
            {
                data = data.Where(d => d.start < model.StartTime || d.start >= model.EndTime);
            }
            //屏蔽加入黑名单的任课老师
            List<CourseManagerBlackListMOD> courseManagerBlackList = new StudentCourseRecordDAL().GetCourseManagerBlackList(studentId).ToList();
            if (courseManagerBlackList.Count > 0)
            {
                foreach (var item in courseManagerBlackList)
                {
                    data = data.Where(d => d.CourseManagerID != item.CourseManagerId);
                }
            }
            //data = data.Where(d => courseManagerBlackList.Any(c => c.CourseManagerId != d.CourseManagerID));
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取课程发布信息
        /// </summary>
        /// <param name="id">课程发布记录ID</param>
        /// <returns></returns>
        public CourseRecordMOD GetMod(int id)
        {
            var data = db.CourseRecord.FirstOrDefault(d => d.ID == id);
            return data;
        }

        /// <summary>
        /// 添加或修改课程发布记录
        /// </summary>
        /// <param name="model"></param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public Tuple<int, string> AeCourseRecord(CourseRecordMOD model, int operateId)
        {
            CourseRecordMOD courseRecord = new CourseRecordMOD();
            if (model.ID > 0)
            {
                courseRecord = GetMod(model.ID);
                if (courseRecord == null)
                    return new Tuple<int, string>(1, "要修改的课程信息不存在，请获取最新数据");
            }
            if (model.CourseManagerID <= 0)
                return new Tuple<int, string>(1, "请选择该课程的任课老师");
            v_ManagerMOD courseManager = new ManagerDAL().GetMOD_v(model.CourseManagerID);
            if (courseManager == null)
                return new Tuple<int, string>(1, "当前管理员信息不存在");
            if (courseRecord.CourseStatus != 0)
                return new Tuple<int, string>(1, "该课程已被学生选取，不允许被修改");
            var timeZoneInfoId = courseManager.timeZone.TimeZoneInfoId;
            if (model.CourseManagerID != operateId)
                timeZoneInfoId = new ManagerDAL().GetMOD_v(operateId)?.timeZone.TimeZoneInfoId;
            model.StartTime = model.StartTime.ConvertUtcTime(timeZoneInfoId);
            if (db.CourseRecord.Count(cr => cr.StartTime.Equals(model.StartTime) && cr.CourseManagerID.Equals(model.CourseManagerID) && cr.ID != model.ID) > 0)
                return new Tuple<int, string>(1, "该任课老师的当前时间段已添加课程，请更改上课时间");
            courseRecord.CourseManagerID = model.CourseManagerID;
            courseRecord.StartTime = model.StartTime;
            courseRecord.EndTime = model.StartTime.AddMinutes(SystemParameDAL.GetSystemParameValue(Keys.SystemParameId.OneLessMinutes) * 1);
            courseRecord.CourseCount = 1;
            courseRecord.CourseStatus = 0;
            if (courseRecord.ID > 0)
            {
                db.Entry(courseRecord).State = EntityState.Modified;
                db.SaveChanges();
                ManagerLogDAL.AddManagerLog("修改课程信息", courseRecord.ToJson(), ManagerLogType.Manager, operateId);
                return new Tuple<int, string>(0, "修改课程信息成功");
            }
            courseRecord.ManagerID = operateId;
            courseRecord.AddTime = DateTime.UtcNow;
            courseRecord = db.CourseRecord.Add(courseRecord);
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("添加课程信息", courseRecord.ToJson(), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "添加课程信息成功");
        }

        /// <summary>
        /// 删除课程发布记录信息
        /// </summary>
        /// <param name="id">课程发布记录ID</param>
        /// <param name="courseManagerId">课程老师ID</param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public Tuple<int, string> Delete(int id, int courseManagerId, int operateId)
        {
            CourseRecordMOD model = GetMod(id);
            if (model != null)
            {
                if (courseManagerId > 0 && model.CourseManagerID != courseManagerId)
                    return new Tuple<int, string>(1, "权限不足，不允许该操作");
                if (model.CourseStatus != 0)
                    return new Tuple<int, string>(1, "该课程已被学生选取，不允许被删除");
                db.Entry(model).State = EntityState.Deleted;
                db.SaveChanges();
            }
            ManagerLogDAL.AddManagerLog("删除课程信息", model.ToJson(), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "删除课程信息成功");
        }

        /// <summary>
        /// 批量修改课程发布记录
        /// </summary>
        /// <param name="courseManagerId">任课老师</param>
        /// <param name="date">更改的日期</param>
        /// <param name="times">发布课程的小时集合</param>
        /// <param name="managerId">操作员</param>
        /// <returns>处理结果</returns>
        public Tuple<int, string> AeCourseRecordList(int courseManagerId, DateTime date, string times, int managerId)
        {
            v_ManagerMOD courseManager = new ManagerDAL().GetMOD_v(courseManagerId);
            if (courseManager == null)
                return new Tuple<int, string>(1, "当前管理员信息不存在");
            DateTime startTime = date.Date.ConvertUtcTime(courseManager.timeZone.TimeZoneInfoId);
            DateTime endTime = date.Date.AddDays(1).ConvertUtcTime(courseManager.timeZone.TimeZoneInfoId);
            var courseRecordList = db.CourseRecord.Where(c => c.StartTime >= startTime && c.StartTime < endTime && c.CourseManagerID == courseManagerId).ToList();
            List<CourseRecordMOD> list = new List<CourseRecordMOD>();
            if (!times.IsNullOrWhiteSpace())
            {
                foreach (var time in times.Split(','))
                {
                    if (int.TryParse(time, out var hours))
                    {
                        CourseRecordMOD model = new CourseRecordMOD
                        {
                            StartTime = date.Date.AddHours(hours),
                            CourseManagerID = courseManager.ID
                        };
                        var courseRecord = courseRecordList.FirstOrDefault(c => c.StartTime == model.StartTime.ConvertUtcTime(courseManager.timeZone.TimeZoneInfoId) && c.CourseManagerID == courseManagerId);
                        if (courseRecord == null || courseRecord.ID <= 0)
                            list.Add(model);
                        else
                            courseRecordList.Remove(courseRecord);
                    }
                }
            }
            foreach (var item in list)
            {
                AeCourseRecord(item, managerId);
            }
            foreach (var item in courseRecordList)
            {
                Delete(item.ID, 0, managerId);
            }
            return new Tuple<int, string>(0, "课程信息批量处理成功");
        }


    }
}
