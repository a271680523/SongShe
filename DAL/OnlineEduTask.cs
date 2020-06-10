using System;
using System.Timers;
using Common;
using Timer = System.Timers.Timer;

namespace DAL
{
    public class OnlineEduTask
    {
        ///// <summary>
        ///// 网站根路径
        ///// </summary>
        //private readonly string _baseMapPath;
        /// <summary>
        /// 根据课程表发布课程信息的最后一次执行时间
        /// </summary>
        private DateTime _lastRuntimeByCreateCourseRecordByCurricuManager;

        private readonly Timer _timer;
        /// <summary>
        /// 
        /// </summary>
        ///// <param name="baseMapPath">网站根路径</param>
        public OnlineEduTask()//string baseMapPath
        {
            //_baseMapPath = baseMapPath;
            //Log.BaseMapPath = baseMapPath;
            _timer = new Timer(1000 * 60);
            _timer.Elapsed += Timer_Elapsed;
            _timer.AutoReset = true;//设置一直执行(true)
            _timer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件
            _timer.Start();
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _timer.Enabled = false;//是否执行System.Timers.Timer.Elapsed事件
                //_timer.Stop();
                DateTime nowTime = DateTime.UtcNow;
                //每周执行一次
                if (_lastRuntimeByCreateCourseRecordByCurricuManager < nowTime.AddDays(-nowTime.DayOfWeek.ToString("d").ToInt()).Date)
                {
                    //需要执行的事务
                    #region 根据管理员课程表发布课程
                    //判断是否自动发布课程
                    if (SystemParameDAL.GetSystemParameValue(Keys.SystemParameId.IsAutoAddCourseRecord) == 1)
                    {
                        var tuple = new CurriculumDAL().CreateCourseRecordByCurricuManager();
                        string content = tuple.Item2 ;//日志内容
                        NLogger.WriteLog(string.Empty, content);
                        //Log.AddSystemLog(content);
                    }
                    #endregion
                    #region 根据学生课程表预约学生课程
                    var tuple2 = new CurriculumDAL().AddStudentCourseRecordByCurricuStudent();
                    if (tuple2.Item1 > 0)
                        NLogger.WriteLog(string.Empty, "更新学生状态失败," + tuple2.Item2);
                    //Log.AddSystemLog("更新学生状态失败," + tuple2.Item2);
                    #endregion
                    _lastRuntimeByCreateCourseRecordByCurricuManager = nowTime;
                }
                #region 更新已过期的学生产品状态
                var tuple1 = new StudentProductDAL().UpdateStudentProductStatus();
                if (tuple1.Item1 > 0)
                    NLogger.WriteLog(string.Empty, "更新学生状态失败," + tuple1.Item2);
                //Log.AddSystemLog("更新学生状态失败," + tuple1.Item2);
                #endregion

                #region 更新学生休学状态
                var updateStudentSuspendSchoolingRecordStatus = new StudentSuspendSchoolingRecordDal().UpdateStatus();
                if (updateStudentSuspendSchoolingRecordStatus.Item1 > 0)
                    NLogger.WriteLog(string.Empty, "更新学生休学记录状态失败，" + updateStudentSuspendSchoolingRecordStatus.Item2);
                //Log.AddSystemLog("更新学生休学记录状态失败，" + updateStudentSuspendSchoolingRecordStatus.Item2);
                #endregion

            }
            catch (Exception ex)
            {
                NLogger.ErrorLog(ex);
                //Log.AddErrorLog(content);
            }
            finally
            {
                _timer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件
                //_timer.Start();
            }
        }
    }
}
