using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DBUtility
{
    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class DESEncrypt
    {
        public static readonly Encoding Encoding = Encoding.UTF8;

        #region ========加密======== 

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            return Encrypt(text, "litianping");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string text, string sKey)
        {
            try
            {
                DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();//实例化加/解密类对象   
                byte[] key = Encoding.GetBytes(sKey);//定义字节数组，用来存储密钥    
                byte[] data = Encoding.GetBytes(text);//定义字节数组，用来存储要加密的字符串  
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

        #region ========解密======== 


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decrypt(string text)
        {
            return Decrypt(text, "litianping");
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string text, string sKey)
        {
            try
            {
                DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();//实例化加/解密类对象    
                byte[] key = Encoding.GetBytes(sKey); //定义字节数组，用来存储密钥    
                byte[] data = Convert.FromBase64String(text);//定义字节数组，用来存储要解密的字符串  
                MemoryStream mStream = new MemoryStream();//实例化内存流对象      
                //使用内存流实例化解密流对象       
                CryptoStream cStream = new CryptoStream(mStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);
                cStream.Write(data, 0, data.Length);//向解密流中写入数据     
                cStream.FlushFinalBlock();//释放解密流      
                return Encoding.GetString(mStream.ToArray());//返回解密后的字符串  
            }
            catch
            {

                throw new Exception("数据解密失败");
            }
        }

        #endregion


    }
}
