///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-10 16:10:15
//CreateBy 		唐翔
//Content       接口返回数据实体类
//////////////////////////////////////////////////////////////////
using System.ComponentModel;

namespace Model.BaseModels
{
    /// <summary>
    /// 返回数据实体
    /// </summary>
    public class ResultDataModel
    {
        /// <summary>
        /// 返回数据实体
        /// </summary>
        public ResultDataModel() { }
        /// <summary>
        /// 返回数据实体
        /// </summary>
        /// <param name="code">状态码 成功为0 跳转为-1(data存放跳转的地址)</param>
        public ResultDataModel(int code) { this.code = code; }
        /// <summary>
        /// 返回数据实体
        /// </summary>
        /// <param name="code">状态码 成功为0 跳转为-1(data存放跳转的地址)</param>
        /// <param name="msg">状态描述</param>
        public ResultDataModel(int code, string msg) { this.code = code; this.msg = msg; }
        /// <summary>
        /// 返回数据实体
        /// </summary>
        /// <param name="code">状态码 成功为0 跳转为-1(data存放跳转的地址)</param>
        /// <param name="msg">状态描述</param>
        /// <param name="count">数据条数</param>
        public ResultDataModel(int code, string msg, int count) { this.code = code; this.msg = msg; this.count = count; }
        /// <summary>
        /// 返回数据实体
        /// </summary>
        /// <param name="code">状态码 成功为0 跳转为-1(data存放跳转的地址)</param>
        /// <param name="msg">状态描述</param>
        /// <param name="count">数据条数</param>
        /// <param name="data">数据</param>
        public ResultDataModel(int code, string msg, int count, object data)
        {
            this.code = code;
            this.msg = msg;
            this.count = count;
            //if()
            //if(data.GetType().GenericTypeArguments.Length>0)
            //{
            //    string modelName = data.GetType().GenericTypeArguments[0].FullName;
            //    switch (modelName)
            //    {
            //        case ""
            //    }
            //}
            //    data.GetType().GenericTypeArguments[0].FullName
            this.data = data;
        }
        /// <summary>
        /// 状态码 成功为0 跳转为-1(data存放跳转的地址)
        /// </summary>
        [DefaultValue(1)]
        public int code { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        [DefaultValue("")]
        public string msg { get; set; }
        /// <summary>
        /// 数据条数
        /// </summary>
        [DefaultValue(0)]
        public int count { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object data { get; set; }
    }
}