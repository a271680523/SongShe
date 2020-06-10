using System;
using System.Data.Entity;
using System.Linq;
using Common;
using Model;

namespace DAL
{
    /// <summary>
    /// 休学记录数据层
    /// </summary>
    public class StudentSuspendSchoolingRecordDal : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取学生休学记录
        /// </summary>
        /// <param name="id">休学记录ID</param>
        /// <returns></returns>
        public StudentSuspendSchoolingRecordMOD GetMod(int id)
        {
            return db.StudentSuspendSchoolingRecord.FirstOrDefault(d => d.ID == id);
        }
        /// <summary>
        /// 获取休学记录集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<StudentSuspendSchoolingRecordMOD> GetList()
        {
            return db.StudentSuspendSchoolingRecord.AsQueryable();
        }
        /// <summary>
        /// 获取休学记录视图集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_StudentSuspendSchoolingRecordMOD> GetList_v()
        {
            return GetList_v(0, 0);
        }
        /// <summary>
        /// 获取休学记录视图集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_StudentSuspendSchoolingRecordMOD> GetListByStudentId_v(int studentId)
        {
            return GetList_v(studentId, 0);
        }
        /// <summary>
        /// 获取休学记录视图集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_StudentSuspendSchoolingRecordMOD> GetListByProductId_v(int productId)
        {
            return GetList_v(0, productId);
        }
        /// <summary>
        /// 获取休学记录视图集合 
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_StudentSuspendSchoolingRecordMOD> GetList_v(int studentId, int productId)
        {
            var data = from sssp in db.StudentSuspendSchoolingRecord
                       join s in db.Student on sssp.StudentID equals s.ID into t1
                       from st in t1.DefaultIfEmpty()
                       join sp in db.StudentProduct on sssp.ProductID equals sp.ID into t2
                       from spt in t2.DefaultIfEmpty()
                       join tz in db.TimeZone on sssp.TimeZoneId equals tz.ID into t3
                       from tzt in t3.DefaultIfEmpty()
                       orderby sssp.StartTime descending
                       select new v_StudentSuspendSchoolingRecordMOD
                       {
                           ID = sssp.ID,
                           StudentID = sssp.StudentID,
                           ProductID = sssp.ProductID,
                           StartTime = sssp.StartTime,
                           EndTime = sssp.EndTime,
                           AddTime = sssp.AddTime,
                           Remark = sssp.Remark,
                           StudentName = st.ChinaName,
                           ProductName = spt.ProductName,
                           TimeZoneId = sssp.TimeZoneId,
                           TimeZone = tzt,
                           AddStudentProductLimitDay = sssp.AddStudentProductLimitDay,
                           SuspendType = sssp.SuspendType,
                           UseLeaveCount = sssp.UseLeaveCount,
                           ProductEndTime = sssp.ProductEndTime,
                           RestOfLeaveCount = sssp.RestOfLeaveCount,
                           Status = sssp.Status,
                           StatusName = sssp.Status == -1 ? "Cancel" : sssp.Status == 0 ? "Waiting" : sssp.Status == 1 ? "Using" : sssp.Status == 2 ? "Completed" : ""
                       };
            if (studentId > 0)
                data = data.Where(d => d.StudentID.Equals(studentId));
            if (productId > 0)
                data = data.Where(d => d.ProductID.Equals(productId));
            return data;
        }
        /// <summary>
        /// 学生申请休学
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="startTime">开始休学时间</param>
        /// <param name="remark">休学备注</param>
        /// <returns></returns>
        public Tuple<int, string> AddSuspendSchooling(int studentId, DateTime startTime, string remark)
        {
            v_StudentMOD student = new StudentDal().GetMOD_v(studentId);
            if (student == null)
                return new Tuple<int, string>(1, "This account does not exist.");//当前学生信息不存在
            //获取学生在读产品信息
            StudentProductMOD studentProduct = new StudentProductDAL().GetStudentProductByUsing(studentId);
            //studentProduct = new StudentProductDAL().GetStudentProduct(17);
            if (studentProduct == null || studentProduct.StudentID != studentId)
                return new Tuple<int, string>(1, "No more suspension days available.");//当前无在读产品信息
            if (studentProduct.LeaveCount <= studentProduct.UseLeaveCount)
                return new Tuple<int, string>(1, "No more suspension days available.");//当前在读产品无可休学时长
            //将周换算成天
            if (studentProduct.LeaveCount < 7)
                studentProduct.LeaveCount = studentProduct.LeaveCount * 7;
            if (startTime < DateTime.UtcNow)
                return new Tuple<int, string>(1, "Please choose a time for beginning your suspension later than now.");//请选择当前时间以后的休学开始时间
            startTime = startTime.Date.ConvertUtcTime(student.timeZone.TimeZoneInfoId);
            //单次休学的天数
            var suspendCycleDay = SystemParameDAL.GetSystemParameValue(Keys.SystemParameId.SuspendSchoolingDay);
            //实际可休学天数,如果剩余可休学天数小于单次休学的天数，则只修剩余可休学天数
            var suspendSchoolingDay = 0;
            //单次休学的结束时间
            DateTime suspendCycleEndTime = startTime.AddDays(suspendCycleDay);
            int holidayDay = 0;//和休学时间段存在重复的假期天数
            //获取单次休学时间段内的假期
            var holidayRecordList = new HolidayDAL().GetList().Where(d => startTime >= d.StartTime && startTime < d.EndTime || suspendCycleEndTime > d.StartTime && suspendCycleEndTime <= d.EndTime || d.StartTime >= startTime && d.StartTime < suspendCycleEndTime || d.EndTime > startTime && d.EndTime <= suspendCycleEndTime).ToList();
            //循环判断每天是否是有效的休学天
            for (int i = 0; i < suspendCycleDay; i++)
            {
                if (studentProduct.LeaveCount - studentProduct.UseLeaveCount > suspendSchoolingDay)
                {
                    if (!holidayRecordList.Any(d => d.StartTime <= startTime.AddDays(i) && d.EndTime > startTime.AddDays(i)))
                        suspendSchoolingDay++;
                    else
                        holidayDay++;
                }
                else
                    break;
            }
            DateTime endTime = startTime.AddDays(holidayDay + suspendSchoolingDay);
            var cout = db.StudentSuspendSchoolingRecord.Count(s => ((s.StartTime < startTime && startTime < s.EndTime) || (s.StartTime < endTime && endTime < s.EndTime)) && s.StudentID.Equals(studentId) && s.SuspendType == 0);
            if (cout > 0)
                return new Tuple<int, string>(1, "The suspension period overlaps with another existing suspension.");//当前休学时间段有正在休学的记录
            if (db.StudentCourseRecord.Count(d => d.StudentProductID == studentProduct.ID && d.StudentCourseStatus >= 0 && d.StartTime >= startTime && d.StartTime < endTime) > 0)
                return new Tuple<int, string>(1, "At least one lesson is already planned during the requested suspension. Please cancel lessons planned during this period before completing the request.");//当前休学时间段有已预约的课程
            studentProduct.UseLeaveCount += suspendSchoolingDay;
            studentProduct.EndDate = studentProduct.EndDate.AddDays(suspendSchoolingDay);
            ////计算到期提示显示距离结束时间的天数 (产品时长*百分值)
            //int productStartRechargePromptDayPercentage = SystemParameDAL.GetSystemParameValue(Keys.SystemParameId.ProductStartRechargePromptDayPercentage);
            //studentProduct.StartRechargePromptTime = studentProduct.EndDate.AddDays(-(studentProduct.LimitDate * productStartRechargePromptDayPercentage / 100));
            StudentSuspendSchoolingRecordMOD studentSuspendSchooling = new StudentSuspendSchoolingRecordMOD
            {
                StudentID = studentId,
                ProductID = studentProduct.ID,
                Remark = remark,
                StartTime = startTime,
                EndTime = endTime,
                AddTime = DateTime.UtcNow,
                TimeZoneId = student.TimeZone,
                AddStudentProductLimitDay = suspendSchoolingDay,
                SuspendType = 0,
                UseLeaveCount = suspendSchoolingDay
            };
            db.Entry(studentSuspendSchooling).State = EntityState.Added;
            db.Entry(studentProduct).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("学生申请休学",
                "学生ID：" + studentId + ",学生产品：" + studentProduct.ProductName + ",开始休学时间:" +
                startTime.ToString("yyyy-MM-dd HH:mm:ss") + ",备注：" + remark, ManagerLogType.Student, studentId);
            //return new Tuple<int, string>(0, "休学申请成功，开始休学时间：" + startTime.ConvertTime(student.timeZone.TimeZoneInfoId).ToString("yyyy-MM-dd HH:mm:ss") + "至" + endTime.ConvertTime(student.timeZone.TimeZoneInfoId).ToString("yyyy-MM-dd HH:mm:ss"));
            return new Tuple<int, string>(0, $"Your request has been processed successfully. Your account will be suspended starting {startTime.ConvertTime(student.timeZone.TimeZoneInfoId):yyyy-MM-dd HH:mm:ss} and ending {endTime.ConvertTime(student.timeZone.TimeZoneInfoId).ToString("yyyy-MM-dd HH:mm:ss")}.");
        }

