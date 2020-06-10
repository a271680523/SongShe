using System;
using System.Runtime.Caching;

namespace Common
{
    /// <summary>
    /// 内存缓存辅助类
    /// </summary>
    public class MemoryCacheHelper
    {

        #region 设置当前应用程序指定CacheKey的Cache值
        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static bool SetCache(string cacheKey, object objObject)
        { 
            CacheItemPolicy oPolicy = new CacheItemPolicy();
            // oPolicy.SlidingExpiration = TimeSpan.Zero;
            return MemoryCache.Default.Add(cacheKey, objObject, oPolicy);
        }
        #endregion

        #region 设置当前应用程序指定CacheKey的Cache值
        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="timeSpan">过期时间，以秒钟为单位</param>
        public static bool SetCache(string cacheKey, object objObject, int timeSpan)
        {
            CacheItemPolicy oPolicy = new CacheItemPolicy {AbsoluteExpiration = DateTime.Now.AddSeconds(timeSpan)};
            return MemoryCache.Default.Add(cacheKey, objObject, oPolicy);
        }
        #endregion
        #region 设置当前应用程序指定CacheKey的Cache值
        /// <summary>
        /// 清除当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        public static bool ClearCache(string cacheKey)
        {
            CacheItemPolicy oPolicy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddSeconds(-1) };
            return MemoryCache.Default.Add(cacheKey, "", oPolicy);
        }
        #endregion

        #region 获取当前应用程序指定CacheKey的Cache值
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string cacheKey)=> MemoryCache.Default.Get(cacheKey);
        public static void Remove(string strCacheName)
        {
            MemoryCache memoryCache = MemoryCache.Default;
            if (memoryCache.Contains(strCacheName))
                memoryCache.Remove(strCacheName);
        }
        #endregion
    }
}
