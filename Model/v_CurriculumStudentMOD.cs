using System;
using System.Collections.Generic;
using System.Linq;
using static Common.Keys;
using Common;
using Common.Extend;

namespace Model
{
    /// <summary>
    /// 学生课程表视图实体
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class v_CurriculumStudentMOD
    {
        #region 原字段
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 课程老师ID
        /// </summary>
        public int CourseManagerID { get; set; }
        /// <summary>
        /// 课程表 ,1-7,1-8,
        /// </summary>
        public string Curriculum { get; set; }
        /// <summary>
        /// 所属时区
        /// </summary>
        public int TimeZoneID { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public int EditManagerID { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentLoginName { get; set; }
        /// <summary>
        /// 课程老师姓名
        /// </summary>
        public string CourseManagerName { get; set; }
        /// <summary>
        /// 修改管理员姓名
        /// </summary>
        public string EditManagerName { get; set; }
        /// <summary>
        /// 时区信息
        /// </summary>
        public TimeZoneModel TimeZone { get; set; }
        /// <summary>
        /// 所处时区的课程表集合
        /// </summary>
        public List<CurricumModel> CurriculumList
        {
            get => CurricumModel.GetCurricumListByStrCurricum(Curriculum, TimeZone?.TimeZoneInfoId);
            set => Curriculum = CurricumModel.GetStrCurricumByCurricumList(value, TimeZone?.TimeZoneInfoId);
        }
        #endregion
    }
    /// <summary>
    /// 课程表实体
    /// </summary>
    public class CurricumModel
    {
        // ReSharper disable once InconsistentNaming
        public int iWeek { get; set; }
        /// <summary>
        /// 周几
        /// </summary>
        public Week Week
        {
            get => (Week)iWeek;
            set => iWeek = value.ToInt();
        }
        public string WeekName => Week.ToString();

        /// <summary>
        /// 几时
        /// </summary>
        public List<double> HourseList { get; set; }

        /// <summary>
        /// 课程表字符串转为课程表集合对象
        /// </summary>
        /// <param name="strCurricum">0时区的课程表集合</param>
        /// <param name="timeZoneInfoId">TimeZoneInfo时区标识Id</param>
        /// <returns></returns>
        public static List<CurricumModel> GetCurricumListByStrCurricum(string strCurricum, string timeZoneInfoId = "")
        {
            List<CurricumModel> list = new List<CurricumModel>();
            if (!strCurricum.IsNullOrWhiteSpace())
            {
                double utcTotalHours = 0;
                if (!timeZoneInfoId.IsNullOrWhiteSpace())
                    utcTotalHours = TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfoId).BaseUtcOffset.TotalHours;
                foreach (var curriculum in strCurricum.Split(','))
                {
                    if (!curriculum.IsNullOrWhiteSpace())
                    {
                        try
                        {
                            string[] curr = curriculum.Split('-');
                            if (curr.Length >= 2)
                            {
                                int week = curr[0].ToInt();
                                double hours = curr[1].ToDouble();
                                var tuple = ToUtcWeekHours(week, hours, -utcTotalHours);
                                var curricum = list.FirstOrDefault(l => l.iWeek.Equals(tuple.Item1));//查找已有的星期数
                                if (curricum == null || curricum.iWeek != tuple.Item1)
                                    list.Add(new CurricumModel { iWeek = tuple.Item1, HourseList = new List<double> { tuple.Item2 } });
                                else if (curricum.HourseList.Count(c => c.Equals(tuple.Item2)) <= 0)//已有周数则直接添加小时数
                                    curricum.HourseList.Add(tuple.Item2);
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }
            }
            //排序
            foreach (var item in list)
            {
                item.HourseList = item.HourseList.OrderBy(d => d).ToList();
            }
            return list.OrderBy(d => d.iWeek).ToList();
        }

        /// <summary>
        /// 课程表字符串转为课程表集合对象
        /// </summary>
        /// <param name="curricumList">时区所在的课程表集合</param>
        /// <param name="timeZoneInfoId">TimeZoneInfo时区标识Id</param>
        /// <returns></returns>
        public static string GetStrCurricumByCurricumList(List<CurricumModel> curricumList, string timeZoneInfoId = "")
        {
            if (!timeZoneInfoId.IsNullOrWhiteSpace())
            {
                double utcTotalHours = 0;
                utcTotalHours = TimeZoneInfo.FindSystemTimeZoneById(timeZoneInfoId).BaseUtcOffset.TotalHours;
                List<CurricumModel> newCurricumList = new List<CurricumModel>();
                foreach (var model in curricumList)
                {
                    foreach (var hours in model.HourseList)
                    {
                        var tuple = ToUtcWeekHours(model.iWeek, hours, utcTotalHours);
                        var curricum = newCurricumList.FirstOrDefault(l => l.iWeek.Equals(tuple.Item1));//查找已有的星期数
                        if (curricum == null || curricum.iWeek != tuple.Item1)
                            newCurricumList.Add(new CurricumModel { iWeek = tuple.Item1, HourseList = new List<double> { tuple.Item2 } });
                        else if (curricum.HourseList.Count(c => c.Equals(tuple.Item2)) <= 0)//已有周数则直接添加小时数
                            curricum.HourseList.Add(tuple.Item2);
                    }
                }
                curricumList = newCurricumList;
            }
            string curr = ",";
            foreach (var item in curricumList.OrderBy(d => d.iWeek))
            {
                if (item.HourseList.Count >= 0)
                    foreach (var hourse in item.HourseList.OrderBy(d => d))
                    {
                        var weekHourse = item.iWeek + "-" + hourse + ",";
                        if (!curr.Contains(weekHourse))
                            curr += weekHourse;
                    }
            }
            if (curr == ",")
                curr = "";
            return curr;
        }
        /// <summary>
        /// 将本地时间转换为UTC时间
        /// </summary>
        /// <param name="week">周几</param>
        /// <param name="hours">小时</param>
        /// <param name="utcTotalHours">UTC值</param>
        /// <returns></returns>
        public static Tuple<int, double> ToUtcWeekHours(int week, double hours, double utcTotalHours)
        {
            hours = (hours - utcTotalHours);
            for (; hours < 0;)//上一天
            {
                week -= 1;
                hours += 24;
            }
            for (; hours >= 24;)//下一天
            {
                week += 1;
                hours -= 24;
            }
            for (; week < 0;)//上一周
                week += 7;
            for (; week > 6;)//下一周
                week -= 7;
            return new Tuple<int, double>(week, hours);
        }
    }
}
