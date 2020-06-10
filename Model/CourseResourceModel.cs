///////////////////////////////////////////////////////////////////
//CreateTime	2018年7月16日17:57:19
//CreateBy 		唐
//Content       课程资源实体类
//////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Common;
using Common.Extend;
using Newtonsoft.Json;

namespace Model
{
    /// <summary>
    /// 课程资源实体类
    /// </summary>
    //[JsonObject(MemberSerialization.OptIn)]
    public class CourseResourceModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public int? ExtendTypeId { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public string ExtendTypeName => ExtendType?.Name ?? "";
        /// <summary>
        /// 有效期，为空表示用久有效
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? EffectiveTime { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownCount { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [JsonIgnoreByAreaName(Keys.AreaNameEnum.User)]
        public int Sort { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [JsonIgnoreByAreaName(Keys.AreaNameEnum.User, Keys.AreaNameEnum.Manage)]
        public int OperateId { get; set; }
        /// <summary>
        /// 操作人名称
        /// </summary>
        [JsonIgnoreByAreaName(Keys.AreaNameEnum.User)]
        public string OperateManageName => OperateManager?.ManagerName ?? "";
        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]//不参与json序列化
        [ForeignKey("ExtendTypeId")]
        public virtual ExtendTypeModel ExtendType { get; set; }
        /// <summary>
        /// 操作管理员
        /// </summary>
        [JsonIgnore]//不参与json序列化
        [ForeignKey("OperateId")]
        public virtual ManagerModel OperateManager { get; set; }

    }
}
