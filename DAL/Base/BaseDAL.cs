using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base
{
    /// <summary>
    /// 父类数据层
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class BaseDAL
    {
        private readonly object Obj = new object();
        //[ThreadStatic]
        private OnlineEduContext _db;
        // ReSharper disable once InconsistentNaming
        public OnlineEduContext db
        {
            get
            {
                if (_db == null)
                {
                    lock (Obj)//加锁防止多线程同时实例化
                    {
                        _db = new OnlineEduContext();
                    }
                }
                //_db.Configuration.ProxyCreationEnabled = false;
                return _db;
            }
        }

        #region CustomContext


        private static CustomContext _customContext;
        /// <summary>
        /// 全局静态上下文对象
        /// </summary>
        public CustomContext customContext
        {
            get
            {
                if (_customContext == null)
                {
                    lock (Obj)//加锁防止多线程同时实例化
                    {
                        _customContext = new CustomContext();
                    }
                }
                return _customContext;
            }
        }
        #endregion
    }
}
