using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Linq;

namespace DAL.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<OnlineEduContext>
    {
        public static List<ActionInfoModel> ActionInfoList = new List<ActionInfoModel>();
        public Configuration()
        {

            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "DAL.OnlineEduContext";
        }

        protected override void Seed(OnlineEduContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //foreach (var actionInfo in context.ActionInfo.ToList())
            //{
            //    context.Entry(actionInfo).State = System.Data.Entity.EntityState.Deleted;
            //}
            //context.SaveChanges();
            if (ActionInfoList != null)
            {
                foreach (var item in ActionInfoList)
                {
                    if (!context.ActionInfo.Any(d => d.ActionName == item.ActionName && d.ActionResultType == item.ActionResultType && d.AreaName == item.AreaName && d.ControllerFullName == item.ControllerFullName && d.ControllerName == item.ControllerName))
                    {
                        context.ActionInfo.Add(item);
                    }
                }
                context.SaveChanges();
            }
            ActionInfoList = context.ActionInfo.ToList();

            var menuList = new List<MenuModel>()
            {
                new MenuModel(){ID=1,MenuName="系统管理",ParentID=0,LinkUrl="/Manage",Grade=1,Sort=0,MenuImg=""},
                #region 上级(1)系统管理
                    new MenuModel(){ID=2,MenuName="菜单列表",ParentID=1,LinkUrl="/Manage/Menu/Index",Grade=2,Sort=0,MenuImg=""},
                    #region 上级(2)菜单列表
                        new MenuModel(){ID=201,MenuName="列表数据",ParentID=2,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=202,MenuName="添加",ParentID=2,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=203,MenuName="修改",ParentID=2,Grade=3,IsAuthority=true}, 
	                #endregion
                    new MenuModel(){ID=3,MenuName="权限列表",ParentID=1,LinkUrl="/Manage/Authority/Index",Grade=2,Sort=0,MenuImg="",},
                    #region 上级(3)权限列表
                        new MenuModel(){ID=301,MenuName="列表数据",ParentID=3,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=302,MenuName="添加",ParentID=3,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=303,MenuName="修改",ParentID=3,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=304,MenuName="删除",ParentID=3,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    #endregion
                    new MenuModel(){ID=4,MenuName="日志列表",ParentID=4,LinkUrl="/Manage/ManagerLog/Index",Grade=2,Sort=0,MenuImg=""},
                    #region 上级(4)日志列表
                        new MenuModel(){ID=401,MenuName="列表数据",ParentID=4,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""}, 
	                #endregion
                    new MenuModel(){ID=5,MenuName="管理员列表",ParentID=1,LinkUrl="/Manage/Manager/Index",Grade=2,Sort=0,MenuImg=""},
                    #region 上级(5)管理员列表
                        new MenuModel(){ID=501,MenuName="列表数据",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=502,MenuName="添加",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=503,MenuName="删除",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=504,MenuName="详细",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=505,MenuName="详细-基本信息-修改基本信息",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=506,MenuName="详细-基本信息-修改账号信息",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=507,MenuName="详细-主管学生",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=508,MenuName="详细-已发布课程",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=509,MenuName="详细-已预约课程",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=510,MenuName="详细-课程表",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=511,MenuName="详细-教案",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    #endregion
                    new MenuModel(){ID=12,MenuName="系统参数",ParentID=1,LinkUrl="/Manage/SystemParame/SystemParameList",Grade=2,Sort=0,MenuImg=""},
                    #region 上级(12)系统参数
                        new MenuModel(){ID=1201,MenuName="列表数据",ParentID=12,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=1202,MenuName="修改",ParentID=12,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
	                #endregion
                    new MenuModel(){ID=13,MenuName="学生课程状态",ParentID=1,LinkUrl="/Manage/SystemParame/StudentCourseStatusList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=16,MenuName="时区设置",ParentID=1,LinkUrl="/Manage/SystemParame/TimeZoneList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=21,MenuName="假期安排",ParentID=1,LinkUrl="/Manage/SystemParame/HolidayRecordList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=22,MenuName="公告设置",ParentID=1,LinkUrl="/Manage/SystemParame/BulletinList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=31,MenuName="类型管理",ParentID=1,LinkUrl="/Manage/SystemParame/ExtendTypeList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=32,MenuName="邮箱账号列表",ParentID=1,LinkUrl="/Manage/SystemParame/EmailAccountList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=34,MenuName="操作方法信息列表",ParentID=1,LinkUrl="/Manage/Menu/ActionInfoList",Grade=2,Sort=0,MenuImg=""},
	            #endregion
                new MenuModel(){ID=14,MenuName="学生信息管理",ParentID=0,LinkUrl="/Manage/Student",Grade=1,Sort=0,MenuImg=""},
                #region 上级(14)学生信息管理
                    new MenuModel(){ID=6,MenuName="所有学生列表",ParentID=14,LinkUrl="/Manage/Student/AllStudentList",Grade=2,Sort=0,MenuImg=""},
                    #region 上级(6)所有学生列表
                        new MenuModel(){ID=601,MenuName="列表数据",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=602,MenuName="注册",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=603,MenuName="修改账号信息",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=604,MenuName="详细",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=605,MenuName="详细-基本信息-修改基本信息",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=606,MenuName="详细-课程信息",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=607,MenuName="详细-产品信息",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=608,MenuName="详细-休学记录",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=609,MenuName="详细-黑名单",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=610,MenuName="详细-固定课程表",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=611,MenuName="详细-备忘录",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=612,MenuName="详细-销售信息",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    #endregion
                    new MenuModel(){ID=7,MenuName="主管学生列表",ParentID=14,LinkUrl="/Manage/Student/SupervisorStudentList",Grade=2,Sort=0,MenuImg=""},
                    #region 上级(7)主管学生列表
                        new MenuModel(){ID=701,MenuName="列表数据",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=702,MenuName="详细",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=703,MenuName="详细-基本信息-修改基本信息",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=704,MenuName="详细-课程信息",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=705,MenuName="详细-产品信息",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=706,MenuName="详细-休学记录",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=707,MenuName="详细-黑名单",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=708,MenuName="详细-固定课程表",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=709,MenuName="详细-备忘录",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    #endregion 
	            #endregion
                new MenuModel(){ID=15,MenuName="课程产品管理",ParentID=0,LinkUrl="/Manage/Product",Grade=1,Sort=0,MenuImg=""},
                #region 上级(15)课程产品管理
                    new MenuModel(){ID=8,MenuName="产品管理",ParentID=15,LinkUrl="/Manage/Product/Index",Grade=2,Sort=0,MenuImg=""},
                    #region 上级(8)产品管理
                        new MenuModel(){ID=801,MenuName="列表数据",ParentID=8,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=802,MenuName="添加",ParentID=8,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=803,MenuName="修改",ParentID=8,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=804,MenuName="删除",ParentID=8,Grade=3,IsAuthority=true}, 
                    #endregion
                    new MenuModel(){ID=17,MenuName="发布课程(全)",ParentID=15,LinkUrl="/Manage/Course/AllCourseRecord",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=9,MenuName="发布课程",ParentID=15,LinkUrl="/Manage/Course/CourseRecord",Grade=2,Sort=0,MenuImg=""},
                    #region 上级(9)发布课程
                        new MenuModel(){ID=901,MenuName="列表数据",ParentID=9,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=902,MenuName="发布单课程",ParentID=9,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=904,MenuName="取消",ParentID=9,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=905,MenuName="批量发布课程",ParentID=9,Grade=3,IsAuthority=true}, 
                    #endregion
                    new MenuModel(){ID=10,MenuName="预约课程",ParentID=15,LinkUrl="/Manage/Course/StudentCourseRecordList",Grade=2,Sort=0,MenuImg=""},
                    #region 上级(10)预约课程
                        new MenuModel(){ID=1001,MenuName="列表数据",ParentID=10,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=1002,MenuName="填写课程完成情况",ParentID=10,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=1003,MenuName="填写教学计划",ParentID=10,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=1004,MenuName="填写课程完成情况",ParentID=10,Grade=3,IsAuthority=true}, 
                    #endregion
                    new MenuModel(){ID=11,MenuName="教材列表",ParentID=15,LinkUrl="/Manage/Book/BookList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=18,MenuName="预约课程(全)",ParentID=15,LinkUrl="/Manage/Course/AllStudentCourseRecordList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=19,MenuName="课程表列表",ParentID=15,LinkUrl="/Manage/Course/CurriculumManagerList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=20,MenuName="课程表",ParentID=15,LinkUrl="/Manage/Course/CurriculumManagerInfo",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=23,MenuName="教案列表(全)",ParentID=15,LinkUrl="/Manage/Book/AllTeachingPlanList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=24,MenuName="教案列表",ParentID=15,LinkUrl="/Manage/Book/TeachingPlanList",Grade=2,Sort=0,MenuImg=""}, 
	            #endregion
                new MenuModel(){ID=25,MenuName="试读申请记录",ParentID=14,LinkUrl="/Manage/Student/ApplyFreeProbationRecordList",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=26,MenuName="学生推荐记录",ParentID=14,LinkUrl="/Manage/Student/RecommendRecordList",Grade=2,Sort=0,MenuImg=""},
                //new MenuModel(){ID=27,MenuName="教案类型管理",ParentID=15,LinkUrl="/Manage/Book/TeachingPlanTypeList",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=28,MenuName="基本信息",ParentID=0,LinkUrl="/Manage/Manager/ManagerBaseInfo",Grade=1,Sort=1,MenuImg=""},
                #region 上级(28)基本信息
                    new MenuModel(){ID=2801,MenuName="界面数据",ParentID=28,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2802,MenuName="修改",ParentID=28,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                #endregion
                new MenuModel(){ID=29,MenuName="销售学生列表",ParentID=14,LinkUrl="/Manage/Student/CourseConsultantStudentList",Grade=2,Sort=0,MenuImg=""},
                #region 上级(29)销售学生列表
                    new MenuModel(){ID=2901,MenuName="列表数据",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2902,MenuName="注册",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2903,MenuName="修改账号信息",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2904,MenuName="详细",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2905,MenuName="详细-基本信息-修改基本信息",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2906,MenuName="详细-课程信息",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2907,MenuName="详细-产品信息",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2908,MenuName="详细-休学记录",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2909,MenuName="详细-黑名单",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2910,MenuName="详细-固定课程表",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2911,MenuName="详细-备忘录",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2912,MenuName="详细-销售信息",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                #endregion
                new MenuModel(){ID=30,MenuName="学生反馈记录",ParentID=14,LinkUrl="/Manage/Student/FeedbackRecordList",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=33,MenuName="课程资源信息",ParentID=15,LinkUrl="/Manage/Course/CourseResourceList",Grade=2,Sort=0,MenuImg=""},
            };
            try
            {
                string tabName = context.GetTabName(typeof(MenuModel));//获取对应的数据库名称
                System.Text.StringBuilder addMenuSql = new System.Text.StringBuilder();
                addMenuSql.Append($"SET IDENTITY_INSERT {tabName} ON;");
                List<SqlParameter> parameters = new List<SqlParameter>();
                int count = 0;
                foreach (var list in menuList)
                {
                    if (context.Menu.FirstOrDefault(d => d.ID == list.ID) == null)
                    {
                        count++;
                        addMenuSql.Append(
                            $"insert into MenuModel(ID,MenuName,ParentID,LinkUrl,Sort,Grade,MenuImg,IsAuthority) values(@ID{count},@MenuName{count},@ParentID{count},@LinkUrl{count},@Sort{count},@Grade{count},@MenuImg{count},@IsAuthority{count});");
                        parameters.Add(new SqlParameter("@ID" + count, SqlDbType.Int) { Value = list.ID });
                        parameters.Add(new SqlParameter("@MenuName" + count, SqlDbType.NVarChar) { Value = list.MenuName ?? "" });
                        parameters.Add(new SqlParameter("@ParentID" + count, SqlDbType.Int) { Value = list.ParentID });
                        parameters.Add(new SqlParameter("@LinkUrl" + count, SqlDbType.NVarChar) { Value = list.LinkUrl ?? "" });
                        parameters.Add(new SqlParameter("@Sort" + count, SqlDbType.Int) { Value = list.Sort });
                        parameters.Add(new SqlParameter("@Grade" + count, SqlDbType.Int) { Value = list.Grade });
                        parameters.Add(new SqlParameter("@MenuImg" + count, SqlDbType.NVarChar) { Value = list.MenuImg ?? "" });
                        parameters.Add(new SqlParameter("@IsAuthority" + count, SqlDbType.Bit) { Value = list.IsAuthority });
                    }
                }
                addMenuSql.Append($"SET IDENTITY_INSERT {tabName} OFF;");
                context.Database.ExecuteSqlCommand(addMenuSql.ToString(), parameters.ToArray<object>());
            }
            catch
            {
                // ignored
            }

            var deleteMenu = context.Menu.FirstOrDefault(d => d.ID == 27);
            if (deleteMenu != null)
                context.Menu.Remove(deleteMenu);
            context.SaveChanges();

            var authorityList = new List<AuthorityModel>()
            {
                new AuthorityModel(){AuthorityID=1,Name="超级管理员",AddTime=DateTime.Now},
                //new AuthorityModel(){AuthorityID=2,Name="管理员",AddTime=DateTime.Now},
            };
            foreach (var list in authorityList)
            {
                if (context.Authority.FirstOrDefault(d => d.AuthorityID == list.AuthorityID) == null)
                    context.Authority.AddOrUpdate(list);
            }
            //authorityList.ForEach(s => context.Authority.AddOrUpdate(m => m.AuthorityID, s));
            context.SaveChanges();

            var authorityItemList = new List<AuthorityItemModel>();
            foreach (var authority in authorityList)
            {
                foreach (var menu in menuList)
                {
                    var authorityItem = new AuthorityItemModel
                    {
                        AuthorityID = authority.AuthorityID,
                        MenuID = menu.ID,
                        AddTime = DateTime.UtcNow
                    };
                    authorityItemList.Add(authorityItem);
                }
            }
            //删除数据库中已经存在的权限数据
            foreach (var item in context.AuthorityItem.ToList())
            {
                if (authorityItemList.Any(d => d.AuthorityID == item.AuthorityID && d.MenuID == item.MenuID))
                    authorityItemList.Remove(item);
            }
            authorityItemList.ForEach(s => context.AuthorityItem.AddOrUpdate(m => new { m.AuthorityID, m.MenuID }, s));
            context.SaveChanges();

            var managerList = new List<ManagerModel>
            {
                new ManagerModel{ID=1,LoginName="admin",ManagerName="admin",Phone="18888888888",Password="QSvnQL2VrJA=",AuthorityID=1,AddTime=DateTime.UtcNow,IsAdmin=true,Operate=0,TimeZone=1},
            };
            foreach (var list in managerList)
            {
                if (context.Manager.FirstOrDefault(d => d.ID == list.ID) == null)
                    context.Manager.AddOrUpdate(list);
            }
            context.SaveChanges();


            var studentCourseStatusList = new List<StudentCourseStatusMOD>()
            {
                new StudentCourseStatusMOD(){StatusID=-3,AddTime=DateTime.UtcNow,ChinaName="管理员取消",EnglishName="管理员取消",IsEffective=false,IsFixedValue=true,FixedValueName="管理员取消"},
                new StudentCourseStatusMOD(){StatusID=-2,AddTime=DateTime.UtcNow,ChinaName="临时取消",EnglishName="临时取消",IsEffective=false,IsFixedValue=true,FixedValueName="学生临时取消"},
                new StudentCourseStatusMOD(){StatusID=-1,AddTime=DateTime.UtcNow,ChinaName="提前取消",EnglishName="提前取消",IsEffective=false,IsFixedValue=true,FixedValueName="学生提前取消"},
                new StudentCourseStatusMOD(){StatusID=0,AddTime=DateTime.UtcNow,ChinaName="未上课",EnglishName="未上课",IsEffective=true,IsFixedValue=true,FixedValueName="已预约未上课"},
                new StudentCourseStatusMOD(){StatusID=1,AddTime=DateTime.UtcNow,ChinaName="正常完成",EnglishName="正常完成",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=2,AddTime=DateTime.UtcNow,ChinaName="学生缺勤",EnglishName="学生缺勤",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=3,AddTime=DateTime.UtcNow,ChinaName="学生迟到",EnglishName="学生迟到",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=4,AddTime=DateTime.UtcNow,ChinaName="学生技术原因迟到",EnglishName="学生技术原因迟到",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=5,AddTime=DateTime.UtcNow,ChinaName="学生早退",EnglishName="学生早退",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=6,AddTime=DateTime.UtcNow,ChinaName="教师缺勤",EnglishName="教师缺勤",IsEffective=false,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=7,AddTime=DateTime.UtcNow,ChinaName="教师迟到",EnglishName="教师迟到",IsEffective=false,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=8,AddTime=DateTime.UtcNow,ChinaName="教师技术原因迟到",EnglishName="教师技术原因迟到",IsEffective=false,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=9,AddTime=DateTime.UtcNow,ChinaName="教师早退",EnglishName="教师早退",IsEffective=false,FixedValueName=""},
            };
            foreach (var list in studentCourseStatusList)
            {
                var model = context.StudentCourseStatus.FirstOrDefault(d => d.StatusID == list.StatusID);
                if (model?.FixedValueName == null)
                    context.StudentCourseStatus.AddOrUpdate(list);
            }
            //StudentCourseStatusList.ForEach(s => context.StudentCourseStatus.AddOrUpdate(m => m.StatusID, s));
            context.SaveChanges();

            //邮箱
            List<EmailAccountModel> emailAccountList = new List<EmailAccountModel>()
            {
                new EmailAccountModel(){Id=1,AddTime=DateTime.Now,EmailName="密码重置中心",EmailAddress="password@ichinesecenter.com", UserName="password@ichinesecenter.com",Password="Abc12345",SmtpServer="smtp.exmail.qq.com:587",EditTime=DateTime.Now,OperateId=1}
            };
            foreach (var model in emailAccountList)
            {
                var isExist = context.EmailAccount.Any(d => d.Id.Equals(model.Id));
                if (!isExist)
                    context.EmailAccount.AddOrUpdate(model);
            }
            context.SaveChanges();

            var systemParameList = new List<SystemParameMOD>()
            {
                new SystemParameMOD(){ID=1,AddTime=DateTime.UtcNow,Name="学生更改或取消课程限制时间小时数",Value=1,IsEnable=true},
                new SystemParameMOD(){ID=2,AddTime=DateTime.UtcNow,Name="学生状态长期不上课最低天数",Value=14,IsEnable=true},
                new SystemParameMOD(){ID=3,AddTime=DateTime.UtcNow,Name="任课老师上课最大连续节数",Value=4,IsEnable=true},
                new SystemParameMOD(){ID=4,AddTime=DateTime.UtcNow,Name="学生选课限制时间小时数",Value=24,IsEnable=true},
                new SystemParameMOD(){ID=5,AddTime=DateTime.UtcNow,Name="课程1个课时的分钟数",Value=50,IsEnable=true},
                new SystemParameMOD(){ID=6,AddTime=DateTime.UtcNow,Name="单次休学的天数",Value=7,IsEnable=true},
                new SystemParameMOD(){ID=7,AddTime=DateTime.UtcNow,Name="选课界面主管背景颜色",StrValue="rgb(70,130,180)",IsEnable=true},
                new SystemParameMOD(){ID=8,AddTime=DateTime.UtcNow,Name="选课界面代课背景颜色",StrValue="rgb(60,179,113)",IsEnable=true},
                new SystemParameMOD(){ID=9,AddTime=DateTime.UtcNow,Name="选课界面已选背景颜色",StrValue="rgb(240,128,128)",IsEnable=true},
                new SystemParameMOD(){ID=10,AddTime=DateTime.UtcNow,Name="选课界面主管字体颜色",StrValue="rgb(255,255,255)",IsEnable=true},
                new SystemParameMOD(){ID=11,AddTime=DateTime.UtcNow,Name="选课界面代课字体颜色",StrValue="rgb(255,255,255)",IsEnable=true},
                new SystemParameMOD(){ID=12,AddTime=DateTime.UtcNow,Name="选课界面已选字体颜色",StrValue="rgb(255,255,255)",IsEnable=true},
                new SystemParameMOD(){ID=13,AddTime=DateTime.UtcNow,Name="选课界面主管边框颜色",StrValue="#3a87ad",IsEnable=true},
                new SystemParameMOD(){ID=14,AddTime=DateTime.UtcNow,Name="选课界面代课边框颜色",StrValue="#3a87ad",IsEnable=true},
                new SystemParameMOD(){ID=15,AddTime=DateTime.UtcNow,Name="选课界面已选边框颜色",StrValue="#3a87ad",IsEnable=true},
                new SystemParameMOD(){ID=16,AddTime=DateTime.UtcNow,Name="是否自动发布课程",Value=1,IsEnable=true},
                new SystemParameMOD(){ID=17,AddTime=DateTime.UtcNow,Name="学生处于续费期的开始时间为整个产品时间的百分比值",Value=20,IsEnable=true},
                new SystemParameMOD(){ID=18,AddTime=DateTime.UtcNow,Name="学生处于续费期的结束时间为产品结束以后的天数以内",Value=28,IsEnable=true},
                new SystemParameMOD(){ID=19,AddTime=DateTime.UtcNow,Name="密码重置码有效时间(分钟)",Value=30,IsEnable=true},
                new SystemParameMOD(){ID=20,AddTime=DateTime.UtcNow,Name="发送密码重置邮件的邮箱账号ID",Value=1,IsEnable=true},
            };
            foreach (var list in systemParameList)
            {
                if (context.SystemParame.FirstOrDefault(d => d.ID == list.ID) == null)
                    context.SystemParame.AddOrUpdate(list);
            }
            //SystemParameList.ForEach(s =>context.SystemParame.AddOrUpdate(m => m.ID, s));
            context.SaveChanges();
            

            var timeZoneInfoList = TimeZoneInfo.GetSystemTimeZones();
            int listCount = 0;
            foreach (var timeZoneInfo in timeZoneInfoList)
            {
                //DisplayName = (UTC-12:00) 国际日期变更线西
                Match match = new Regex(@"^\((?<utc>[\s\S]*?)\)\s*(?<name>[\s\S]*?)$", RegexOptions.IgnoreCase).Match(timeZoneInfo.DisplayName);
                var model = new TimeZoneModel()
                {
                    ID = listCount++,
                    TimeZoneInfoId = timeZoneInfo.Id,
                    Name = match.Groups["name"].Value,
                    EnglishName = timeZoneInfo.Id,
                    UTCValueName = match.Groups["utc"].Value,
                    UTCTotalHours = timeZoneInfo.BaseUtcOffset.TotalHours,
                    IsDST = timeZoneInfo.SupportsDaylightSavingTime,
                    IsEnable = true
                };
                context.TimeZone.AddOrUpdate(model);
            }
            context.SaveChanges();

            //创建触发器trigger_StudentProduct   修改EndDate时同步变动StartRechargePromptTime和EndRechargePromptTime
            try
            {
                string strSql =
                    @"if((select count(1) from sysobjects where xtype='TR' and name='trigger_StudentProduct')>0)
                      begin
                      	drop Trigger trigger_StudentProduct;
                      end";
                context.Database.ExecuteSqlCommand(strSql);
                strSql = @"Create Trigger trigger_StudentProduct 
                          On StudentProductMOD--在StudentProductMOD表中创建触发器 
                          for Insert,Update--为什么事件触发 
                          As
                          --事件触发后所要做的事情
                          if(UPDATE(EndDate))
                          begin
                            declare @StartRechargePromptDayPercentage int=0;
                            select @StartRechargePromptDayPercentage=value from SystemparameMOD where ID = 17;
                            if(@StartRechargePromptDayPercentage is null or @StartRechargePromptDayPercentage<0)
                            	set @StartRechargePromptDayPercentage=0;
                            declare @EndRechargePromptDay int=0;
                            select @EndRechargePromptDay=value from SystemparameMOD where ID = 18;
                            if(@EndRechargePromptDay is null or @EndRechargePromptDay<0)
                            	set @EndRechargePromptDay=0;
                            if(@StartRechargePromptDayPercentage>0 or @EndRechargePromptDay>0)
                            begin
                            	update StudentProductMOD set StartRechargePromptTime=DATEADD(hour,-(LimitDate*7*24*@StartRechargePromptDayPercentage/100),EndDate),EndRechargePromptTime=DATEADD(hour,@EndRechargePromptDay*24,EndDate)
                            	where ID in (select ID from inserted);
                            end
                          end";
                context.Database.ExecuteSqlCommand(strSql);
                strSql = @"update StudentProductMOD set EndDate=EndDate";
                context.Database.ExecuteSqlCommand(strSql);
                context.SaveChanges();
            }
            catch
            {
                // ignored
            }
        }
    }
}