        /// <summary>
        /// 取消休学
        /// </summary>
        /// <param name="suspendSchoolingRecordId">休学记录ID</param>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public Tuple<int, string> CancelSuspendSchoolingRecord(int suspendSchoolingRecordId, int studentId)
        {
            v_StudentMOD student = new StudentDal().GetMOD_v(studentId);
            if (student == null)
                return new Tuple<int, string>(1, "This account does not exist.");//当前学生信息不存在
            var suspendSchoolingRecord = GetMod(suspendSchoolingRecordId);
            if (suspendSchoolingRecord == null || suspendSchoolingRecord.StudentID != studentId)
                return new Tuple<int, string>(1, "The suspension no longer exists.");//休学记录不存在或已取消
            var studentProduct = new StudentProductDAL().GetStudentProduct(suspendSchoolingRecord.ProductID);
            if (studentProduct == null)
                return new Tuple<int, string>(1, "Error: It is not possible to edit or cancel this suspension. It is already in the past.");//该休学记录不可取消
            if (suspendSchoolingRecord.EndTime < DateTime.UtcNow)
                return new Tuple<int, string>(1, "This suspension has already finished.");//当前休学时间已结束
            //TimeSpan endTimeTicks = new TimeSpan(suspendSchoolingRecord.EndTime.Ticks);
            //TimeSpan startTimeTicks = new TimeSpan(suspendSchoolingRecord.StartTime.Ticks);
            //var day = endTimeTicks.Subtract(startTimeTicks).Duration().Days;
            var studentCourseStartTime = studentProduct.EndDate.AddDays(-suspendSchoolingRecord.UseLeaveCount);
            var studentCourseEndTime = studentProduct.EndDate;
            var existStudentCourse = new StudentCourseRecordDAL().GetStudentCourseRecordList().Where(d => d.StudentProductID == studentProduct.ID && d.StartTime >= studentCourseStartTime && d.StartTime <= studentCourseEndTime);
            if (existStudentCourse.Any())
            {
                //return new Tuple<int, string>(1, $"产品(" + studentProduct.ProductName + ")" + studentCourseStartTime.ToString("yyyy-MM-dd HH:mm:ss") + "至" + studentCourseEndTime.ToString("yyyy-MM-dd HH:mm:ss") + "期间存在已预约的课程,请先取消该时间段内已预约的课程");
                return new Tuple<int, string>(1, $"You have already scheduled some lessons between {studentCourseStartTime:yyyy-MM-dd HH:mm:ss} and {studentCourseEndTime:yyyy-MM-dd HH:mm:ss}. Please cancel these lessons before requesting a suspension.");
            }

            studentProduct.UseLeaveCount -= suspendSchoolingRecord.UseLeaveCount;
            studentProduct.EndDate = studentProduct.EndDate.AddDays(-suspendSchoolingRecord.UseLeaveCount);
            ////计算到期提示显示距离结束时间的天数 (产品时长*百分值)
            //int productStartRechargePromptDayPercentage = SystemParameDAL.GetSystemParameValue(Keys.SystemParameId.ProductStartRechargePromptDayPercentage);
            //studentProduct.StartRechargePromptTime = studentProduct.EndDate.AddDays(-(studentProduct.LimitDate * productStartRechargePromptDayPercentage / 100));
            db.Entry(suspendSchoolingRecord).State = EntityState.Deleted;
            db.Entry(studentProduct).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("学生取消休学", "学生ID：" + studentId + ",学生产品：" + studentProduct.ProductName + ",休学时间:" + suspendSchoolingRecord.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + "至" + suspendSchoolingRecord.EndTime.ToString("yyyy-MM-dd HH:mm:ss") + ",备注：" + suspendSchoolingRecord.Remark, ManagerLogType.Student, studentId);
            //return new Tuple<int, string>(0, "已成功取消" + suspendSchoolingRecord.StartTime.ConvertTime(student.timeZone.TimeZoneInfoId).ToString("yyyy-MM-dd HH:mm:ss") + "至" + suspendSchoolingRecord.EndTime.ConvertTime(student.timeZone.TimeZoneInfoId).ToString("yyyy-MM-dd HH:mm:ss") + "的休学申请");
            return new Tuple<int, string>(0, "Your request has been processed successfully.");
        }
        /// <summary>
        /// 更新满足条件的休学记录状态 休学中和休学结束
        /// </summary>
        /// <returns></returns>
        public Tuple<int, string> UpdateStatus()
        {
            DateTime nowUtcTime = DateTime.UtcNow;
            #region 更新进入休学中的记录

            var data = GetList().Where(d => d.StartTime <= nowUtcTime && nowUtcTime <= d.EndTime && d.Status == (int)Keys.StudentSuspendSchoolingRecordStatus.Waiting).ToList();

            foreach (var model in data)
            {
                model.Status = (int)Keys.StudentSuspendSchoolingRecordStatus.Using;
                db.Entry(model).State = EntityState.Modified;
            }
            db.SaveChanges();
            #endregion
            #region 更新已完成休学的记录
            //获取满足休学已结束条件的休学
            data = GetList().Where(d => d.StartTime <= nowUtcTime && (d.Status == (int)Keys.StudentSuspendSchoolingRecordStatus.Using || d.Status == (int)Keys.StudentSuspendSchoolingRecordStatus.Waiting)).ToList();
            int updateRecordCount = 0;
            string strCompletedRecordIds = "";
            foreach (var model in data)
            {
                model.Status = (int)Keys.StudentSuspendSchoolingRecordStatus.Completed;
                var lastCompletedSuspendSchooling = GetList_v()
                    .Where(d => d.ProductID.Equals(model.ProductID) &&
                                d.Status == (int)Keys.StudentSuspendSchoolingRecordStatus.Completed)
                    .OrderByDescending(d => d.EndTime).FirstOrDefault();
                if (lastCompletedSuspendSchooling?.ProductEndTime == null)//如果没有找到已完成的休学记录，或者已完成的休学记录的产品结束为空，则根据产品信息计算
                {
                    StudentProductMOD studentProduct = new StudentProductDAL().GetStudentProduct(model.ProductID);
                    if (studentProduct == null)
                        continue;
                    //已休学成功的天数
                    int useLeaveCount = GetListByProductId_v(model.ProductID).Where(d => d.Status == (int)Keys.StudentSuspendSchoolingRecordStatus.Completed).Sum(d => (int?)d.UseLeaveCount) ?? 0;
                    lastCompletedSuspendSchooling =
                        new v_StudentSuspendSchoolingRecordMOD
                        {
                            ProductEndTime = studentProduct.StartDate.AddDays(studentProduct.LimitDate * 7).AddDays(useLeaveCount),//产品开始时间加上时长为标准的结束时间，再加上已休学天数
                            RestOfLeaveCount = studentProduct.LeaveCount - studentProduct.UseLeaveCount
                        };
                }
                model.ProductEndTime = lastCompletedSuspendSchooling.ProductEndTime.ToDateTime(model.EndTime)
                    .AddDays(model.AddStudentProductLimitDay);
                model.RestOfLeaveCount = lastCompletedSuspendSchooling.RestOfLeaveCount - model.UseLeaveCount;
                db.Entry(model).State = EntityState.Modified;
                updateRecordCount++;
                strCompletedRecordIds += model.ID + ",";
            }
            db.SaveChanges();
            #endregion
            if (updateRecordCount > 0)
                ManagerLogDAL.AddManagerLog("更新已结束休学的休学记录",
                    "本次更新已完成的休学记录" + updateRecordCount + "条,包含休学记录ID：" + strCompletedRecordIds, ManagerLogType.System);
            return new Tuple<int, string>(0, "本次更新已完成的休学记录" + updateRecordCount + "条");
        }
    }
}
