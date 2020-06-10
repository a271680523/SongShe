using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace DAL
{
    public class BulletinDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取公告集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<BulletinModel> GetList()
        {
            var data = from b in db.BulletinList
                       orderby b.Sort, b.EndTime descending
                       select b;
            return data.AsQueryable();
        }

        /// <summary>
        /// 获取公告视图集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_BulletinModel> GetList_v()
        {
            var data = from b in db.BulletinList
                       join om in db.Manager on b.OperateId equals om.ID into t1
                       from omt in t1.DefaultIfEmpty()
                       orderby b.Sort, b.EndTime descending
                       select new v_BulletinModel()
                       {
                           Id = b.Id,
                           AddTime = b.AddTime,
                           EditTime = b.EditTime,
                           Title = b.Title,
                           Content = b.Content,
                           StartTime = b.StartTime,
                           EndTime = b.EndTime,
                           Sort = b.Sort,
                           OperateId = b.OperateId,
                           OperateName = omt.ManagerName,
                           Status = b.Status,
                           StatusName = b.Status == 0 ? "不显示" : b.Status == 1 ? "显示" : ""
                       };
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取公告信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public BulletinModel GetModel(int id)
        {
            return db.BulletinList.FirstOrDefault(d => d.Id == id);
        }
        /// <summary>
        /// 获取公告视图信息
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public v_BulletinModel GetModel_v(int id)
        {
            return GetList_v().FirstOrDefault(d => d.Id.Equals(id));
        }

        /// <summary>
        /// 添加或修改公告
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="status">状态</param>
        /// <param name="sort">优先值</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns></returns>
        public Tuple<int, string> AeModel(int id, string title, string content, DateTime startTime, DateTime endTime,
            int status, int sort, int operateId)
        {
            BulletinModel model = new BulletinModel();
            if (id > 0)
            {
                model = GetModel(id);
                if (model == null)
                    return new Tuple<int, string>(1, "公告信息不存在");
            }
            if (content.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "公告内容不能为空");
            if (startTime > endTime)
                return new Tuple<int, string>(1, "公告展示结束时间不能小于开始时间");
            if (status != 0 && status != 1)
                return new Tuple<int, string>(1, "公告状态值异常");
            v_ManagerMOD operateManager = new ManagerDAL().GetMOD_v(operateId);
            if (operateManager == null)
                return new Tuple<int, string>(1, "当前操作员信息不存在");
            model.Title = title;
            model.Content = content;
            model.StartTime = startTime.ConvertUtcTime(operateManager.timeZone.TimeZoneInfoId);
            model.EndTime = endTime.ConvertUtcTime(operateManager.timeZone.TimeZoneInfoId);
            model.Status = status;
            model.Sort = sort;
            model.OperateId = operateId;
            if (model.Id > 0)
            {
                model.EditTime = DateTime.UtcNow;
                db.Entry(model).State = EntityState.Modified;
            }
            else
            {
                model.AddTime = DateTime.UtcNow;
                model.EditTime = model.AddTime;
                db.Entry(model).State = EntityState.Added;
            }
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog((id > 0 ? "修改" : "添加") + "公告", "编号:" + id + ",标题:" + title + ",内容:" + content + ",开始时间:" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + ",结束时间:" + endTime.ToString("yyyy-MM-dd HH:mm:ss") + ",状态值:" + status + "(" + (status == 1 ? "显示" : "不显示") + "),操作员:" + operateId + "(" + operateManager.ManagerName + ")", ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, (id > 0 ? "修改" : "添加") + "公告信息成功");
        }
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="operateId">操作员</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteModel(int id, int operateId)
        {
            BulletinModel model = GetModel(id);
            if (model == null)
                return new Tuple<int, string>(1, "公告不存在或已删除");
            v_ManagerMOD operateManager = new ManagerDAL().GetMOD_v(operateId);
            if (operateManager == null)
                return new Tuple<int, string>(1, "当前操作员信息不存在");
            db.Entry(model).State = EntityState.Deleted;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("删除公告", "编号:" + id + ",操作员:" + operateId + "(" + operateManager.ManagerName + ")", ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "公告删除成功");
        }
    }
}
