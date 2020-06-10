using Model;
using System;
using System.Linq;

namespace DAL
{
    // ReSharper disable once InconsistentNaming
    public class ManagerLogDAL : Base.BaseDAL
    {
        private readonly static OnlineEduContext _db = new OnlineEduContext();
        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="logType">日子类型</param>
        /// <param name="managerId">操作管理员</param>
        public static ManagerLogModel AddManagerLog(string title, string content, ManagerLogType logType, int managerId = 0)
        {
            try
            {
                ManagerLogModel log = new ManagerLogModel
                {
                    AddTime = DateTime.UtcNow,
                    Title = title,
                    LogType = logType,
                    Content = content,
                    ManagerID = managerId
                };
                log = _db.ManagerLog.Add(log);
                _db.SaveChangesAsync();
                return log;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取日志视图集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_ManagerLogModel> GetList_v()
        {
            var data = from ml in db.ManagerLog
                       join m in db.Manager
                       on ml.ManagerID equals m.ID into t1
                       from mml in t1.DefaultIfEmpty()
                       select new v_ManagerLogModel()
                       {
                           ID = ml.ID,
                           LogTypeName = ml.LogType.ToString(),
                           ManagerID = ml.ManagerID,
                           Title = ml.Title,
                           Content = ml.Content,
                           AddTime = ml.AddTime,
                           ManagerName = mml.ManagerName
                       };
            return data.AsQueryable();
        }
    }
}
