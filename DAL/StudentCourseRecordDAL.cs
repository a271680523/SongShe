//////////////////////////////////////////////////////////////////
//CreateTime	2018-1-29 16:35:25
//CreateBy 		唐翔
//Content       学生预约课程数据处理类
//////////////////////////////////////////////////////////////////
using Model;
using System;
using System.Data.Entity;
using System.Linq;
using Common;
using static Common.Keys;

namespace DAL
{
    /// <summary>
    /// 学生预约课程数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class StudentCourseRecordDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取当前学生选课记录列表
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="isExistCancelCourse">是否包含已取消课程</param>
        /// <returns></returns>
        public IQueryable<v_StudentCourseRecordModel> GetStudentCourseRecordList(int studentId = 0, DateTime? startTime = null, DateTime? endTime = null, bool isExistCancelCourse = false)
        {
            DateTime nowUtcTime = DateTime.UtcNow;
            var data = from scr in db.StudentCourseRecord
                       join cm in db.Manager on scr.CourseManagerID equals cm.ID into t1
                       from cmt in t1.DefaultIfEmpty()
                       join sp in db.StudentProduct on scr.StudentProductID equals sp.ID into t2
                       from spt in t2.DefaultIfEmpty()
                       join s in db.Student on scr.StudentID equals s.ID into t4
                       from st in t4.DefaultIfEmpty()
                       join stz in db.TimeZone on scr.StudentTimeZoneID equals stz.ID into t5
                       from stzt in t5.DefaultIfEmpty()
                       join mtz in db.TimeZone on scr.CourseManagerTimeZoneID equals mtz.ID into t6
                       from mtzt in t6.DefaultIfEmpty()
                       orderby scr.StartTime, scr.StudentCourseStatus == 0 ? 1 : scr.StudentCourseStatus > 0 ? 0 : -1 descending
                       select new v_StudentCourseRecordModel()
                       {
                           ID = scr.ID,
                           CourseManagerID = scr.CourseManagerID,
                           CourseManagerName = cmt.ManagerName,
                           StartTime = scr.StartTime,
                           EndTime = scr.EndTime,
                           ProductName = spt.ProductName,
                           StudentProductID = scr.StudentProductID,
                           CourseCount = scr.CourseCount,
                           CourseSuccessTime = scr.CourseSuccessTime,
                           StudentCourseStatus = scr.StudentCourseStatus,
                           IsCourseSuccessTimeOut = scr.IsCourseSuccessTimeOut,
                           IsSupervisor = st.Supervisor == scr.CourseManagerID,
                           StudentCourseStatusEnglishName = scr.StudentCourseStatusEnglishName,
                           StudentCourseStatusChinaName = scr.StudentCourseStatusChinaName,
                           IsEffective = scr.IsEffective,
                           AddTime = scr.AddTime,
                           CourseRecordID = scr.CourseRecordID,
                           StudentID = scr.StudentID,
                           StudentChinaName = st.ChinaName,
                           StudentLoginName = st.LoginName,
                           BookBuyInfo = scr.BookBuyInfo,
                           ChangeBookCause = scr.ChangeBookCause,
                           CourseContent = scr.CourseContent,
                           CourseEmphasis = scr.CourseEmphasis,
                           CourseManagerFeedback = scr.CourseManagerFeedback,
                           CourseQuestion = scr.CourseQuestion,
                           IsChangeBook = scr.IsChangeBook,
                           NextBookID = scr.NextBookID,
                           NextBookName = scr.NextBookName,
                           NowBookID = scr.NowBookID,
                           NowBookName = scr.NowBookName,
                           StudentFeeback = scr.StudentFeeback,
                           StudentPerformed = scr.StudentPerformed,
                           Task = scr.Task,
                           StudentTimeZoneID = scr.StudentTimeZoneID,
                           StudentTimeZone = stzt ?? db.TimeZone.FirstOrDefault(d => d.ID.Equals(st.TimeZone)),
                           CourseManagerTimeZoneID = scr.CourseManagerTimeZoneID,
                           CourseManagerTimeZone = mtzt,
                           StudentFeedbackTime = scr.StudentFeedbackTime,
                           StudentRateCourseManager = scr.StudentRateCourseManager,
                           StudentRateLesson = scr.StudentRateLesson,
                           SortNumberByProduct = scr.SortNumberByProduct,
                           IsNewStudentCourse = scr.SortNumberByProduct > 0 && scr.SortNumberByProduct <= 3,
                           StudentCourseType = scr.StudentProductID == 0 ? (scr.StartTime > nowUtcTime ? StudentCourseType.FtUseWill : StudentCourseType.FtUsed) : st.Supervisor == scr.CourseManagerID ? (scr.StartTime > nowUtcTime && scr.StudentCourseStatus == 0 ? StudentCourseType.SupervisorUseWill : StudentCourseType.SupervisorUsed) : (scr.StartTime > nowUtcTime ? StudentCourseType.OtherUseWill : StudentCourseType.OtherUsed),
                           CoursePlanContent = scr.CoursePlanContent,
                           TeachingPlanId = scr.TeachingPlanId,
                           TeachingPlanName = scr.TeachingPlanName,
                           TeachingPlanNumber = scr.TeachingPlanNumber,
                           BuyIntention = scr.BuyIntention,
                           LearningAbility = scr.LearningAbility,
                           LearningTarget = scr.LearningTarget,
                           RecommendBook = scr.RecommendBook,
                           StudentVitae = scr.StudentVitae
                       };
            if (studentId > 0)
                data = data.Where(d => d.StudentID.Equals(studentId));
            if (startTime != null)
                data = data.Where(d => d.StartTime >= startTime);
            if (endTime != null)
                data = data.Where(d => d.StartTime <= endTime);
            if (!isExistCancelCourse)
                data = data.Where(d => d.StudentCourseStatus >= 0);
            return data;
        }
        /// <summary>
        /// 根据学生课程记录ID获取实体
        /// </summary>
        /// <param name="studentCourseId"></param>
        /// <returns></returns>
        public v_StudentCourseRecordModel GetStudentCourseRecord_v(int studentCourseId)
        {
            var data = GetStudentCourseRecordList().FirstOrDefault(d => d.ID.Equals(studentCourseId));
            return data;
        }
        /// <summary>
        /// 根据学生课程记录ID获取实体
        /// </summary>
        /// <param name="studentCourseId"></param>
        /// <returns></returns>
        public StudentCourseRecordModel GetStudentCourseRecord(int studentCourseId)
        {
            return db.StudentCourseRecord.FirstOrDefault(d => d.ID == studentCourseId);
        }
        /// <summary>
        /// 更新学生课程在该学生中的顺序编号，试读课不参与排序
        /// </summary>
        /// <param name="id">学生课程ID</param>
        /// <param name="studentId">学生ID</param>
        public void UpdateColumnSortNumberByStudent(int id = 0, int studentId = 0)
        {
            try
            {
                string strWhere = " where StudentProductID>0 and StudentCourseStatus>=0";
                if (id != 0)
                    strWhere += " and ID=" + id;
                if (studentId != 0)
                    strWhere += " and StudentID=" + studentId;
                string strSql =
                    $"update t1 set SortNumberByProduct=case when t2.SortNumberByProduct is null then 0 else t2.SortNumberByProduct end from StudentCourseRecordModel t1 left join (SELECT cast(ROW_NUMBER() OVER(PARTITION BY StudentID ORDER BY StartTime) as int) as SortNumberByProduct,ID,StudentID,StudentProductID,CourseRecordID,StartTime FROM StudentCourseRecordModel {strWhere}) t2 on t1.ID = t2.ID";
                db.Database.ExecuteSqlCommand(strSql);
            }
            catch (Exception ex)
            {
                ManagerLogDAL.AddManagerLog("更新学生课程编号异常",
                    $"学生课程ID：{id}，学生ID：{studentId}，异常原因：{ex.Message}", ManagerLogType.System);
            }
        }

