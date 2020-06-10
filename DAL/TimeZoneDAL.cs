//////////////////////////////////////////////////////////////////
//CreateTime	2018-1-29 16:35:25
//CreateBy 		唐翔
//Content       时区数据处理类
//////////////////////////////////////////////////////////////////
using Model;
using System;
using System.Linq;
using Common;

namespace DAL
{
    /// <summary>
    /// 时区数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class TimeZoneDAL : Base.BaseDAL
    {

        //public static DateTime ToLocalTime(this DateTime utcTime,int timeZoneId)
        //{
        //    TimeZoneInfo.
        //    DateTimeOffset dateTimeOffset=new DateTimeOffset().Subtract
        //    new DateTime(utc)
        //}
        /// <summary>
        /// 获取时区信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TimeZoneModel GetModel(int id)
        {
            var model = db.TimeZone.FirstOrDefault(d => d.ID.Equals(id));
            return model;
        }
        /// <summary>
        /// 根据时区值获取时区信息
        /// </summary>
        /// <param name="utcValue">时区值</param>
        /// <returns></returns>
        public TimeZoneModel GetModelByUtcValue(double utcValue)
        {
            var model = db.TimeZone.FirstOrDefault(d => d.UTCTotalHours.Equals(utcValue));
            return model;
        }
        /// <summary>
        /// 获取时区信息集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<TimeZoneModel> GetList()
        {
            ////将本地时间转换为美国标准中部时间
            //DateTime dt = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local);
            //Console.WriteLine(dt.ToString());

            //dt = TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time"));
            //Console.WriteLine(dt.ToString());
            // 列举所有支持的时区列表
            //System.Collections.ObjectModel.ReadOnlyCollection<TimeZoneInfo> lst = TimeZoneInfo.GetSystemTimeZones();
            //foreach (TimeZoneInfo tzi in lst)
            //{
            //    Console.WriteLine(tzi.Id);
            //}

            var data = from t in db.TimeZone
                       orderby t.ID
                       select t;
            return data;
        }

        /// <summary>
        /// 获取时区信息集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<TimeZoneModel> GetEnableList(bool isEnable = true)
        {
            var data = from t in db.TimeZone
                       orderby t.ID
                       where t.IsEnable.Equals(isEnable)
                       select t;
            return data;
        }
        /// <summary>
        /// 添加或修改时区信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public Tuple<int, string> AeModel(TimeZoneModel model, int managerId)
        {
            TimeZoneModel timeZone = new TimeZoneModel();
            if (model.ID > 0)
            {
                timeZone = GetModel(model.ID);
                if (timeZone == null || timeZone.ID != model.ID)
                    return new Tuple<int, string>(1, "时区信息不存在");
            }
            if (model.Name.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请填写时区中文名称");
            if (model.EnglishName.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请填写时区英文名称");
            timeZone.Name = model.Name;
            timeZone.EnglishName = model.EnglishName;
            timeZone.IsEnable = model.IsEnable;
            if (timeZone.ID > 0)
            {
                db.Entry(timeZone).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ManagerLogDAL.AddManagerLog("修改时区信息", "TimeZoneMOD:" + timeZone.ToJson(), ManagerLogType.Manager, managerId);
                return new Tuple<int, string>(0, "修改时区信息成功");
            }
            else
            {
                timeZone = db.TimeZone.Add(timeZone);
                db.SaveChanges();
                ManagerLogDAL.AddManagerLog("添加时区信息", "TimeZoneMOD:" + timeZone.ToJson(), ManagerLogType.Manager, managerId);
                return new Tuple<int, string>(0, "添加时区信息成功");
            }
        }
    }
}
