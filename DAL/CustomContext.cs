using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBUtility.ORM;
using Model;

namespace DAL
{
    public class CustomContext : CoreContext
    {
        public CustomContext() : base("OnlineEduContext")
        {

        }
        /// <summary>
        /// 管理员表
        /// </summary>
        public DbSet<ManagerModel> Manager { get; set; }
        /// <summary>
        /// 菜单表
        /// </summary>
        public DbSet<MenuModel> Menu { get; set; }
        /// <summary>
        /// 操作方法实体类
        /// </summary>
        public DbSet<ActionInfoModel> ActionInfo { get; set; }
        /// <summary>
        /// 菜单操作方法联系实体类
        /// </summary>
        public DbSet<MenuActionInfoModel> MenuActionInfo { get; set; }
        /// <summary>
        /// 权限表
        /// </summary>
        public DbSet<AuthorityModel> Authority { get; set; }
        /// <summary>
        /// 权限明细表
        /// </summary>
        public DbSet<AuthorityItemModel> AuthorityItem { get; set; }
        /// <summary>
        /// 管理日志表
        /// </summary>
        public DbSet<ManagerLogModel> ManagerLog { get; set; }
        /// <summary>
        /// 学生表
        /// </summary>
        public DbSet<StudentMOD> Student { get; set; }
        /// <summary>
        /// 产品表
        /// </summary>
        public DbSet<ProductMOD> Product { get; set; }
        /// <summary>
        /// 学生产品表
        /// </summary>
        public DbSet<StudentProductMOD> StudentProduct { get; set; }
        /// <summary>
        /// 学生产品分期记录表
        /// </summary>
        public DbSet<InstallmentPayInfoModel> InstallmentPayInfo { get; set; }
        /// <summary>
        /// 课程发布记录表
        /// </summary>
        public DbSet<CourseRecordMOD> CourseRecord { get; set; }
        /// <summary>
        /// 学生选课记录表
        /// </summary>
        public DbSet<StudentCourseRecordModel> StudentCourseRecord { get; set; }
        /// <summary>
        /// 学生课程状态表
        /// </summary>
        public DbSet<StudentCourseStatusMOD> StudentCourseStatus { get; set; }
        /// <summary>
        /// 学生休学记录表
        /// </summary>
        public DbSet<StudentSuspendSchoolingRecordMOD> StudentSuspendSchoolingRecord { get; set; }
        /// <summary>
        /// 系统参数表
        /// </summary>
        public DbSet<SystemParameMOD> SystemParame { get; set; }
        /// <summary>
        /// 教材表
        /// </summary>
        public DbSet<BookMOD> Book { get; set; }
        /// <summary>
        /// 时区表
        /// </summary>
        public DbSet<TimeZoneModel> TimeZone { get; set; }
        /// <summary>
        /// 管理员课程表
        /// </summary>
        public DbSet<CurriculumManagerMOD> CurriculumManager { get; set; }
        /// <summary>
        /// 学生课程表
        /// </summary>
        public DbSet<CurriculumStudentMOD> CurriculumStudent { get; set; }
        /// <summary>
        /// 课程老师黑名单
        /// </summary>
        public DbSet<CourseManagerBlackListMOD> CourseManagerBlackList { get; set; }
        /// <summary>
        /// 假期记录
        /// </summary>
        public DbSet<HolidayRecordModel> HolidayRecordList { get; set; }
        /// <summary>
        /// 反馈记录
        /// </summary>
        public DbSet<FeedbackRecordModel> FeedbackRecordList { get; set; }
        /// <summary>
        /// 公告记录
        /// </summary>
        public DbSet<BulletinModel> BulletinList { get; set; }
        /// <summary>
        /// 教案表
        /// </summary>
        public DbSet<TeachingPlanModel> TeachingPlanList { get; set; }
        /// <summary>
        /// 教案课时内容表
        /// </summary>
        public DbSet<TeachingPlanCourseModel> TeachingPlanCourseList { get; set; }
        /// <summary>
        /// 申请免费试读课程表        
        /// </summary>
        public DbSet<ApplyFreeProbationRecordModel> ApplyFreeProbationRecordList { get; set; }
        /// <summary>
        /// 推荐记录表
        /// </summary>
        public DbSet<RecommendRecordModel> RecommendRecordList { get; set; }
        /// <summary>
        /// 备忘录记录表
        /// </summary>
        public DbSet<MemorandumRecordModel> MemorandumRecordList { get; set; }
        /// <summary>
        /// 扩展类型表
        /// </summary>
        public DbSet<ExtendTypeModel> ExtendType { get; set; }
        /// <summary>
        /// 邮箱账号表
        /// </summary>
        public DbSet<EmailAccountModel> EmailAccount { get; set; }
        /// <summary>
        /// 课程资源表
        /// </summary>
        public DbSet<CourseResourceModel> CourseResource { get; set; }
    }
}
