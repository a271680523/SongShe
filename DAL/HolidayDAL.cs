//////////////////////////////////////////////////////////////////
//CreateTime	2018-1-29 16:35:25
//CreateBy 		唐翔
//Content       假期信息数据处理类
//////////////////////////////////////////////////////////////////
using System;
using System.Data.Entity;
using System.Linq;
using Common;
using Model;

namespace DAL
{
    /// <summary>
    /// 假期信息数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class HolidayDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取假期记录视图集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_HolidayRecordModel> GetList_v()
        {
            var data = from hr in db.HolidayRecordList
                       join om in db.Manager on hr.OperateId equals om.ID into t1
                       from omt in t1.DefaultIfEmpty()
                       join tz in db.TimeZone on hr.TimeZoneId equals tz.ID into t2
                       from tzt in t2.DefaultIfEmpty()
                       orderby hr.StartTime descending
                       select new v_HolidayRecordModel()
                       {
                           Id = hr.Id,
                           AddTime = hr.AddTime,
                           StartTime = hr.StartTime,
                           EndTime = hr.EndTime,
                           HolidayName = hr.HolidayName,
                           OperateId = hr.OperateId,
                           OperateName = omt.ManagerName,
                           TimeZone = tzt,
                           TimeZoneId = hr.TimeZoneId,
                           HolidayDays = hr.HolidayDays
                       };
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取假期记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HolidayRecordModel GetModel(int id)
        {
            return db.HolidayRecordList.FirstOrDefault(d => d.Id == id);
        }
        /// <summary>
        /// 获取假期记录集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<HolidayRecordModel> GetList()
        {
            return db.HolidayRecordList.AsQueryable();
        }

        /// <summary>
        /// 添加假期记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="holidayName">假期名称</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="holidayDays">假期天数</param>
        /// <param name="timeZoneId">所处时区</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns></returns>
        public Tuple<int, string> AddModel(int id, string holidayName, DateTime startTime, int holidayDays, int timeZoneId, int operateId)
        {
            HolidayRecordModel model = GetModel(id) ?? new HolidayRecordModel();
            TimeZoneModel timeZone = new TimeZoneDAL().GetModel(timeZoneId);
            if (timeZone == null)
                return new Tuple<int, string>(1, "所处时区不存在");
            model.TimeZoneId = timeZoneId;
            if (model.Id != id)
                return new Tuple<int, string>(1, "假期记录不存在");
            if (model.Id > 0 && model.StartTime <= DateTime.UtcNow)
                return new Tuple<int, string>(1, "该假期已开始，不能再修改");
            model.HolidayName = holidayName;
            model.HolidayDays = holidayDays;
            model.StartTime = startTime.Date.ConvertUtcTime(timeZone.TimeZoneInfoId);
            model.EndTime = model.StartTime.AddDays(holidayDays);
            model.OperateId = operateId;
            if (model.StartTime <= DateTime.UtcNow)
                return new Tuple<int, string>(1, "假期开始时间不能小于当前时间");
            if (model.StartTime > model.EndTime)
                return new Tuple<int, string>(1, "假期开始时间不能小于结束时间");
            if (model.HolidayDays <= 0)
                return new Tuple<int, string>(1, "假期天数至少一天以上");

            ManagerModel operate = new ManagerDAL().GetMod(model.OperateId);
            if (operate == null)
                return new Tuple<int, string>(1, "操作员信息不存在");
            if (holidayName.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "请输入假期名称");
            //判断假期开始时间和结束时间有没有在已有假期记录中
            if (GetList().Count(d => d.Id != model.Id && (model.StartTime > d.StartTime && model.StartTime < d.EndTime || model.EndTime > d.StartTime && model.EndTime < d.EndTime)) > 0)
                return new Tuple<int, string>(1, "假期时间与其他已有假期时间有重叠");
            if (model.Id > 0)
            {
                db.Entry(model).State = EntityState.Modified;
            }
            else
            {
                model.AddTime = DateTime.UtcNow;
                db.Entry(model).State = EntityState.Added;
            }
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog((id > 0 ? "修改" : "添加") + "假期记录", "ID：" + id + ",假期名称：" + holidayName + ",开始时间:" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + ",假期天数：" + holidayDays, ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, (id > 0 ? "修改" : "添加") + "假期记录成功");
        }

        /// <summary>
        /// 删除假期安排记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public Tuple<int, string> DeleteModel(int id, int operateId)
        {
            var model = GetModel(id);
            if (model == null)
                return new Tuple<int, string>(1, "假期安排记录不存在或已删除");
            if (model.StartTime < DateTime.UtcNow)
                return new Tuple<int, string>(1, "该假期已经开始时间，不能删除");
            db.Entry(model).State = EntityState.Deleted;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("删除假期记录", "ID：" + id + ",假期名称：" + model.HolidayName + ",开始时间:" + model.StartTime.ToString("yyyy-MM-dd HH:mm:ss") + ",结束时间：" + model.EndTime.ToString("yyyy-MM-dd HH:mm:ss"), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "删除假期记录成功");
        }
    }
}
