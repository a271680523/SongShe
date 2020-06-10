using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /// <summary>
    /// 学生实体类
    /// </summary>
    public class StudentMOD
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 所处时区
        /// </summary>
        public int TimeZone { get; set; }
        /// <summary>
        /// 主管老师
        /// </summary>
        public int Supervisor { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        public string ChinaName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 住宅电话
        /// </summary>
        public string HomePhone { get; set; }
        /// <summary>
        /// 工作电话
        /// </summary>
        public string WorkPhone { get; set; }
        /// <summary>
        /// 家长电话
        /// </summary>
        public string FamilyPhone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 工作邮箱
        /// </summary>
        public string WorkEmail { get; set; }
        /// <summary>
        /// Skype
        /// </summary>
        public string Skype { get; set; }
        /// <summary>
        /// 备用邮箱
        /// </summary>
        public string BackupEmail { get; set; }
        /// <summary>
        /// 家长邮箱
        /// </summary>
        public string FamilyEmail { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string Wechat { get; set; }

        /// <summary>
        /// 住宅电话(学生端)
        /// </summary>
        public string s_HomePhone { get; set; }
        /// <summary>
        /// 工作电话(学生端)
        /// </summary>
        public string s_WorkPhone { get; set; }
        /// <summary>
        /// 家长电话(学生端)
        /// </summary>
        public string s_FamilyPhone { get; set; }
        /// <summary>
        /// 工作邮箱(学生端)
        /// </summary>
        public string s_WorkEmail { get; set; }
        /// <summary>
        /// Skype(学生端)
        /// </summary>
        public string s_Skype { get; set; }
        /// <summary>
        /// 备用邮箱(学生端)
        /// </summary>
        public string s_BackupEmail { get; set; }
        /// <summary>
        /// 家长邮箱(学生端)
        /// </summary>
        public string s_FamilyEmail { get; set; }
        /// <summary>
        /// 微信号(学生端)
        /// </summary>
        public string s_Wechat { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Six { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 是否少儿
        /// </summary>
        public bool IsNonage { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 母语
        /// </summary>
        public string NativeLanguage { get; set; }
        /// <summary>
        /// 是否来过中国
        /// </summary>
        public bool IsToChina { get; set; }
        /// <summary>
        /// 家长姓名
        /// </summary>
        public string FamilyName { get; set; }
        /// <summary>
        /// 学生性格
        /// </summary>
        public string Character { get; set; }
        /// <summary>
        /// 学习态度
        /// </summary>
        public string LearningAttitude { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        public string Hobbies { get; set; }
        /// <summary>
        /// 禁忌话题
        /// </summary>
        public string TabooTopic { get; set; }
        /// <summary>
        /// 家长关系
        /// </summary>
        public string FamilyRelationship { get; set; }
        /// <summary>
        /// 学生背景
        /// </summary>
        public string Vitae { get; set; }
        /// <summary>
        /// 学习需求
        /// </summary>
        public string LearningNeeds { get; set; }
        /// <summary>
        /// 学习内容
        /// </summary>
        public string LearningContent { get; set; }
        /// <summary>
        /// 学习方法
        /// </summary>
        public string LearningMethod { get; set; }
        /// <summary>
        /// 注册者管理员ID
        /// </summary>
        public int ManagerID { get; set; }
        /// <summary>
        /// 是否免费试读
        /// </summary>
        public bool IsFreeProbation { get; set; }
        /// <summary>
        /// 免费试读次数
        /// </summary>
        public int FreeProbationCount { get; set; }
        /// <summary>
        /// 学生助理
        /// </summary>
        public int SellerManager { get; set; }
        /// <summary>
        /// 课程顾问(销售员)
        /// </summary>
        public int CourseConsultant { get; set; }
        /// <summary>
        /// 学习能力
        /// </summary>
        public string LearningAbility { get; set; }
        /// <summary>
        /// 对代课要求
        /// </summary>
        public string CourseManagerTheDemand { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string CourseManagerRemark { get; set; }
        /// <summary>
        /// 最后一次修改管理员ID
        /// </summary>
        public int LastEditManagerID { get; set; }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime? LastEditTime { get; set; }
        /// <summary>
        /// 推荐者ID
        /// </summary>
        public int RecommendStudentId { get; set; }
        /// <summary>
        /// 课程顾问主管(销售主管)
        /// </summary>
        public int CourseConsultantSupervisorId { get; set; }
        /// <summary>
        /// Ft订课邮件内容
        /// </summary>
        public string FtEmailContent { get; set; }
        /// <summary>
        /// Ft汉语水平
        /// </summary>
        public string FtLanguageLevel { get; set; }
        /// <summary>
        /// Ft学习需求
        /// </summary>
        public string FtLearningNeeds { get; set; }
        /// <summary>
        /// Ft修改人
        /// </summary>
        public int FtEditManagerId { get; set; }
        /// <summary>
        /// Ft修改时间
        /// </summary>
        public DateTime? FtEditTime { get; set; }


    }
    /// <summary>
    /// 学生视图实体类
    /// </summary>
    public class v_StudentMOD
    {
        #region 原表字段
        /// <summary>
        /// 主键ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 所处时区
        /// </summary>
        public int TimeZone { get; set; }
        /// <summary>
        /// 主管老师
        /// </summary>
        public int Supervisor { get; set; }
        /// <summary>
        /// 中文名
        /// </summary>
        public string ChinaName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 住宅电话
        /// </summary>
        public string HomePhone { get; set; }
        /// <summary>
        /// 工作电话
        /// </summary>
        public string WorkPhone { get; set; }
        /// <summary>
        /// 家长电话
        /// </summary>
        public string FamilyPhone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 工作邮箱
        /// </summary>
        public string WorkEmail { get; set; }
        /// <summary>
        /// Skype
        /// </summary>
        public string Skype { get; set; }
        /// <summary>
        /// 备用邮箱
        /// </summary>
        public string BackupEmail { get; set; }
        /// <summary>
        /// 家长邮箱
        /// </summary>
        public string FamilyEmail { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string Wechat { get; set; }
        /// <summary>
        /// 住宅电话(学生端)
        /// </summary>
        public string s_HomePhone { get; set; }
        /// <summary>
        /// 工作电话(学生端)
        /// </summary>
        public string s_WorkPhone { get; set; }
        /// <summary>
        /// 家长电话(学生端)
        /// </summary>
        public string s_FamilyPhone { get; set; }
        /// <summary>
        /// 工作邮箱(学生端)
        /// </summary>
        public string s_WorkEmail { get; set; }
        /// <summary>
        /// Skype(学生端)
        /// </summary>
        public string s_Skype { get; set; }
        /// <summary>
        /// 备用邮箱(学生端)
        /// </summary>
        public string s_BackupEmail { get; set; }
        /// <summary>
        /// 家长邮箱(学生端)
        /// </summary>
        public string s_FamilyEmail { get; set; }
        /// <summary>
        /// 微信号(学生端)
        /// </summary>
        public string s_Wechat { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Six { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 是否少儿
        /// </summary>
        public bool IsNonage { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 母语
        /// </summary>
        public string NativeLanguage { get; set; }
        /// <summary>
        /// 是否来过中国
        /// </summary>
        public bool IsToChina { get; set; }
        /// <summary>
        /// 家长姓名
        /// </summary>
        public string FamilyName { get; set; }
        /// <summary>
        /// 学生性格
        /// </summary>
        public string Character { get; set; }
        /// <summary>
        /// 学习态度
        /// </summary>
        public string LearningAttitude { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        public string Hobbies { get; set; }
        /// <summary>
        /// 禁忌话题
        /// </summary>
        public string TabooTopic { get; set; }
        /// <summary>
        /// 家长关系
        /// </summary>
        public string FamilyRelationship { get; set; }
        /// <summary>
        /// 学生背景
        /// </summary>
        public string Vitae { get; set; }
        /// <summary>
        /// 学习需求
        /// </summary>
        public string LearningNeeds { get; set; }
        /// <summary>
        /// 学习内容
        /// </summary>
        public string LearningContent { get; set; }
        /// <summary>
        /// 学习方法
        /// </summary>
        public string LearningMethod { get; set; }
        /// <summary>
        /// 注册者管理员ID
        /// </summary>
        public int ManagerID { get; set; }
        /// <summary>
        /// 是否免费试读
        /// </summary>
        public bool IsFreeProbation { get; set; }
        /// <summary>
        /// 免费试读次数
        /// </summary>
        public int FreeProbationCount { get; set; }
        /// <summary>
        /// 学生助理
        /// </summary>
        public int SellerManager { get; set; }
        /// <summary>
        /// 课程顾问(销售员)
        /// </summary>
        public int CourseConsultant { get; set; }
        /// <summary>
        /// 学习能力
        /// </summary>
        public string LearningAbility { get; set; }
        /// <summary>
        /// 对代课要求
        /// </summary>
        public string CourseManagerTheDemand { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string CourseManagerRemark { get; set; }
        /// <summary>
        /// 最后一次修改管理员ID
        /// </summary>
        public int LastEditManagerID { get; set; }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime? LastEditTime { get; set; }
        /// <summary>
        /// 推荐者ID
        /// </summary>
        public int RecommendStudentId { get; set; }
        /// <summary>
        /// 课程顾问主管(销售主管)
        /// </summary>
        public int CourseConsultantSupervisorId { get; set; }
        /// <summary>
        /// Ft订课邮件内容
        /// </summary>
        public string FtEmailContent { get; set; }
        /// <summary>
        /// Ft汉语水平
        /// </summary>
        public string FtLanguageLevel { get; set; }
        /// <summary>
        /// Ft学习需求
        /// </summary>
        public string FtLearningNeeds { get; set; }
        /// <summary>
        /// Ft修改人
        /// </summary>
        public int FtEditManagerId { get; set; }
        /// <summary>
        /// Ft修改时间
        /// </summary>
        public DateTime? FtEditTime { get; set; }
        #endregion


        #region 扩展显示字段
        /// <summary>
        /// 添加学生的管理员姓名
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 主管老师姓名
        /// </summary>
        public string SupervisorName { get; set; }
        /// <summary>
        /// 学生助理姓名
        /// </summary>
        public string SellerManagerName { get; set; }
        /// <summary>
        /// 课程顾问姓名
        /// </summary>
        public string CourseConsultantName { get; set; }
        /// <summary>
        /// 最近正在读的产品信息
        /// </summary>
        public StudentProductModel Producting { get; set; }
        /// <summary>
        /// 分期产品数量
        /// </summary>
        public int InstallmentProductCount { get; set; }
        /// <summary>
        /// 最后一次上课时间
        /// </summary>
        [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
        public DateTime? LastCourseTime { get; set; }
        /// <summary>
        /// 休学记录
        /// </summary>
        public StudentSuspendSchoolingRecordMOD SuspendSchooling { get; set; }
        /// <summary>
        /// 最近正在读的产品信息实体
        /// </summary>
        public class StudentProductModel
        {
            /// <summary>
            /// 学生产品ID
            /// </summary>
            public int ID { get; set; }
            /// <summary>
            /// 学生产品状态
            /// </summary>
            public int ProductStatus { get; set; }
            /// <summary>
            /// 学生产品名称
            /// </summary>
            public string ProductName { get; set; }
            /// <summary>
            /// 学生产品结束时间
            /// </summary>
            [Newtonsoft.Json.JsonConverter(typeof(Common.Extend.DateTimeJsonConverter))]
            public DateTime EndTime { get; set; }
            /// <summary>
            /// 学籍时长
            /// </summary>
            public int LimitDate { get; set; }
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
        }
        /// <summary>
        /// 时区信息
        /// </summary>
        public TimeZoneModel timeZone { get; set; }
        /// <summary>
        /// 最后修改人姓名
        /// </summary>
        public string LastEditManagerName { get; set; }
        /// <summary>
        /// 推荐者(学生)账号
        /// </summary>
        public string RecommendStudentLoginName { get; set; }
        /// <summary>
        /// 课程顾问主管姓名(销售主管姓名)
        /// </summary>
        public string CourseConsultantSupervisorManagerName { get; set; }
        /// <summary>
        /// Ft修改人姓名
        /// </summary>
        public string FtEditManagerName { get; set; }
        #endregion
    }
}