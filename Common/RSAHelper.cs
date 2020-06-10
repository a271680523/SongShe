using System;
using System.IO;
using System.Security.Cryptography;

namespace Common
{
    /// <summary>
    /// 加密加签类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class RSAHelper
    {
        private const string EncryptKey = "EncryKey";
        #region 加密字符串
        /// <summary> 
        /// 加密字符串   
        /// </summary>  
        /// <param name="str">要加密的字符串</param>  
        /// <returns>加密后的字符串</returns>  
        public static string Encrypt(string str)
        {
            try
            {
                DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();//实例化加/解密类对象   
                byte[] key = Keys.DefaultEncoding.GetBytes(EncryptKey);//定义字节数组，用来存储密钥    
                byte[] data = Keys.DefaultEncoding.GetBytes(str);//定义字节数组，用来存储要加密的字符串  
                MemoryStream mStream = new MemoryStream();//实例化内存流对象      
                                                          //使用内存流实例化加密流对象   
                CryptoStream cStream = new CryptoStream(mStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);
                cStream.Write(data, 0, data.Length);//向加密流中写入数据      
                cStream.FlushFinalBlock();//释放加密流      
                return Convert.ToBase64String(mStream.ToArray());//返回加密后的字符串  
            }
            catch
            {
                throw new Exception("数据加密失败");
            }
        }
        #endregion

        #region 解密字符串
        /// <summary>  
        /// 解密字符串   
        /// </summary>  
        /// <param name="str">要解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        public static string Decrypt(string str)
        {
            try
            {
                DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();//实例化加/解密类对象    
                byte[] key = Keys.DefaultEncoding.GetBytes(EncryptKey); //定义字节数组，用来存储密钥    
                byte[] data = Convert.FromBase64String(str);//定义字节数组，用来存储要解密的字符串  
                MemoryStream mStream = new MemoryStream();//实例化内存流对象      
                                                          //使用内存流实例化解密流对象       
                CryptoStream cStream = new CryptoStream(mStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);
                cStream.Write(data, 0, data.Length);//向解密流中写入数据     
                cStream.FlushFinalBlock();//释放解密流      
                return Keys.DefaultEncoding.GetString(mStream.ToArray());//返回解密后的字符串  
            }
            catch
            {

                throw new Exception("数据解密失败");
            }
        }
        #endregion 

    }
}