        /// <summary>
        /// 添加学生预约课程记录
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="courseId">课程ID</param>
        /// <param name="studentCourseId"></param>
        /// <param name="isFreeProbation">是否免费试读</param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public Tuple<int, string> AddStudentCourseRecord(int studentId, int courseId, int studentCourseId = 0, bool isFreeProbation = false, int operateId = 0)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                var bllCourseRecord = new CourseRecordDAL();
                var bllStudentProduct = new StudentProductDAL();
                try
                {
                    DateTime nowUtcTime = DateTime.UtcNow;
                    StudentCourseRecordModel beforeStduentCourseRecord = null;
                    if (studentCourseId > 0)
                    {
                        beforeStduentCourseRecord = GetStudentCourseRecord(studentCourseId);
                        if (beforeStduentCourseRecord == null || beforeStduentCourseRecord.ID <= 0 || beforeStduentCourseRecord.StudentID != studentId || beforeStduentCourseRecord.ID != studentCourseId)
                            return new Tuple<int, string>(1, "Please refresh your browser.");//原课程信息不存在，请获取最新数据
                        Tuple<int, string> tupe = DeleteStudentCourseRecord(studentCourseId, studentId);
                        if (tupe.Item1 > 0)
                            return new Tuple<int, string>(1, tupe.Item2);
                    }
                    CourseRecordMOD course = bllCourseRecord.GetMod(courseId);
                    if (course == null)
                        return new Tuple<int, string>(1, "Please refresh your browser.");//课程信息不存在，请获取最新数据
                    if (course.CourseStatus != 0)
                    {
                        if (course.StudentID == studentId)
                            return new Tuple<int, string>(1, "You have already scheduled this lesson.");//您已成功预约该课程，无需再次预约
                        return new Tuple<int, string>(1, "That lesson has already been booked by another student. Please choose a different lesson");//该课程已被其他人预约，请选择其他课程
                    }
                    v_StudentMOD vStudent = new StudentDal().GetMOD_v(studentId);
                    if (vStudent == null)
                        return new Tuple<int, string>(1, "This account does not exist.");//申请预约人信息不存在
                    if (vStudent.timeZone == null)
                        return new Tuple<int, string>(1, "Please set your time zone first.");//申请预约人时区信息异常，请先选择所处时区

                    #region 判断课程是否满足条件
                    //学生更改或取消课程限制时间小时数
                    int changeStudentCourseRecordMinHours = SystemParameDAL.GetSystemParameValue(SystemParameId.ChangeStudentCourseRecordId);
                    //学生选课限制时间小时数
                    int addStudentCourseRecordMinHours = SystemParameDAL.GetSystemParameValue(SystemParameId.AddStudentCourseRecordId);
                    //判断更换课程时不能小于可更换最小时间
                    if (beforeStduentCourseRecord != null && beforeStduentCourseRecord.StartTime <= nowUtcTime.AddHours(changeStudentCourseRecordMinHours))
                        return new Tuple<int, string>(1, $"It is already too late to reschedule this lesson.Please reschedule a lesson at least {changeStudentCourseRecordMinHours} hour in advance.");//原预约课程已超过其可更换时间，不能更换
                    //判断选课课程是否是限制时间以内
                    if (course.StartTime <= nowUtcTime.AddHours(addStudentCourseRecordMinHours))
                    {
                        if (studentCourseId > 0 && course.CourseManagerID != beforeStduentCourseRecord?.CourseManagerID)//更换课程更换原课程时间以后的原课程任课老师的课程
                        {
                            if (course.StartTime <= nowUtcTime.AddHours(changeStudentCourseRecordMinHours))
                                return new Tuple<int, string>(1, $"It is already too late to schedule this lesson.Please schedule a lesson at least {addStudentCourseRecordMinHours} in advance.");//该课程非原预约课程任课老师课程，已超过其可选课时间，请更换其他课程
                        }
                        else
                        {
                            return new Tuple<int, string>(1, "It is already too late to schedule this lesson.Please choose another lesson.");//该课程已超过可选课时间，请更换其他课程
                        }
                    }
                    #endregion
                    StudentProductMOD studentProduct = null;
                    if (beforeStduentCourseRecord != null)
                        studentProduct = bllStudentProduct.GetStudentProduct(beforeStduentCourseRecord.StudentProductID);
                    else
                    {
                        //获取满足时间条件的使用中产品或未使用的产品
                        var studentProductList = db.StudentProduct.Where(d => d.StudentID.Equals(studentId) && (d.ProductStatus == 0 || d.ProductStatus == 1)).OrderByDescending(p => p.ProductStatus).ThenBy(p => p.AddTime).ToList();
                        foreach (var model in studentProductList)
                        {
                            if (model.ProductStatus == 0)//使用中产品，判断开始时间和结束时间是否在上课时间区间
                            {
                                if (model.StartDate <= course.StartTime && model.EndDate > course.StartTime)
                                {
                                    studentProduct = model;
                                    break;
                                }
                            }
                            if (model.ProductStatus == 1)
                            {
                                studentProduct = model;
                                break;
                            }
                        }
                        //studentProduct = _db.StudentProduct.Where(p => p.StudentID.Equals(studentId)).Where(p => (p.ProductStatus == 0 || p.ProductStatus == 1) && p.StartDate <= course.StartTime && p.EndDate >= course.StartTime).OrderByDescending(p => p.ProductStatus).ThenBy(p => p.ID).FirstOrDefault();
                    }
                    var studentProductId = studentProduct?.ID ?? 0;
                    DateTime studentCourseTime = course.StartTime.ConvertTime(vStudent.timeZone.TimeZoneInfoId);//学生所处时区的时间，根据该时间来确定时间周期
                    if (!isFreeProbation)//是否是试读课
                    {
                        DateTime startWeek = studentCourseTime.AddDays(-studentCourseTime.DayOfWeek.ToString("d").ToInt()).Date.ConvertUtcTime(vStudent.timeZone.TimeZoneInfoId);//学生时间的本周周日的0点的UTC时间
                        DateTime endWeek = startWeek.AddDays(7);//学生时间的本周周六24点的UTC时间

                        if (studentProduct == null)
                            return new Tuple<int, string>(1, "No more lessons available.");//无可使用的产品
                        //判断当前产品是否有可用课时
                        if (studentProduct.RestOfCourseCount <= 0)
                            return new Tuple<int, string>(1, "You have already used up all the lessons in your current subscription.");//当前产品无可使用剩余课时
                        if (studentProduct.ProductStatus == (int)StudentProductStatus.Using && studentProduct.EndDate < course.StartTime)//使用中的产品,判断结束时间
                            return new Tuple<int, string>(1, "The lesson you are trying to choose is after the end of your current subscription. (Please choose a lesson within your subscription period.");//当前选择预约的课程已超过当前产品的结束时间
                        if (studentProduct.ProductStatus == (int)StudentProductStatus.NoUsed)
                        {
                            var studentCourseList = db.StudentCourseRecord.Where(d => d.StudentProductID == studentProduct.ID).ToList();
                            studentCourseList.Add(new StudentCourseRecordModel() { StartTime = course.StartTime });
                            var maxTime = studentCourseList.Max(d => d.StartTime);
                            var minTime = studentCourseList.Min(d => d.StartTime);
                            if (maxTime.Subtract(minTime).TotalDays > studentProduct.LimitDate * 7)
                                return new Tuple<int, string>(1, "The lesson you are trying to choose is after the end of your current subscription. (Please choose a lesson within your subscription period.");//当前预约课程上课时间超过了产品的有效选课时间段
                        }
                        //判断当前产品本周已选课程数量
                        int selectedCourseCount = db.StudentCourseRecord.Count(c => c.StartTime >= startWeek && c.StartTime < endWeek && c.StudentProductID == studentProduct.ID && c.StudentID == studentId && c.ID != studentCourseId && c.StudentCourseStatus >= 0);
                        if (studentProduct.Frequency != 0 && studentProduct.Frequency <= selectedCourseCount)
                            return new Tuple<int, string>(1, $"The total number of scheduled lessons in this week has exceeded its limit ({studentProduct.Frequency} lessons/week)");//当前已选课程数量已达到每周选课数量上限
                        //判断任课老师不能连续上课多少节课
                        DateTime startTime = course.StartTime.AddHours(-SystemParameDAL.GetSystemParameValue(SystemParameId.ManagerMaxContinsCourseCountId));
                        DateTime endTime = course.EndTime.AddHours(SystemParameDAL.GetSystemParameValue(SystemParameId.ManagerMaxContinsCourseCountId));
                        var dicDateTime = db.StudentCourseRecord.Where(c => c.StartTime > startTime && c.StartTime <= endTime && c.CourseManagerID.Equals(course.CourseManagerID) && c.ID != studentCourseId && c.StudentCourseStatus >= 0).ToDictionary(c => c.StartTime, c => c.EndTime);
                        dicDateTime.Add(course.StartTime, course.EndTime);
                        if (StaticCommon.GetMaxContins(dicDateTime) > SystemParameDAL.GetSystemParameValue(SystemParameId.ManagerMaxContinsCourseCountId))
                            return new Tuple<int, string>(1, "该任课老师连续任课时间过长，请更换上课时间");
                        //判断是否有产品分期记录，如有则判断是否有逾期未还的记录，限制其选课的课程时间不能超过该记录限制选课时间
                        if (new StudentProductDAL().IsOverdue(studentId, course.StartTime))
                            return new Tuple<int, string>(1, "You are overdue on your latest payment installment. Please make your payment before choosing further lessons.");//该课程上课时间已超过产品分期限制还款时间，还款成功以后可选
                    }

                    //判断课程是否在休学范围内
                    StudentSuspendSchoolingRecordMOD studentSuspendSchooling =
                        db.StudentSuspendSchoolingRecord.FirstOrDefault(s =>
                            s.StartTime < course.StartTime && s.EndTime > course.StartTime && s.StudentID.Equals(studentId) && studentProductId == s.ProductID);
                    if (studentSuspendSchooling != null)
                        return new Tuple<int, string>(1, "当前课程上课时间处于休学状态中，请更换上课时间");

                    //更改课程状态为已选择
                    course.CourseStatus = 1;
                    course.StudentID = studentId;
                    StudentCourseRecordModel studentCourseRecord = new StudentCourseRecordModel
                    {
                        AddTime = nowUtcTime,
                        StudentID = studentId,
                        CourseManagerID = course.CourseManagerID,
                        CourseRecordID = course.ID,
                        StudentProductID = isFreeProbation ? 0 : studentProduct.ID,
                        StartTime = course.StartTime,
                        EndTime = course.EndTime,
                        CourseCount = course.CourseCount,
                        StudentCourseStatus = 0,
                        StudentTimeZoneID = vStudent.TimeZone
                    };
                    //获取课程状态
                    StudentCourseStatusMOD studentCourseStatus = new StudentCourseStatusDAL().GetStudentCourseStatus(studentCourseRecord.StudentCourseStatus);
                    if (studentCourseStatus != null)
                    {
                        studentCourseRecord.StudentCourseStatusChinaName = studentCourseStatus.ChinaName;
                        studentCourseRecord.StudentCourseStatusEnglishName = studentCourseStatus.EnglishName;
                        studentCourseRecord.IsEffective = studentCourseStatus.IsEffective;
                    }
                    else
                    {
                        studentCourseRecord.StudentCourseStatusEnglishName = "Not attending class";
                        studentCourseRecord.StudentCourseStatusChinaName = "未上课";
                    }
                    if (!isFreeProbation)
                    {
                        int studentCourseCount = GetUesStudentCourseCountByProductId(studentProduct.ID);
                        studentProduct.RestOfCourseCount = studentProduct.TotalCourseCount - studentCourseCount - 1;
                        if (studentProduct.ProductStatus == (int)StudentProductStatus.NoUsed)//未使用的产品
                        {
                            var studentCourseByMinStartTime = GetStudentCourseRecordList(studentId).OrderBy(d => d.StartTime).FirstOrDefault(d => d.StudentProductID.Equals(studentProduct.ID));
                            if (studentCourseByMinStartTime != null)
                            {
                                studentProduct.StartDate = studentCourseByMinStartTime.StartTime;
                                studentProduct.EndDate = studentProduct.StartDate.AddDays(studentProduct.LimitDate * 7);
                            }
                        }
                        if (studentProduct.RestOfCourseCount < 0)
                            studentProduct.RestOfCourseCount = 0;
                        db.Entry(studentProduct).State = EntityState.Modified;
                    }
                    else
                    {
                        StudentMOD student = db.Student.FirstOrDefault(d => d.ID.Equals(studentId));
                        if (student != null)
                        {
                            student.IsFreeProbation = true;
                            student.FreeProbationCount += 1;
                            db.Entry(student).State = EntityState.Modified;
                        }
                    }
                    db.Entry(course).State = EntityState.Modified;
                    db.StudentCourseRecord.Add(studentCourseRecord);
                    db.SaveChanges();
                    var timeZoneInfoId = vStudent.timeZone.TimeZoneInfoId;
                    if (operateId > 0)
                    {
                        timeZoneInfoId = new ManagerDAL().GetMOD_v(operateId)?.timeZone.TimeZoneInfoId;
                    }
                    scope.Complete();
                    UpdateColumnSortNumberByStudent(studentId: studentId);//更新学生课程顺序编号
                    ManagerLogDAL.AddManagerLog("学生自助选课", "CourseRecordMOD:" + course.ToJson() + ",StudentProduct:" + studentProduct.ToJson() + ",StudentCourseRecord:" + studentCourseRecord.ToJson(), ManagerLogType.Student, studentId);
                    if (studentCourseId > 0)
                    {
                        //return new Tuple<int, string>(0, "恭喜您，更改预约课程成功，新上课时间：" + course.StartTime.ConvertTime(timeZoneInfoId).ToString("yyyy-MM-dd HH:mm:ss") + ",请勿错过上课时间");
                        return new Tuple<int, string>(0, $"Your request has been processed successfully.Your lesson has been rescheduled for {course.StartTime.ConvertTime(timeZoneInfoId).ToString("yyyy-MM-dd HH:mm:ss")}.");
                    }
                    //return new Tuple<int, string>(0, "恭喜您，预约成功，上课时间：" + course.StartTime.ConvertTime(timeZoneInfoId).ToString("yyyy-MM-dd HH:mm:ss") + ",请勿错过上课时间");
                    return new Tuple<int, string>(0, $"Your request has been processed successfully.Your lesson has been scheduled for {course.StartTime.ConvertTime(timeZoneInfoId).ToString("yyyy-MM-dd HH:mm:ss")}.");
                }
                catch (Exception err)
                {
                    ManagerLogDAL.AddManagerLog("学生自助选课异常", "StudentID:" + studentId + ",CourseID:" + courseId + ",StudentCourseID:" + studentCourseId + ",错误信息：" + err.Message, ManagerLogType.Student, studentId);
                    return new Tuple<int, string>(1, "We are not able to process your request at this moment. Please try again later.");//预约课程处理发生错误，请稍后再试
                }
            }
        }

        /// <summary>
        /// 取消学生预约课程
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Tuple<int, string> CancelStudentCourseRecordByCourseId(int courseId, int studentId)
        {
            var studentCourse = db.StudentCourseRecord.FirstOrDefault(s => s.StudentCourseStatus == 0 && s.CourseRecordID.Equals(courseId));
            if (studentCourse == null || studentCourse.ID <= 0)
                return new Tuple<int, string>(1, "Please refresh your browser.");//当前课程信息无预约信息
            return CancelStudentCourseRecord(studentCourse.ID, studentId);
        }

        /// <summary>
        /// 取消学生预约课程
        /// </summary>
        /// <param name="studentCourseId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Tuple<int, string> CancelStudentCourseRecord(int studentCourseId, int studentId)
        {
            DateTime nowUtcTime = DateTime.UtcNow;
            var studentCourseRecord = db.StudentCourseRecord.FirstOrDefault(d => d.ID == studentCourseId);
            if (studentCourseRecord == null || studentCourseRecord.StudentID != studentId)
                return new Tuple<int, string>(1, "Your request has been processed successfully.");//预约课程取消成功
            //已上课的和超过当前限制时间的预约课程不能取消
            if (studentCourseRecord.StudentCourseStatus != 0 || studentCourseRecord.StartTime <= nowUtcTime.AddHours(SystemParameDAL.GetSystemParameValue(SystemParameId.ChangeStudentCourseRecordId)))
                return new Tuple<int, string>(1, "It is already too late to cancel this lesson. Please always cancel a lesson in advance.");//该课程已超出取消限制
            var courseRecord = new CourseRecordDAL().GetMod(studentCourseRecord.CourseRecordID);
            if (courseRecord == null || courseRecord.ID <= 0 || courseRecord.CourseStatus < 0)
                return new Tuple<int, string>(1, "This session does not exist.");//当前课程信息不存在
            var studentProduct = new StudentProductDAL().GetStudentProduct(studentCourseRecord.StudentProductID);
            courseRecord.CourseStatus = 0;
            courseRecord.StudentID = 0;
            db.Entry(courseRecord).State = EntityState.Modified;
            if (studentProduct != null)
            {
                int studentCourseCount = GetUesStudentCourseCountByProductId(studentProduct.ID);
                studentProduct.RestOfCourseCount = studentProduct.TotalCourseCount - studentCourseCount + 1;
                if (studentProduct.ProductStatus == (int)StudentProductStatus.NoUsed)//未使用的产品
                {
                    var studentCourseByMinStartTime = GetStudentCourseRecordList(studentId).OrderBy(d => d.StartTime).FirstOrDefault(d => d.StudentProductID.Equals(studentProduct.ID) && d.ID != studentCourseId);
                    studentProduct.StartDate = studentCourseByMinStartTime?.StartTime ?? studentProduct.AddTime;
                    studentProduct.EndDate = studentProduct.StartDate.AddDays(studentProduct.LimitDate * 7);
                }
                if (studentProduct.TotalCourseCount < studentProduct.RestOfCourseCount)
                    studentProduct.RestOfCourseCount = studentProduct.TotalCourseCount;
                db.Entry(studentProduct).State = EntityState.Modified;
            }
            else//试读课程没有产品
            {
                StudentMOD student = new StudentDal().GetMod(studentId);
                if (student != null)
                {
                    student.FreeProbationCount -= 1;
                    if (student.FreeProbationCount <= 0)
                    {
                        student.FreeProbationCount = 0;
                        student.IsFreeProbation = false;
                    }
                    db.Entry(student).State = EntityState.Modified;
                }
            }
            //获取取消状态参数
            int studentCourseStatusValue = courseRecord.StartTime > nowUtcTime.AddHours(SystemParameDAL.GetSystemParameValue(SystemParameId.AddStudentCourseRecordId)) ? -1 : -2;
            var studentCourseStatus = new StudentCourseStatusDAL().GetStudentCourseStatus(studentCourseStatusValue) ?? new StudentCourseStatusMOD() { StatusID = studentCourseStatusValue, ChinaName = "学生取消", EnglishName = "Student Cancel" };
            studentCourseRecord.StudentCourseStatus = studentCourseStatusValue;
            studentCourseRecord.StudentCourseStatusChinaName = studentCourseStatus.ChinaName;
            studentCourseRecord.StudentCourseStatusEnglishName = studentCourseStatus.EnglishName;
            db.Entry(studentCourseRecord).State = EntityState.Modified;
            db.SaveChanges();
            UpdateColumnSortNumberByStudent(studentId: studentId);//更新学生课程顺序编号
            return new Tuple<int, string>(0, "Your request has been processed successfully.");//预约课程已成功取消
        }

        /// <summary>
        /// 添加教学计划
        /// </summary>
        /// <param name="id">已预约课程ID</param>
        /// <param name="teachingPlanNumber">教案编号</param>
        /// <param name="nowBookId">教材ID</param>
        /// <param name="coursePlanContent">计划上课内容</param>
        /// <param name="courseManagerId">任课老师ID</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns></returns>
        public Tuple<int, string> AddCoursePlanContent(int id, int teachingPlanNumber, int nowBookId,
            string coursePlanContent, int courseManagerId, int operateId)
        {
            try
            {
                StudentCourseRecordModel model = GetStudentCourseRecord(id);
                if (model == null || (courseManagerId > 0 && model.CourseManagerID != courseManagerId))
                    return new Tuple<int, string>(1, "该课程不存在，请获取最新数据再试");
                if (model.StudentCourseStatus != 0)
                    return new Tuple<int, string>(1, "该课程已超过填写教学计划限制，只有未开课的课程可填写教学计划");
                ManagerModel courseManager = new ManagerDAL().GetMod(model.CourseManagerID);
                if (courseManager == null)
                    return new Tuple<int, string>(1, "任课老师信息不存在");
                TeachingPlanModel teachingPlan = new TeachingPlanDAL().GetModel(teachingPlanNumber);
                if (teachingPlan == null || teachingPlan.ManagerId != model.CourseManagerID)
                    return new Tuple<int, string>(1, "请选择本节课所使用的教案");
                BookMOD book = new BookDAL().GetModel(nowBookId);
                if (book == null)
                    return new Tuple<int, string>(1, "请选择本节课所使用的教材");
                if (coursePlanContent.IsNullOrWhiteSpace())
                    return new Tuple<int, string>(1, "请填写本节课的教学计划");
                model.NowBookID = nowBookId;
                model.NowBookName = book.Name;
                model.TeachingPlanId = teachingPlan.Id;
                model.TeachingPlanName = teachingPlan.Name;
                model.TeachingPlanNumber = teachingPlan.Number;
                model.CoursePlanContent = coursePlanContent;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return new Tuple<int, string>(0, "教学计划保存成功");
            }
            catch (Exception ex)
            {
                ManagerLogDAL.AddManagerLog("添加学生课程教学计划失败", "学生课程记录ID：" + id + ",教案编号：" + teachingPlanNumber + ",教材编号:" + nowBookId + ",计划上课内容：" + coursePlanContent + ",失败原因：" + ex.Message, ManagerLogType.Manager, operateId);
                return new Tuple<int, string>(0, "教学计划保存出现异常，" + ex.Message);
            }
        }

        /// <summary>
        /// 更新学生课程进度信息
        /// </summary>
        /// <param name="studentCourseId">学生预约课程ID</param>
        /// <param name="courseManagerId">任课老师ID</param>
        /// <param name="studentCourseStatus">学生课程完成状态</param>
        /// <param name="isChangeBook">是否更改教材</param>
        /// <param name="nextBookId">下节课教材ID</param>
        /// <param name="changeBookCause">更改教材原因</param>
        /// <param name="learningTarget">学习目标</param>
        /// <param name="learningAbility">学习能力</param>
        /// <param name="studentVitae">学生背景</param>
        /// <param name="recommendBook">推荐教材</param>
        /// <param name="buyIntention">购买意向</param>
        /// <param name="courseContent">课堂内容</param>
        /// <param name="studentPerformed">学生表现</param>
        /// <param name="courseManagerFeedback">教师教学反馈</param>
        /// <param name="task">作业</param>
        /// <param name="courseQuestion">课堂问题</param>
        /// <param name="courseEmphasis">重点内容</param>
        /// <param name="operateId">操作员</param>
        /// <returns></returns>
        public Tuple<int, string> UpdateStudentCourseRecord(int studentCourseId, int courseManagerId, int studentCourseStatus, bool isChangeBook, int nextBookId, string changeBookCause, LearningInfoModel learningTarget, LearningInfoModel learningAbility, string studentVitae, string recommendBook, string buyIntention, string courseContent, string studentPerformed, string courseManagerFeedback, string task, string courseQuestion, string courseEmphasis, int operateId)
        {
            try
            {
                DateTime nowUtcTime = DateTime.UtcNow;
                StudentCourseRecordModel studentCourse = GetStudentCourseRecord(studentCourseId);
                using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
                {
                    if (studentCourse == null)
                        return new Tuple<int, string>(1, "学生预约课程信息不存在");
                    if (studentCourse.StartTime > nowUtcTime)
                        return new Tuple<int, string>(1, "该课程未到进度填写时间，请在该课结束以后开始进行进度填写");//至第二天凌晨一点之间
                    if (courseManagerId > 0 && courseManagerId != studentCourse.CourseManagerID)
                        return new Tuple<int, string>(1, "权限不足，不能操作非自己任课的课程");
                    v_ManagerMOD courseManager = new ManagerDAL().GetMOD_v(studentCourse.CourseManagerID);
                    if (courseManager == null)
                        return new Tuple<int, string>(1, "任课老师信息不存在");
                    if (courseManager.timeZone == null)
                        return new Tuple<int, string>(1, "任课老师的时区信息还未设置，请先设置所处时区");
                    studentCourse.IsChangeBook = isChangeBook;
                    if (studentCourse.IsChangeBook)//更换教材
                    {
                        var book = new BookDAL().GetModel(nextBookId);
                        if (book == null)
                            return new Tuple<int, string>(1, "请选择下节课的教材");
                        studentCourse.NextBookID = book.ID;
                        studentCourse.NextBookName = book.Name;
                        if (changeBookCause.IsNullOrWhiteSpace())
                            return new Tuple<int, string>(1, "请输入教材更换原因");
                        studentCourse.ChangeBookCause = changeBookCause;
                    }
                    if (studentCourse.StudentCourseStatus == 0 && studentCourseStatus <= 0)
                        return new Tuple<int, string>(1, "请选择该课程完成状态");
                    if (studentCourseStatus > 0)
                    {
                        StudentCourseStatusMOD status = new StudentCourseStatusDAL().GetStudentCourseStatus(studentCourseStatus);
                        if (status == null || status.StatusID <= 0)
                            return new Tuple<int, string>(1, "该课程状态暂时无法使用，请选择其他课程状态");
                        if (studentCourse.StudentProductID > 0 && studentCourse.StudentCourseStatus == 0 && status.IsEffective == false)//第一次更换课程状态，预约时默认已扣除学时，只做不计算学时的操作
                        {
                            var studentProduct = new StudentProductDAL().GetStudentProduct(studentCourse.StudentProductID);
                            if (studentProduct == null)
                            {
                                studentCourse.StudentProductID = 0;
                            }
                            else
                            {
                                int studentCourseCount = GetUesStudentCourseCountByProductId(studentCourse.StudentProductID);
                                studentProduct.RestOfCourseCount = studentProduct.TotalCourseCount - studentCourseCount + 1;
                                if (studentProduct.ProductStatus == (int)StudentProductStatus.NoUsed)//未使用的产品
                                {
                                    var studentCourseByMinStartTime = GetStudentCourseRecordList(studentCourse.StudentID).OrderBy(d => d.StartTime).FirstOrDefault(d => d.StudentProductID.Equals(studentProduct.ID) && d.ID != studentCourse.ID);
                                    studentProduct.StartDate = studentCourseByMinStartTime?.StartTime ?? studentProduct.AddTime;
                                    studentProduct.EndDate = studentProduct.StartDate.AddDays(studentProduct.LimitDate * 7);
                                }
                                if (studentProduct.TotalCourseCount < studentProduct.RestOfCourseCount)
                                    studentProduct.RestOfCourseCount = studentProduct.TotalCourseCount;
                                db.Entry(studentProduct).State = EntityState.Modified;
                            }
                        }
                        //再次更改课程状态，限制不能从不计算课时变成计算课时状态，其他的状态变化是可以的
                        if (studentCourse.StudentProductID > 0 && studentCourse.StudentCourseStatus > 0 && status.StatusID != studentCourse.StudentCourseStatus)
                        {
                            if (studentCourse.IsEffective == false && status.IsEffective)
                                return new Tuple<int, string>(1, "课程已是不计算课时状态，不允许修改为计算课时状态");
                        }
                        studentCourse.StudentCourseStatus = status.StatusID;
                        studentCourse.StudentCourseStatusChinaName = status.ChinaName;
                        studentCourse.StudentCourseStatusEnglishName = status.EnglishName;
                        studentCourse.IsEffective = status.IsEffective;
                    }
                    //修改学生课程的课程完成时间，以及判断是否已超时
                    if (studentCourse.CourseSuccessTime == null)
                    {
                        studentCourse.CourseSuccessTime = nowUtcTime;
                        if (studentCourse.StartTime.ConvertTime(courseManager.timeZone.TimeZoneInfoId).AddDays(1).Date.AddHours(1).ConvertUtcTime(courseManager.timeZone.TimeZoneInfoId) < nowUtcTime)
                        {
                            studentCourse.IsCourseSuccessTimeOut = true;
                            //return new Tuple<int, string>(1, "该课程已超过进度填写时间，请在该课结束以后至第二天凌晨一点之间进行进度填写");
                        }
                    }
                    if (studentCourse.StudentProductID <= 0)
                    {
                        studentCourse.LearningTarget = learningTarget;
                        studentCourse.LearningAbility = learningAbility;
                        studentCourse.StudentVitae = studentVitae;
                        studentCourse.RecommendBook = recommendBook;
                        studentCourse.BuyIntention = buyIntention;
                    }
                    studentCourse.CourseContent = courseContent;
                    studentCourse.StudentPerformed = studentPerformed;
                    studentCourse.CourseManagerFeedback = courseManagerFeedback;
                    studentCourse.Task = task;
                    studentCourse.CourseQuestion = courseQuestion;
                    studentCourse.CourseEmphasis = courseEmphasis;
                    studentCourse.CourseManagerTimeZoneID = courseManager.TimeZone;
                    //如果该节课计算课时，更新相关学生产品信息
                    if (studentCourse.StudentProductID > 0 && studentCourse.IsEffective)
                    {
                        StudentProductMOD studentProduct = new StudentProductDAL().GetStudentProduct(studentCourse.StudentProductID);
                        //当学生产品为未使用的时候，更改其状态和产品开始时间和结束时间
                        if (studentProduct != null && studentProduct.ProductStatus == (int)StudentProductStatus.NoUsed)
                        {
                            studentProduct.ProductStatus = (int)StudentProductStatus.Using;
                            v_StudentCourseRecordModel minStudentCourseRecord = GetStudentCourseRecordList(studentProduct.StudentID).OrderBy(d => d.StartTime).FirstOrDefault(d => d.StudentProductID.Equals(studentProduct.ID));
                            if (minStudentCourseRecord != null)
                                studentProduct.StartDate = minStudentCourseRecord.StartTime;
                            studentProduct.EndDate = studentProduct.StartDate.AddDays(studentProduct.LimitDate * 7);
                            db.Entry(studentProduct).State = EntityState.Modified;
                        }
                    }
                    db.Entry(studentCourse).State = EntityState.Modified;
                    db.SaveChanges();
                    scope.Complete();
                }
                ManagerLogDAL.AddManagerLog("更新学生课程进度信息", "学生课程记录ID：" + studentCourse.ID + "课程记录详情：" + studentCourse.ToJson(), ManagerLogType.Manager, operateId);
                return new Tuple<int, string>(0, "学生课程进度信息更新成功");
            }
            catch (Exception ex)
            {
                ManagerLogDAL.AddManagerLog("更新学生课程进度信息异常", "学生课程记录ID：" + studentCourseId + "，错误原因：" + ex.Message, ManagerLogType.Manager, operateId);
                return new Tuple<int, string>(0, "学生课程进度信息更新失败，" + ex.Message);
            }
        }

        /// <summary>
        /// 删除学生预约课程记录
        /// </summary>
        /// <param name="studentCourseId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Tuple<int, string> DeleteStudentCourseRecord(int studentCourseId, int studentId)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                try
                {
                    Tuple<int, string> tuple = CancelStudentCourseRecord(studentCourseId, studentId);
                    if (tuple.Item1 != 0)
                        return tuple;
                    StudentCourseRecordModel model = GetStudentCourseRecord(studentCourseId);
                    if (model != null)
                    {
                        db.Entry(model).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                    scope.Complete();
                    return new Tuple<int, string>(0, "删除学生预约课程记录成功");
                }
                catch (Exception ex)
                {

                    return new Tuple<int, string>(1, "删除学生预约课程记录发生异常，" + ex.Message);
                }
            }
        }
        /// <summary>
        /// 根据学生产品获取已使用学生课程课时
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public int GetUesStudentCourseCountByProductId(int productId)
        {
            return GetStudentCourseRecordList().Count(d => d.StudentProductID.Equals(productId) && d.IsEffective);
        }
        /// <summary>
        /// 获取任课老师黑名单集合
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public IQueryable<v_CourseManagerBlackListMOD> GetCourseManagerBlackList_v(int studentId = 0)
        {
            var data = from cmb in db.CourseManagerBlackList
                       join s in db.Student on cmb.StudentId equals s.ID into t1
                       from st in t1
                       join cm in db.Manager on cmb.CourseManagerId equals cm.ID into t2
                       from cmt in t2.DefaultIfEmpty()
                       join om in db.Manager on cmb.OperateId equals om.ID into t3
                       from omt in t3.DefaultIfEmpty()
                       select new v_CourseManagerBlackListMOD()
                       {
                           Id = cmb.Id,
                           AddTime = cmb.AddTime,
                           StudentId = cmb.StudentId,
                           StudentChinaName = st.ChinaName,
                           StudentEnglishName = st.EnglishName,
                           StudentLoginName = st.LoginName,
                           CourseManagerId = cmb.CourseManagerId,
                           CourseManagerName = cmt.ManagerName,
                           OperateId = cmb.OperateId,
                           OperateName = omt.ManagerName
                       };
            if (studentId > 0)
                data = data.Where(d => d.StudentId.Equals(studentId));
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取任课老师黑名单集合
        /// </summary>
        /// <param name="studentId">学生Id</param>
        /// <returns></returns>
        public IQueryable<CourseManagerBlackListMOD> GetCourseManagerBlackList(int studentId = 0)
        {
            return db.CourseManagerBlackList.Where(d => d.StudentId.Equals(studentId));
        }
        /// <summary>
        /// 获取任课老师黑名单视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public v_CourseManagerBlackListMOD GetCourseManagerBlack_v(int id)
        {
            return GetCourseManagerBlackList_v().FirstOrDefault(d => d.Id.Equals(id));
        }
        /// <summary>
        /// 获取任课老师黑名单信息
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="courseManagerId"></param>
        /// <returns></returns>
        public CourseManagerBlackListMOD GetCourseManagerBlack(int studentId, int courseManagerId)
        {
            return db.CourseManagerBlackList.FirstOrDefault(d =>
                d.StudentId.Equals(studentId) && d.CourseManagerId.Equals(courseManagerId));
        }
        /// <summary>
        /// 添加任课老师黑名单信息
        /// </summary>
        /// <param name="studentId">学生Id</param>
        /// <param name="courseManagerId">任课老师Id</param>
        /// <param name="operateId">操作人Id</param>
        /// <returns></returns>
        public Tuple<int, string> AddCourseManagerBlack(int studentId, int courseManagerId, int operateId = 0)
        {
            StudentMOD student = new StudentDal().GetMod(studentId);
            if (student == null)
            {
                return new Tuple<int, string>(1, "学生信息不存在");
            }
            ManagerModel courseManager = new ManagerDAL().GetMod(courseManagerId);
            if (courseManager == null)
            {
                return new Tuple<int, string>(1, "任课老师信息不存在");
            }
            CourseManagerBlackListMOD courseManagerBlack = GetCourseManagerBlack(studentId, courseManagerId);
            if (courseManagerBlack != null)
            {
                return new Tuple<int, string>(1, "该任课老师黑名单信息已存在");
            }
            courseManagerBlack = new CourseManagerBlackListMOD()
            {
                CourseManagerId = courseManagerId,
                StudentId = studentId,
                OperateId = operateId,
                AddTime = DateTime.UtcNow
            };
            db.Entry(courseManagerBlack).State = EntityState.Added;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("添加任课老师黑名单",
                "任课老师ID：" + courseManagerBlack.CourseManagerId + ",学生ID:" + courseManagerBlack.StudentId,
                operateId > 0 ? ManagerLogType.Manager : ManagerLogType.Student, operateId);
            return new Tuple<int, string>(0, "添加任课老师黑名单信息成功");
        }
        /// <summary>
        /// 删除任课老师黑名单
        /// </summary>
        /// <param name="courseManagerId">任课老师Id</param>
        /// <param name="studentId">学生Id</param>
        /// <param name="operateId">操作人</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteCourseManagerBlack(int courseManagerId, int studentId, int operateId = 0)
        {
            var courseManagerBlack = db.CourseManagerBlackList.FirstOrDefault(d =>
                d.CourseManagerId.Equals(courseManagerId) && d.StudentId.Equals(studentId));
            if (courseManagerBlack == null)
                return new Tuple<int, string>(1, "任课老师黑名单信息不存在");
            db.Entry(courseManagerBlack).State = EntityState.Deleted;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("删除任课老师黑名单",
                "任课老师ID：" + courseManagerBlack.CourseManagerId + ",学生ID:" + courseManagerBlack.StudentId,
               operateId > 0 ? ManagerLogType.Manager : ManagerLogType.Student, operateId);
            return new Tuple<int, string>(0, "删除任课老师黑名单成功");
        }
        /// <summary>
        /// 学生填写课程反馈情况
        /// </summary>
        /// <param name="id">学生课程ID</param>
        /// <param name="studentId">学生ID</param>
        /// <param name="studentRateYourLesson">课程评分</param>
        /// <param name="studentRateYourTeacher">老师评分</param>
        /// <param name="studentFeeback">反馈情况</param>
        /// <returns></returns>
        public Tuple<int, string> StudentFeedback(int id, int studentId, int studentRateYourLesson,
            int studentRateYourTeacher, string studentFeeback)
        {
            var studentCourse = GetStudentCourseRecord(id);
            if (studentCourse == null || studentCourse.StudentID != studentId)
                return new Tuple<int, string>(1, "This session does not exist.");//学生课程记录不存在
            if (studentCourse.StartTime > DateTime.UtcNow)
                return new Tuple<int, string>(1, "It's still too early to provide feedback for this lesson.");//该课程未到反馈时间
            if (studentRateYourLesson <= 0 || studentRateYourLesson > 10)
                return new Tuple<int, string>(1, "Error! Please refresh your browser.");//课程评分值异常，请重新选择
            if (studentRateYourTeacher <= 0 || studentRateYourTeacher > 10)
                return new Tuple<int, string>(1, "Error! Please refresh your browser.");//任课老师评分值异常，请重新选择

            studentCourse.StudentRateLesson = studentRateYourLesson;
            studentCourse.StudentRateCourseManager = studentRateYourTeacher;
            studentCourse.StudentFeeback = studentFeeback;
            studentCourse.StudentFeedbackTime = DateTime.UtcNow;
            db.Entry(studentCourse).State = EntityState.Modified;
            db.SaveChanges();
            return new Tuple<int, string>(0, "Thank you! Your feedback helps us improve.");//反馈信息提交成功
        }
    }
}
