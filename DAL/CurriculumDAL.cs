using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common;
using Model;

namespace DAL
{
    // ReSharper disable once InconsistentNaming
    public class CurriculumDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        #region 管理员课程表
        /// <summary>
        /// 获取管理员课程表集合
        /// </summary>
        /// <param name="courseManagerId"></param>
        /// <returns></returns>
        public IQueryable<v_CurriculumManagerMOD> GetCurriculumManageList(int courseManagerId = 0)
        {
            var data = from cm in db.CurriculumManager
                       join m in db.Manager on cm.ManagerID equals m.ID into t1
                       from mt in t1.DefaultIfEmpty()
                       join tz in db.TimeZone on cm.TimeZoneID equals tz.ID into t2
                       from tzt in t2.DefaultIfEmpty()
                       join em in db.Manager on cm.EditManagerID equals em.ID into t3
                       from emt in t3.DefaultIfEmpty()
                       select new v_CurriculumManagerMOD()
                       {
                           ID = cm.ID,
                           AddTime = cm.AddTime,
                           Curriculum = cm.Curriculum,
                           TimeZoneID = cm.TimeZoneID,
                           TimeZone = tzt,
                           ManagerID = cm.ManagerID,
                           EditManagerID = cm.EditManagerID,
                           EditManagerName = emt.ManagerName,
                           ManagerName = mt.ManagerName,
                           EditTime = cm.EditTime
                       };
            if (courseManagerId > 0)
                data = data.Where(d => d.ManagerID.Equals(courseManagerId));
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取管理员课程表
        /// </summary>
        /// <param name="curriculumManagerId"></param>
        /// <param name="courseManagerId"></param>
        /// <returns></returns>
        public v_CurriculumManagerMOD GetCurriculumManager_v(int curriculumManagerId = 0, int courseManagerId = 0)
        {
            var data = GetCurriculumManageList();
            data = curriculumManagerId > 0 ? data.Where(d => d.ID.Equals(curriculumManagerId)) : data.Where(d => d.ManagerID.Equals(courseManagerId));
            return data.FirstOrDefault();
        }
        /// <summary>
        /// 获取管理员课程表
        /// </summary>
        /// <param name="curriculumManagerId"></param>
        /// <param name="courseManagerId"></param>
        /// <returns></returns>
        public CurriculumManagerMOD GetCurriculumManager(int curriculumManagerId = 0, int courseManagerId = 0)
        {
            return curriculumManagerId > 0 ? db.CurriculumManager.FirstOrDefault(d => d.ID == curriculumManagerId) : db.CurriculumManager.FirstOrDefault(d => d.ManagerID.Equals(courseManagerId));
        }

        /// <summary>
        /// 设置管理员课程表信息
        /// </summary>
        /// <param name="courseManagerId">任课老师ID</param>
        /// <param name="timeZoneId">时区ID</param>
        /// <param name="weekHourseList">课程表集合</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns></returns>
        public Tuple<int, string> AeCurriculumManager(int courseManagerId, int timeZoneId, string weekHourseList, int operateId)
        {
            try
            {
                v_ManagerMOD courseManager = new ManagerDAL().GetMOD_v(courseManagerId);
                if (courseManager == null)
                    return new Tuple<int, string>(1, "当前管理员信息不存在");
                CurriculumManagerMOD curriculumManager = GetCurriculumManager(courseManagerId: courseManagerId) ?? new CurriculumManagerMOD() { ManagerID = courseManagerId, TimeZoneID = courseManager.TimeZone, AddTime = DateTime.UtcNow, EditManagerID = operateId, EditTime = DateTime.UtcNow };
                TimeZoneModel timeZone = new TimeZoneDAL().GetModel(timeZoneId);
                if (timeZone == null)
                    return new Tuple<int, string>(1, "请选择正确的时区信息");
                curriculumManager.TimeZoneID = timeZoneId;
                var curricumList = CurricumModel.GetCurricumListByStrCurricum(weekHourseList);
                curriculumManager.Curriculum = CurricumModel.GetStrCurricumByCurricumList(curricumList, timeZone.TimeZoneInfoId);
                if (curricumList.Count > 0)
                {
                    if (curriculumManager.ID > 0)
                        db.Entry(curriculumManager).State = EntityState.Modified;
                    else
                        db.CurriculumManager.Add(curriculumManager);
                }
                else if (curriculumManager.ID > 0)
                    db.Entry(curriculumManager).State = EntityState.Deleted;
                db.SaveChanges();
                ManagerLogDAL.AddManagerLog("设置管理员课程表", curriculumManager.ToJson(), ManagerLogType.Manager, operateId);
                return new Tuple<int, string>(0, "管理员课程表信息设置成功");
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(1, "设置管理员课程表信息发生错误，" + ex.Message);
            }
        }

        /// <summary>
        /// 根据课程表信息发布加当周四周的课程
        /// </summary>
        /// <param name="courseManagerId">任课老师ID，默认所有老师</param>
        /// <param name="operateManagerId">操作管理员ID</param>
        /// <returns></returns>
        public Tuple<int, string> CreateCourseRecordByCurricuManager(int courseManagerId = 0, int operateManagerId = 0)
        {
            CourseRecordDAL courseDal = new CourseRecordDAL();
            List<CourseRecordMOD> courseRecordList = new List<CourseRecordMOD>();

            //DateTime todayDate = todayTime.Date;
            DateTime nowUtcTime = DateTime.UtcNow;
            DateTime nowUtcTimeToFourWeekUtc = nowUtcTime.AddDays(7 * 4);
            List<v_HolidayRecordModel> holidayRecordList = new HolidayDAL().GetList_v().Where(d => d.StartTime >= nowUtcTime && d.StartTime < nowUtcTimeToFourWeekUtc).ToList();
            int addCourseRecord = 0;
            int courseManagerCount = 0;
            try
            {
                List<v_CurriculumManagerMOD> list = GetCurriculumManageList(courseManagerId).ToList();
                foreach (var vCurriculumManagerMod in list)//循环课程表
                {
                    bool isExistCourse = false;
                    v_ManagerMOD courseManager = new ManagerDAL().GetMOD_v(vCurriculumManagerMod.ManagerID);
                    if (courseManager == null)//当前任课老师信息不存在
                        continue;
                    string timeZoneInfoIdCurriculum = vCurriculumManagerMod.TimeZone?.TimeZoneInfoId ?? courseManager.timeZone.TimeZoneInfoId;//课程表所设置的时区
                    string timeZoneInfoIdCourseManager = courseManager.timeZone.TimeZoneInfoId;//任课老师所处时区
                    DateTime localTimeCurriculum = DateTime.Now.ConvertTime(timeZoneInfoIdCurriculum).Date;//课表时区本地时间凌晨
                    DateTime startTime = localTimeCurriculum.AddDays(-localTimeCurriculum.DayOfWeek.ToString("d").ToInt());//第一周第一天开始时间
                    DateTime startUtcTime = startTime.ConvertUtcTime(timeZoneInfoIdCurriculum);//第一周第一天UTC开始时间
                    DateTime endTime = startTime.AddDays(7 * 4);//第四周最后一天结束时间
                    DateTime endUtcTime = endTime.ConvertUtcTime(timeZoneInfoIdCurriculum);//第四周最后一天UTC结束时间
                    //获取任课老师时间内已发布课程
                    var existCourseRecordList = courseDal.GetCouseRecordList().Where(c => c.StartTime >= startUtcTime && c.StartTime < endUtcTime && c.CourseManagerID.Equals(courseManager.ID)).ToList();
                    //循环每天的时间,0时开始
                    for (DateTime nowTime = startTime; nowTime < endTime; nowTime = nowTime.AddDays(1))
                    {
                        int week = nowTime.DayOfWeek.ToString("d").ToInt();//星期几
                        //if (week == 0)
                        //    week = 7;
                        var curricumMod = vCurriculumManagerMod.CurriculumList.FirstOrDefault(d => d.iWeek.Equals(week));
                        if (curricumMod != null)
                        {
                            foreach (var hours in curricumMod.HourseList) //循环当天的要发布课程的时间点
                            {
                                DateTime courseStartTime = nowTime.AddHours(hours);//课表时区上课时间
                                DateTime courseUtcStartTime = courseStartTime.ConvertUtcTime(timeZoneInfoIdCurriculum);//上课时间的UTC时间
                                //判断是否超过当前时间
                                if (courseUtcStartTime <= nowUtcTime)
                                    continue;
                                //判断是否在假期时间内
                                if (holidayRecordList.Any(d => courseUtcStartTime >= d.StartTime && courseUtcStartTime < d.EndTime))
                                    continue;
                                //判断是否已发布课程
                                if (existCourseRecordList.Any(d => d.StartTime.Equals(courseUtcStartTime)))
                                    continue;
                                CourseRecordMOD courseRecord = new CourseRecordMOD
                                {
                                    StartTime = courseUtcStartTime.ConvertTime(timeZoneInfoIdCourseManager),//任课老师当前的时区的上课时间
                                    CourseManagerID = courseManager.ID
                                };
                                courseRecordList.Add(courseRecord);
                                isExistCourse = true;
                            }
                        }
                    }
                    if (isExistCourse) courseManagerCount++;
                }
            }
            catch (Exception ex)
            {
                ManagerLogDAL.AddManagerLog("课程表发布课程异常", "错误信息：" + ex.Message + (courseManagerId > 0 ? ",任课老师编号:" + courseManagerId : "") + (operateManagerId > 0 ? ",操作人编号:" + operateManagerId : ",系统自动发布课程"),
                    (operateManagerId > 0 ? ManagerLogType.Manager : ManagerLogType.System), operateManagerId);
            }
            //循环发布课程
            foreach (var item in courseRecordList)
            {
                try
                {
                    if (courseDal.AeCourseRecord(item, 0).Item1 == 0)
                        addCourseRecord++;
                }
                catch (Exception ex)
                {
                    ManagerLogDAL.AddManagerLog("课程表发布课程,保存课程信息异常", "错误信息：" + ex.Message + ",任课老师：" + item.CourseManagerID + ",任课时间:" + item.StartTime + (operateManagerId > 0 ? ",操作人编号:" + operateManagerId : ",系统自动发布课程"),
                        (operateManagerId > 0 ? ManagerLogType.Manager : ManagerLogType.System), operateManagerId);
                }
            }
            return new Tuple<int, string>(0, $"本次根据课程表更新课程情况:预计发布课程{courseRecordList.Count}节,实际发布课程{addCourseRecord}节" + (courseManagerId > 0 ? ",发布的课程老师编号：" + courseManagerId : ",共发布了" + courseManagerCount + "位任课老师的课程"));
        }
        #endregion

        #region 学生固定课程表
        /// <summary>
        /// 获取学生固定课程表集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_CurriculumStudentMOD> GetCurriculumStudentList_v(int studentId = 0)
        {
            var data = from cs in db.CurriculumStudent
                       join s in db.Student on cs.StudentID equals s.ID into t1
                       from st in t1.DefaultIfEmpty()
                       join cm in db.Manager on cs.CourseManagerID equals cm.ID into t2
                       from cmt in t2.DefaultIfEmpty()
                       join tz in db.TimeZone on cs.TimeZoneID equals tz.ID into t3
                       from tzt in t3.DefaultIfEmpty()
                       join em in db.Manager on cs.EditManagerID equals em.ID into t4
                       from emt in t4.DefaultIfEmpty()
                       select new v_CurriculumStudentMOD()
                       {
                           ID = cs.ID,
                           AddTime = cs.AddTime,
                           TimeZone = tzt,
                           CourseManagerID = cs.CourseManagerID,
                           EditManagerID = cs.EditManagerID,
                           StudentID = cs.StudentID,
                           TimeZoneID = cs.TimeZoneID,
                           EditTime = cs.EditTime,
                           CourseManagerName = cmt.ManagerName,
                           Curriculum = cs.Curriculum,
                           EditManagerName = emt.ManagerName,
                           StudentLoginName = st.LoginName,
                       };
            if (studentId > 0)
                data = data.Where(d => d.StudentID.Equals(studentId));
            return data.AsQueryable();
        }

        /// <summary>
        /// 获取学生固定课程表信息
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="courseManagerId">课程老师ID</param>
        /// <returns></returns>
        public v_CurriculumStudentMOD GetCurriculumStudentModelByStudentId_v(int studentId, int courseManagerId)
        {
            return GetCurriculumStudentList_v().FirstOrDefault(d => d.StudentID.Equals(studentId) && d.CourseManagerID.Equals(courseManagerId));
        }

        /// <summary>
        /// 获取学生固定课程表视图信息
        /// </summary>
        /// <param name="id">课程表ID</param>
        /// <returns></returns>
        public v_CurriculumStudentMOD GetCurriculumStudentModelById_v(int id)
        {
            return GetCurriculumStudentList_v().FirstOrDefault(d => d.ID.Equals(id));
        }
        /// <summary>
        /// 获取学生固定课程表信息
        /// </summary>
        /// <param name="id">课程表ID</param>
        /// <returns></returns>
        public CurriculumStudentMOD GetCurriculumStudentModelById(int id)
        {
            return db.CurriculumStudent.FirstOrDefault(d => d.ID == id);
        }
        /// <summary>
        /// 获取学生固定课程表信息
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="courseManagerId">课程老师ID</param>
        /// <returns></returns>
        public CurriculumStudentMOD GetCurriculumStudentModelByStudentId(int studentId, int courseManagerId)
        {
            return db.CurriculumStudent.FirstOrDefault(d => d.StudentID.Equals(studentId) && d.CourseManagerID.Equals(courseManagerId));
        }

        /// <summary>
        /// 设置学生固定课程表信息
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="courseManagerId">任课老师ID</param>
        /// <param name="timeZoneId">时区ID</param>
        /// <param name="weekHourseList">课程表集合</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns></returns>
        public Tuple<int, string> AeCurriculumStudent(int studentId, int courseManagerId, int timeZoneId, string weekHourseList, int operateId)
        {
            try
            {
                v_StudentMOD student = new StudentDal().GetMOD_v(studentId);
                if (student == null)
                    return new Tuple<int, string>(1, "This account does not exist.");//当前学生信息不存在
                CurriculumStudentMOD curriculumStudent = GetCurriculumStudentModelByStudentId(studentId, courseManagerId) ?? new CurriculumStudentMOD() { StudentID = studentId, TimeZoneID = student.TimeZone, AddTime = DateTime.UtcNow, EditManagerID = operateId, EditTime = DateTime.UtcNow, CourseManagerID = courseManagerId };
                TimeZoneModel timeZone = new TimeZoneDAL().GetModel(timeZoneId);
                if (timeZone == null)
                    return new Tuple<int, string>(1, "请选择所属时区信息");
                curriculumStudent.TimeZoneID = timeZoneId;
                curriculumStudent.Curriculum = CurricumModel.GetStrCurricumByCurricumList(CurricumModel.GetCurricumListByStrCurricum(weekHourseList), timeZone.TimeZoneInfoId);
                if (!curriculumStudent.Curriculum.IsNullOrWhiteSpace())
                {
                    string[] curriculumStrs = curriculumStudent.Curriculum.Split(',');
                    var data = GetCurriculumStudentList_v().Where(d => d.StudentID.Equals(studentId));
                    foreach (var hours in curriculumStrs)
                    {
                        data = data.Where(d => d.Curriculum.Contains("," + hours + ","));
                    }
                    if (data.Any())
                        return new Tuple<int, string>(1, "固定课程表中包含已有时间点的课程，请重新调整或获取最新课程表信息");
                }
                if (curriculumStudent.Curriculum.IsNullOrWhiteSpace())
                {
                    if (curriculumStudent.ID > 0)
                        db.Entry(curriculumStudent).State = EntityState.Deleted;
                }
                else if (curriculumStudent.ID > 0)
                    db.Entry(curriculumStudent).State = EntityState.Modified;
                else
                    db.CurriculumStudent.Add(curriculumStudent);

                db.SaveChanges();
                ManagerLogDAL.AddManagerLog("设置学生固定课程表", curriculumStudent.ToJson(), ManagerLogType.Manager, operateId);
                return new Tuple<int, string>(0, "学生固定课程表信息设置成功");
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(1, "设置学生固定课程表信息发生错误，" + ex.Message);
            }
        }

        /// <summary>
        /// 删除学生固定课程表
        /// </summary>
        /// <param name="id">学生课程表ID</param>
        /// <param name="studentId">学生ID</param>
        /// <param name="operateId">操作人ID</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteCurricumStudentModel(int id, int studentId, int operateId = 0)
        {
            try
            {
                var model = GetCurriculumStudentModelById(id);
                if (model == null || model.StudentID != studentId)
                    return new Tuple<int, string>(1, "学生固定课程表不存在或已删除");
                db.Entry(model).State = EntityState.Deleted;
                db.SaveChanges();
                return new Tuple<int, string>(0, "删除学生固定课程表信息成功");
            }
            catch (Exception ex)
            {
                ManagerLogDAL.AddManagerLog("删除学生固定课程表异常", "学生课程表ID：" + id + "，操作人ID：" + operateId + "，错误原因：" + ex.Message,
                    operateId > 0 ? ManagerLogType.Manager : ManagerLogType.System, operateId);
                return new Tuple<int, string>(1, "删除学生固定课程表信息错误，错误原因：" + ex.Message);
            }
        }
        /// <summary>
        /// 根据课程表信息发布加当周四周的课程
        /// </summary>
        /// <param name="studentId">学生ID，默认所有学生</param>
        /// <param name="operateId">操作管理员ID</param>
        /// <returns></returns>
        public Tuple<int, string> AddStudentCourseRecordByCurricuStudent(int studentId = 0, int operateId = 0)
        {
            CourseRecordDAL courseDal = new CourseRecordDAL();
            var courseRecordList = new List<CourseRecordMOD>();
            //DateTime nowTime = DateTime.Now;
            //DateTime nowDate = nowTime.Date;
            DateTime nowUtcTime = DateTime.UtcNow;
            DateTime nowUtcTimeToFourWeek = nowUtcTime.AddDays(7 * 4);
            List<v_HolidayRecordModel> holidayRecordList = new HolidayDAL().GetList_v().Where(d => d.StartTime >= nowUtcTime && d.StartTime < nowUtcTimeToFourWeek).ToList();
            int addCourseRecord = 0;
            int courseStudentCount = 0;
            try
            {
                List<v_CurriculumStudentMOD> list = GetCurriculumStudentList_v(studentId).ToList();
                foreach (var vCurriculumStudent in list)//循环课程表
                {
                    bool isExistCourse = false;
                    v_StudentMOD student = new StudentDal().GetMOD_v(vCurriculumStudent.StudentID);
                    if (student == null) //当前学生信息不存在,删除不存在的课程表信息
                    {
                        new CurriculumDAL().DeleteCurricumStudentModel(vCurriculumStudent.ID, vCurriculumStudent.StudentID);
                        continue;
                    }
                    string timeZoneInfoIdCurriculum = vCurriculumStudent.TimeZone?.TimeZoneInfoId ?? student.timeZone.TimeZoneInfoId;//课程表所设置的时区
                    string timeZoneInfoIdStudent = student.timeZone.TimeZoneInfoId;//学生当前所处时区
                    DateTime nowTimeCurricum = nowUtcTime.ConvertTime(timeZoneInfoIdCurriculum);//课程表所处时区当前时间
                    DateTime startTime = nowTimeCurricum.AddDays(-nowTimeCurricum.DayOfWeek.ToString("d").ToInt()).Date;//课程表时区第一周第一天开始时间
                    DateTime startUtcTime = startTime.ConvertUtcTime(timeZoneInfoIdCurriculum);//第一周第一天UTC开始时间
                    DateTime endTime = startTime.AddDays(7 * 4);//第四周最后一天结束时间
                    DateTime endUtcTime = endTime.ConvertUtcTime(timeZoneInfoIdCurriculum);//第四周最后一天UTC结束时间
                    //获取任课老师时间内已发布未选的课程
                    var existCourseRecordList = courseDal.GetCouseRecordList().Where(c => c.StartTime >= startUtcTime && c.StartTime < endUtcTime && c.CourseManagerID.Equals(vCurriculumStudent.CourseManagerID) && c.CourseStatus == 0).ToList();
                    //循环每天的时间,0时开始
                    for (DateTime nowTime = startTime; nowTime < endTime; nowTime = nowTime.AddDays(1))
                    {
                        int week = nowTime.DayOfWeek.ToString("d").ToInt();//星期几
                        var curricumMod = vCurriculumStudent.CurriculumList.FirstOrDefault(d => d.iWeek.Equals(week));
                        if (curricumMod != null)
                        {
                            foreach (var hours in curricumMod.HourseList) //循环当天的要发布课程的时间点
                            {
                                DateTime courseStartTime = nowTime.AddHours(hours);//课程表时区上课时间
                                DateTime courseUtcStartTime = courseStartTime.ConvertUtcTime(timeZoneInfoIdCurriculum);//UTC上课时间
                                //判断是否超过当前时间
                                if (courseUtcStartTime <= nowUtcTime)
                                    continue;
                                //判断是否在假期时间内
                                if (holidayRecordList.Any(d => courseUtcStartTime >= d.StartTime && courseUtcStartTime < d.EndTime))
                                    continue;
                                //获取是否存在未选择的课程
                                var existCourseRecord = existCourseRecordList.FirstOrDefault(d => d.StartTime.Equals(courseUtcStartTime));
                                if (existCourseRecord != null)
                                {
                                    CourseRecordMOD courseRecord = new CourseRecordMOD
                                    {
                                        StartTime = courseUtcStartTime.ConvertTime(timeZoneInfoIdStudent),//学生当前的时区的上课时间
                                        StudentID = student.ID,
                                        ID = existCourseRecord.ID
                                    };
                                    courseRecordList.Add(courseRecord);
                                    isExistCourse = true;
                                }
                            }
                        }
                    }
                    if (isExistCourse) courseStudentCount++;
                }
            }
            catch (Exception ex)
            {
                ManagerLogDAL.AddManagerLog("学生固定课程表预约课程异常", "错误信息：" + ex.Message + (studentId > 0 ? ",学生编号:" + studentId : "") + (operateId > 0 ? ",操作人编号:" + operateId : ",系统自动发布课程"),
                    operateId > 0 ? ManagerLogType.Manager : ManagerLogType.System, operateId);
            }
            var studentCourseRecordDal = new StudentCourseRecordDAL();
            //循环发布课程
            foreach (var item in courseRecordList)
            {
                try
                {
                    if (studentCourseRecordDal.AddStudentCourseRecord(item.StudentID, item.ID).Item1 == 0)
                        addCourseRecord++;
                }
                catch (Exception ex)
                {
                    ManagerLogDAL.AddManagerLog("固定课程表发布课程,保存课程信息异常", "错误信息：" + ex.Message + ",学生编号：" + item.StudentID + ",上课时间:" + item.StartTime + (operateId > 0 ? ",操作人编号:" + operateId : ",系统自动发布课程"),
                        operateId > 0 ? ManagerLogType.Manager : ManagerLogType.System, operateId);
                }
            }
            return new Tuple<int, string>(0, $"本次根据学生固定课程表预约课程情况:预计预约课程{courseRecordList.Count}节,实际成功预约课程{addCourseRecord}节" + (studentId > 0 ? ",预约的学生编号：" + studentId : ",共预约了" + courseStudentCount + "位学生的课程"));
        }
        #endregion
    }
}
