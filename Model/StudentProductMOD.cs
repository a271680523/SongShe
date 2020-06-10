using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    /// <summary>
    /// 学生产品表
    /// </summary>
    public class StudentProductMOD
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 频率
        /// <para>值为0时表示不限制</para>
        /// </summary>
        public int Frequency { get; set; }
        /// <summary>
        /// 学籍时长
        /// </summary>
        public int LimitDate { get; set; }
        /// <summary>
        /// 总课时
        /// </summary>
        public int TotalCourseCount { get; set; }
        /// <summary>
        /// 可休学时长(天)
        /// </summary>
        public int LeaveCount { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal PriceMoney { get; set; }
        /// <summary>
        /// 产品状态
        /// </summary>
        public int ProductStatus { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 剩余课时
        /// </summary>
        public int RestOfCourseCount { get; set; }
        /// <summary>
        /// 已休学时长(天)
        /// </summary>
        public int UseLeaveCount { get; set; }
        /// <summary>
        /// 是否分期付款
        /// </summary>
        public bool IsInstallment { get; set; }
        /// <summary>
        /// 未结清金额
        /// </summary>
        public decimal UnliquidatedMoney { get; set; }
        /// <summary>
        /// 显示到期提示的开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime StartRechargePromptTime { get; set; }
        /// <summary>
        /// 显示到期提示的结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EndRechargePromptTime { get; set; }
        /// <summary>
        /// 退学备注
        /// </summary>
        public string CancelProductRemark { get; set; }
    }
    /// <summary>
    /// 分期信息实体类
    /// </summary>
    public class InstallmentPayInfoModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 学生产品编号
        /// </summary>
        public int StudentProductId { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal PayMoney { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime PayDate { get; set; }
        /// <summary>
        /// 选课时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime SelectCourseDate { get; set; }
        /// <summary>
        /// 支付状态 0未支付 1已支付 2已取消
        /// </summary>
        public int PayStatus { get; set; }
    }

    /// <summary>
    /// 学生产品扩展显示视图
    /// </summary>
    public class v_StudentProductMOD
    {
        #region 原表字段
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 频率
        /// <para>值为0时表示不限制</para>
        /// </summary>
        public int Frequency { get; set; }
        /// <summary>
        /// 学籍时长
        /// </summary>
        public int LimitDate { get; set; }
        /// <summary>
        /// 总课时
        /// </summary>
        public int TotalCourseCount { get; set; }
        /// <summary>
        /// 可休学时长
        /// </summary>
        public int LeaveCount { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public decimal PriceMoney { get; set; }
        /// <summary>
        /// 产品状态
        /// </summary>
        public int ProductStatus { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 剩余课时
        /// </summary>
        public int RestOfCourseCount { get; set; }
        /// <summary>
        /// 已休学时长
        /// </summary>
        public int UseLeaveCount { get; set; }
        /// <summary>
        /// 是否分期付款
        /// </summary>
        public bool IsInstallment { get; set; }
        /// <summary>
        /// 未结清金额
        /// </summary>
        public decimal UnliquidatedMoney { get; set; }
        /// <summary>
        /// 显示到期提示的开始时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime StartRechargePromptTime { get; set; }
        /// <summary>
        /// 显示到期提示的结束时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime EndRechargePromptTime { get; set; }
        /// <summary>
        /// 退学备注
        /// </summary>
        public string CancelProductRemark { get; set; }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 分期信息集合
        /// </summary>
        public List<InstallmentPayInfoModel> InstallmentList { get; set; }
        /// <summary>
        /// 产品状态
        /// </summary>
        public string ProductStatusName => ProductStatus == 0 ? "使用中" : ProductStatus == 1 ? "未使用" : ProductStatus == 2 ? "已结束" : ProductStatus == 3 ? "已退学" : "";
        /// <summary>
        /// 下一次支付信息
        /// </summary>
        public InstallmentPayInfoModel NextInstallment
        {
            get
            {
                if (InstallmentList != null)
                    return InstallmentList.OrderBy(d => d.PayDate).FirstOrDefault(d => d.PayStatus == 0);
                else
                    return null;
            }
        }// InstallmentList != null && InstallmentList.Count() > 0 ? InstallmentList.OrderBy(d => d.PayDate).FirstOrDefault(d => d.PayStatus == 0) : null;
        /// <summary>
        /// 休学记录
        /// </summary>
        public IQueryable<StudentSuspendSchoolingRecordMOD> SuspendSchoolingRecordList { get; set; }
        #endregion
    }
}