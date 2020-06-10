using System;
using System.Text;
using Common.Extend;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Keys
    {
        private static string _randenKey = "";
        /// <summary>
        /// 生成的全局静态随机数
        /// </summary>
        public static string RandenKey
        {
            get
            {
                if (_randenKey.IsNullOrWhiteSpace())
                {
                    _randenKey = StaticCommon.Rand.Next(1000, 9999).ToString();
                }
                return _randenKey;
            }
        }

        /// <summary>
        /// 默认编码格式
        /// </summary>
        public static Encoding DefaultEncoding = Encoding.UTF8;
        /// <summary>
        /// 学生状态
        /// </summary>
        public enum StduentStatus
        {
            /// <summary>
            /// 注册未试课
            /// </summary>
            NoFreeProbation = -1,
            /// <summary>
            /// 试课未购买
            /// </summary>
            FreeProbationAndNoProduct = -2,
            /// <summary>
            /// 休学中
            /// </summary>
            SuspendSchooling = -3,
            /// <summary>
            /// 长期不上课
            /// </summary>
            LongTimeNoCourse = -4,
            /// <summary>
            /// 续费期
            /// </summary>
            ProductEnd = -5,
            /// <summary>
            /// 分期未付款
            /// </summary>
            Installmenting = -6,
        }
        /// <summary>
        /// 系统参数对应ID
        /// </summary>
        public class SystemParameId
        {
            /// <summary>
            /// 学生更改或取消课程限制时间小时数
            /// </summary>
            public const int ChangeStudentCourseRecordId = 1;
            /// <summary>
            /// 学生状态长期不上课最低天数
            /// </summary>
            public const int LongTimeNoCourseId = 2;
            /// <summary>
            /// 任课老师上课最大连续节数
            /// </summary>
            public const int ManagerMaxContinsCourseCountId = 3;
            /// <summary>
            /// 学生选课限制时间小时数
            /// </summary>
            public const int AddStudentCourseRecordId = 4;
            /// <summary>
            /// 一个课时的分钟数
            /// </summary>
            public const int OneLessMinutes = 5;
            /// <summary>
            /// 单次休学的天数
            /// </summary>
            public const int SuspendSchoolingDay = 6;
            /// <summary>
            /// 课程主管老师背景颜色
            /// </summary>
            public const int CourseSupervisorBackgroundColorId = 7;
            /// <summary>
            /// 课程老师背景颜色
            /// </summary>
            public const int CourseManagerBackgroundColorId = 8;
            /// <summary>
            /// 课程学生背景颜色
            /// </summary>
            public const int CourseStudentBackgroundColorId = 9;
            /// <summary>
            /// 课程主管老师字体颜色
            /// </summary>
            public const int CourseSupervisorTextColorId = 10;
            /// <summary>
            /// 课程老师字体颜色
            /// </summary>
            public const int CourseManagerTextColorId = 11;
            /// <summary>
            /// 课程学生字体颜色
            /// </summary>
            public const int CourseStudentTextColorId = 12;
            /// <summary>
            /// 课程主管老师边框颜色
            /// </summary>
            public const int CourseSupervisorBorderColorId = 13;
            /// <summary>
            /// 课程老师边框颜色
            /// </summary>
            public const int CourseManagerBorderColorId = 14;
            /// <summary>
            /// 课程学生边框颜色
            /// </summary>
            public const int CourseStudentBorderColorId = 15;
            /// <summary>
            /// 是否自动发布课程
            /// </summary>
            public const int IsAutoAddCourseRecord = 16;
            /// <summary>
            /// 开始到期提示的天数百分比(产品时长的百分比)，距离结束时间
            /// </summary>
            public const int ProductStartRechargePromptDayPercentage = 17;
            /// <summary>
            /// 学生产品结束到期提示的天数，超过结束时间
            /// </summary>
            public const int ProductEndRechargePromptDay = 18;
            /// <summary>
            /// 重置密码有效分钟数
            /// </summary>
            public const int ResetPwdEffectiveMinutes = 19;
            /// <summary>
            /// 发送重置密码邮件账号ID
            /// </summary>
            public const int SendResetPwdEmailAccountId = 20;
        }
        /// <summary>
        /// 区域名称枚举
        /// </summary>
        public enum AreaNameEnum
        {
            /// <summary>
            /// 无区域名称
            /// </summary>
            NullArea = 0,
            /// <summary>
            /// 用户User区域
            /// </summary>
            User = 1,
            /// <summary>
            /// 管理员Manage区域
            /// </summary>
            Manage = 2,
            /// <summary>
            /// Api接口区域
            /// </summary>
            Api = 0,
        }

        /// <summary>
        /// 学生产品状态
        /// </summary>
        public enum StudentProductStatus
        {
            /// <summary>
            /// 未使用
            /// </summary>
            NoUsed = 1,
            /// <summary>
            /// 使用中
            /// </summary>
            Using = 0,
            /// <summary>
            /// 使用完
            /// </summary>
            UseUp = 2,
            /// <summary>
            /// 已退学
            /// </summary>
            Cancel = 3
        }
        /// <summary>
        /// 申请免费试读课程状态
        /// </summary>
        public enum VerifyStatus
        {
            /// <summary>
            /// 申请中
            /// </summary>
            Verifying = 0,
            /// <summary>
            /// 申请成功
            /// </summary>
            Success = 1,
            /// <summary>
            /// 申请失败
            /// </summary>
            Fail = 2
        }
        /// <summary>
        /// 处理状态
        /// </summary>
        public enum HandleStatus
        {
            /// <summary>
            /// 处理中
            /// </summary>
            [LanguageName("处理中", "Handling")]
            Handling = 0,
            /// <summary>
            /// 处理成功
            /// </summary>
            [LanguageName("处理成功", "Success")]
            Success = 1,
            /// <summary>
            /// 处理失败
            /// </summary>
            [LanguageName("处理失败", "Fail")]
            Fail = 2,
            /// <summary>
            /// 已取消
            /// </summary>
            [LanguageName("已取消", "Cancel")]
            Cancel = 3
        }
        /// <summary>
        /// 自定义语言类型
        /// </summary>
        public enum LanguageType
        {
            ChinaName = 1,
            EnglishName = 2
        }

        /// <summary>
        /// 学生课程类型
        /// </summary>
        public enum StudentCourseType
        {
            /// <summary>
            /// 未知类型
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// 试读预约课
            /// </summary>
            FtUseWill = 1,
            /// <summary>
            /// 试读已结束
            /// </summary>
            FtUsed = 2,
            /// <summary>
            /// 主管预约课
            /// </summary>
            SupervisorUseWill = 3,
            /// <summary>
            /// 主管课程已结束
            /// </summary>
            SupervisorUsed = 5,
            /// <summary>
            /// 代课预约课
            /// </summary>
            OtherUseWill = 4,
            /// <summary>
            /// 代课课程已结束
            /// </summary>
            OtherUsed = 6
        }
        /// <summary>
        /// 学生休学记录状态
        /// </summary>
        public enum StudentSuspendSchoolingRecordStatus
        {
            /// <summary>
            /// 已取消
            /// </summary>
            Cancel = -1,
            /// <summary>
            /// 等待休学
            /// </summary>
            Waiting = 0,
            /// <summary>
            /// 休学中
            /// </summary>
            Using = 1,
            /// <summary>
            /// 休学结束
            /// </summary>
            Completed = 2
        }
        /// <summary>
        /// 周枚举
        /// </summary>
        public enum Week
        {
            Sunday = 0,
            Monday = 1,
            Tuesday = 2,
            Wednesday = 3,
            Thursday = 4,
            Friday = 5,
            Saturday = 6
        }
        /// <summary>
        /// 邮件类型
        /// </summary>
        public enum MailType
        {
            /// <summary>
            /// 发送
            /// </summary>
            Send = 0,
            /// <summary>
            /// 接收
            /// </summary>
            Receive = 1
        }
        /// <summary>
        /// 邮件内容格式
        /// </summary>
        public enum MailContentType
        {
            /// <summary>
            /// 文本
            /// </summary>
            Text = 0,
            /// <summary>
            /// html
            /// </summary>
            Html = 1
        }
        /// <summary>
        /// 邮件状态
        /// </summary>
        public enum MailStatus
        {
            /// <summary>
            /// 未发送
            /// </summary>
            NoSend = 0,
            /// <summary>
            /// 成功
            /// </summary>
            Success = 1,
            /// <summary>
            /// 失败
            /// </summary>
            Fail = 1
        }
        /// <summary>
        /// 扩展类型所属类型
        /// </summary>
        public enum ExtendTypeId
        {
            /// <summary>
            /// 反馈记录类型
            /// </summary>
            反馈记录类型 = 1,
            /// <summary>
            /// 教案类型
            /// </summary>
            教案类型 = 2,
            /// <summary>
            /// 课程资源类型
            /// </summary>
            课程资源类型 = 3
        }


        /// <summary>
        /// 页数名称
        /// </summary>
        public const string Page = "page";
        /// <summary>
        /// 每页数量名称
        /// </summary>
        public const string Limit = "limit";
        /// <summary>
        /// 登陆学生ID名称
        /// </summary>
        public const string LoginStudentId = "LoginStudentID";
        /// <summary>
        /// 登陆学生信息实体名称
        /// </summary>
        public const string LoginStudent = "LoginStudent";
        /// <summary>
        /// 登录学生姓名名称
        /// </summary>
        public const string LoginStudentName = "LoginStudentName";
        /// <summary>
        /// 登陆管理员ID名称
        /// </summary>
        public const string LoginManagerId = "LoginManagerId";
        /// <summary>
        /// 登陆管理员信息实体名称
        /// </summary>
        public const string LoginManager = "LoginManager";
        /// <summary>
        /// 登陆管理员姓名名称
        /// </summary>
        public const string LoginManagerName = "LoginManagerName";

        /// <summary>
        /// 时区显示值名称
        /// </summary>
        public const string UtcShowEnglishName = "UtcShowEnglishName";
        /// <summary>
        /// 当前登录者的时区标识Id名称
        /// </summary>
        public const string TimeZoneInfoIdName = "TimeZoneInfoId";
        /// <summary>
        /// 公告名称
        /// </summary>
        public const string Bulletin = "Bulletin";
        /// <summary>
        /// 菜单栏显示菜单列表
        /// </summary>
        public const string MenuList = "MenuList";
        /// <summary>
        /// 管理员权限信息参数名
        /// </summary>
        public const string ManageAuthority = "ManageAuthority";
        /// <summary>
        /// 是否处理成功参数名
        /// </summary>
        public const string IsFail = "IsFail";
        /// <summary>
        /// 返回数据参数名
        /// </summary>
        public const string ResultData = "ResultData";
        /// <summary>
        /// 密码重置码参数名
        /// </summary>
        public const string ResetCode = "ResetCode";

        /// <summary>
        /// 语言
        /// </summary>
        public const string LangName = "lang";
    }
}
