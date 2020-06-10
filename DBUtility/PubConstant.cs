using System;
using System.Configuration;

namespace DBUtility
{

    public class PubConstant
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["OnlineEduContext"]?.ConnectionString;
                string conStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
                if (conStringEncrypt == "true")
                {
                    connectionString = DESEncrypt.Decrypt(connectionString);
                }
                return connectionString;
            }
        }

        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["OnlineEduContext"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(configName))
                connectionString = ConfigurationManager.AppSettings[configName];
            if (string.IsNullOrWhiteSpace(configName))
                connectionString = configName;
            string conStringEncrypt = ConfigurationManager.AppSettings["ConStringEncrypt"];
            if (conStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }


    }
}
