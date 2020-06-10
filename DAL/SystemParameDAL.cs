using Model;
using System;
using System.Linq;

namespace DAL
{
    // ReSharper disable once InconsistentNaming
    public class SystemParameDAL : Base.BaseDAL
    {
        private readonly static OnlineEduContext _db = new OnlineEduContext();
        /// <summary>
        /// 获取系统参数
        /// </summary>
        /// <returns></returns>
        public static SystemParameMOD GetSystemParame(int id)
        {
            SystemParameMOD systemParame = _db.SystemParame.FirstOrDefault(d => d.ID == id);
            return systemParame;
        }
        /// <summary>
        /// 获取系统参数值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int GetSystemParameValue(int id)
        {
            var data = GetSystemParame(id);
            return data?.Value ?? 0;
        }
        /// <summary>
        /// 获取系统参数值(非整数值)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetSystemParameStrValue(int id)
        {
            var data = GetSystemParame(id);
            return data?.StrValue ?? "";
        }
        /// <summary>
        /// 获取系统参数集合
        /// </summary>
        /// <returns></returns>
        public static IQueryable<SystemParameMOD> GetSystemParameList()
        {
            var data = from sp in _db.SystemParame
                       orderby sp.ID
                       select sp;
            return data;
        }

        /// <summary>
        /// 修改系统参数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="managerId"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static Tuple<int, string> AeSystemParame(int id, int value, int managerId, string strValue)
        {
            SystemParameMOD systemParame = _db.SystemParame.FirstOrDefault(d => d.ID == id);
            if (systemParame == null)
                return new Tuple<int, string>(1, "系统参数不存在");
            systemParame.Value = value;
            systemParame.StrValue = strValue;
            systemParame.AddTime = DateTime.UtcNow;
            _db.Entry(systemParame).State = System.Data.Entity.EntityState.Modified;
            _db.SaveChanges();
            ManagerLogDAL.AddManagerLog("修改系统参数", "参数ID：" + systemParame.ID + ",参数名:" + systemParame.Name + ",参数值:" + systemParame.Value, ManagerLogType.Manager, managerId);
            return new Tuple<int, string>(0, "修改系统参数成功");
        }
    }
}
