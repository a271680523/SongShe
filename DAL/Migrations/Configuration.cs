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
                new MenuModel(){ID=1,MenuName="ϵͳ����",ParentID=0,LinkUrl="/Manage",Grade=1,Sort=0,MenuImg=""},
                #region �ϼ�(1)ϵͳ����
                    new MenuModel(){ID=2,MenuName="�˵��б�",ParentID=1,LinkUrl="/Manage/Menu/Index",Grade=2,Sort=0,MenuImg=""},
                    #region �ϼ�(2)�˵��б�
                        new MenuModel(){ID=201,MenuName="�б�����",ParentID=2,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=202,MenuName="���",ParentID=2,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=203,MenuName="�޸�",ParentID=2,Grade=3,IsAuthority=true}, 
	                #endregion
                    new MenuModel(){ID=3,MenuName="Ȩ���б�",ParentID=1,LinkUrl="/Manage/Authority/Index",Grade=2,Sort=0,MenuImg="",},
                    #region �ϼ�(3)Ȩ���б�
                        new MenuModel(){ID=301,MenuName="�б�����",ParentID=3,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=302,MenuName="���",ParentID=3,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=303,MenuName="�޸�",ParentID=3,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=304,MenuName="ɾ��",ParentID=3,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    #endregion
                    new MenuModel(){ID=4,MenuName="��־�б�",ParentID=4,LinkUrl="/Manage/ManagerLog/Index",Grade=2,Sort=0,MenuImg=""},
                    #region �ϼ�(4)��־�б�
                        new MenuModel(){ID=401,MenuName="�б�����",ParentID=4,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""}, 
	                #endregion
                    new MenuModel(){ID=5,MenuName="����Ա�б�",ParentID=1,LinkUrl="/Manage/Manager/Index",Grade=2,Sort=0,MenuImg=""},
                    #region �ϼ�(5)����Ա�б�
                        new MenuModel(){ID=501,MenuName="�б�����",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=502,MenuName="���",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=503,MenuName="ɾ��",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=504,MenuName="��ϸ",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=505,MenuName="��ϸ-������Ϣ-�޸Ļ�����Ϣ",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=506,MenuName="��ϸ-������Ϣ-�޸��˺���Ϣ",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=507,MenuName="��ϸ-����ѧ��",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=508,MenuName="��ϸ-�ѷ����γ�",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=509,MenuName="��ϸ-��ԤԼ�γ�",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=510,MenuName="��ϸ-�γ̱�",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=511,MenuName="��ϸ-�̰�",ParentID=5,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    #endregion
                    new MenuModel(){ID=12,MenuName="ϵͳ����",ParentID=1,LinkUrl="/Manage/SystemParame/SystemParameList",Grade=2,Sort=0,MenuImg=""},
                    #region �ϼ�(12)ϵͳ����
                        new MenuModel(){ID=1201,MenuName="�б�����",ParentID=12,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=1202,MenuName="�޸�",ParentID=12,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
	                #endregion
                    new MenuModel(){ID=13,MenuName="ѧ���γ�״̬",ParentID=1,LinkUrl="/Manage/SystemParame/StudentCourseStatusList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=16,MenuName="ʱ������",ParentID=1,LinkUrl="/Manage/SystemParame/TimeZoneList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=21,MenuName="���ڰ���",ParentID=1,LinkUrl="/Manage/SystemParame/HolidayRecordList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=22,MenuName="��������",ParentID=1,LinkUrl="/Manage/SystemParame/BulletinList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=31,MenuName="���͹���",ParentID=1,LinkUrl="/Manage/SystemParame/ExtendTypeList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=32,MenuName="�����˺��б�",ParentID=1,LinkUrl="/Manage/SystemParame/EmailAccountList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=34,MenuName="����������Ϣ�б�",ParentID=1,LinkUrl="/Manage/Menu/ActionInfoList",Grade=2,Sort=0,MenuImg=""},
	            #endregion
                new MenuModel(){ID=14,MenuName="ѧ����Ϣ����",ParentID=0,LinkUrl="/Manage/Student",Grade=1,Sort=0,MenuImg=""},
                #region �ϼ�(14)ѧ����Ϣ����
                    new MenuModel(){ID=6,MenuName="����ѧ���б�",ParentID=14,LinkUrl="/Manage/Student/AllStudentList",Grade=2,Sort=0,MenuImg=""},
                    #region �ϼ�(6)����ѧ���б�
                        new MenuModel(){ID=601,MenuName="�б�����",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=602,MenuName="ע��",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=603,MenuName="�޸��˺���Ϣ",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=604,MenuName="��ϸ",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=605,MenuName="��ϸ-������Ϣ-�޸Ļ�����Ϣ",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=606,MenuName="��ϸ-�γ���Ϣ",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=607,MenuName="��ϸ-��Ʒ��Ϣ",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=608,MenuName="��ϸ-��ѧ��¼",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=609,MenuName="��ϸ-������",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=610,MenuName="��ϸ-�̶��γ̱�",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=611,MenuName="��ϸ-����¼",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=612,MenuName="��ϸ-������Ϣ",ParentID=6,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    #endregion
                    new MenuModel(){ID=7,MenuName="����ѧ���б�",ParentID=14,LinkUrl="/Manage/Student/SupervisorStudentList",Grade=2,Sort=0,MenuImg=""},
                    #region �ϼ�(7)����ѧ���б�
                        new MenuModel(){ID=701,MenuName="�б�����",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=702,MenuName="��ϸ",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=703,MenuName="��ϸ-������Ϣ-�޸Ļ�����Ϣ",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=704,MenuName="��ϸ-�γ���Ϣ",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=705,MenuName="��ϸ-��Ʒ��Ϣ",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=706,MenuName="��ϸ-��ѧ��¼",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=707,MenuName="��ϸ-������",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=708,MenuName="��ϸ-�̶��γ̱�",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                        new MenuModel(){ID=709,MenuName="��ϸ-����¼",ParentID=7,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    #endregion 
	            #endregion
                new MenuModel(){ID=15,MenuName="�γ̲�Ʒ����",ParentID=0,LinkUrl="/Manage/Product",Grade=1,Sort=0,MenuImg=""},
                #region �ϼ�(15)�γ̲�Ʒ����
                    new MenuModel(){ID=8,MenuName="��Ʒ����",ParentID=15,LinkUrl="/Manage/Product/Index",Grade=2,Sort=0,MenuImg=""},
                    #region �ϼ�(8)��Ʒ����
                        new MenuModel(){ID=801,MenuName="�б�����",ParentID=8,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=802,MenuName="���",ParentID=8,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=803,MenuName="�޸�",ParentID=8,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=804,MenuName="ɾ��",ParentID=8,Grade=3,IsAuthority=true}, 
                    #endregion
                    new MenuModel(){ID=17,MenuName="�����γ�(ȫ)",ParentID=15,LinkUrl="/Manage/Course/AllCourseRecord",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=9,MenuName="�����γ�",ParentID=15,LinkUrl="/Manage/Course/CourseRecord",Grade=2,Sort=0,MenuImg=""},
                    #region �ϼ�(9)�����γ�
                        new MenuModel(){ID=901,MenuName="�б�����",ParentID=9,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=902,MenuName="�������γ�",ParentID=9,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=904,MenuName="ȡ��",ParentID=9,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=905,MenuName="���������γ�",ParentID=9,Grade=3,IsAuthority=true}, 
                    #endregion
                    new MenuModel(){ID=10,MenuName="ԤԼ�γ�",ParentID=15,LinkUrl="/Manage/Course/StudentCourseRecordList",Grade=2,Sort=0,MenuImg=""},
                    #region �ϼ�(10)ԤԼ�γ�
                        new MenuModel(){ID=1001,MenuName="�б�����",ParentID=10,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=1002,MenuName="��д�γ�������",ParentID=10,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=1003,MenuName="��д��ѧ�ƻ�",ParentID=10,Grade=3,IsAuthority=true},
                        new MenuModel(){ID=1004,MenuName="��д�γ�������",ParentID=10,Grade=3,IsAuthority=true}, 
                    #endregion
                    new MenuModel(){ID=11,MenuName="�̲��б�",ParentID=15,LinkUrl="/Manage/Book/BookList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=18,MenuName="ԤԼ�γ�(ȫ)",ParentID=15,LinkUrl="/Manage/Course/AllStudentCourseRecordList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=19,MenuName="�γ̱��б�",ParentID=15,LinkUrl="/Manage/Course/CurriculumManagerList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=20,MenuName="�γ̱�",ParentID=15,LinkUrl="/Manage/Course/CurriculumManagerInfo",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=23,MenuName="�̰��б�(ȫ)",ParentID=15,LinkUrl="/Manage/Book/AllTeachingPlanList",Grade=2,Sort=0,MenuImg=""},
                    new MenuModel(){ID=24,MenuName="�̰��б�",ParentID=15,LinkUrl="/Manage/Book/TeachingPlanList",Grade=2,Sort=0,MenuImg=""}, 
	            #endregion
                new MenuModel(){ID=25,MenuName="�Զ������¼",ParentID=14,LinkUrl="/Manage/Student/ApplyFreeProbationRecordList",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=26,MenuName="ѧ���Ƽ���¼",ParentID=14,LinkUrl="/Manage/Student/RecommendRecordList",Grade=2,Sort=0,MenuImg=""},
                //new MenuModel(){ID=27,MenuName="�̰����͹���",ParentID=15,LinkUrl="/Manage/Book/TeachingPlanTypeList",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=28,MenuName="������Ϣ",ParentID=0,LinkUrl="/Manage/Manager/ManagerBaseInfo",Grade=1,Sort=1,MenuImg=""},
                #region �ϼ�(28)������Ϣ
                    new MenuModel(){ID=2801,MenuName="��������",ParentID=28,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2802,MenuName="�޸�",ParentID=28,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                #endregion
                new MenuModel(){ID=29,MenuName="����ѧ���б�",ParentID=14,LinkUrl="/Manage/Student/CourseConsultantStudentList",Grade=2,Sort=0,MenuImg=""},
                #region �ϼ�(29)����ѧ���б�
                    new MenuModel(){ID=2901,MenuName="�б�����",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2902,MenuName="ע��",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2903,MenuName="�޸��˺���Ϣ",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2904,MenuName="��ϸ",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2905,MenuName="��ϸ-������Ϣ-�޸Ļ�����Ϣ",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2906,MenuName="��ϸ-�γ���Ϣ",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2907,MenuName="��ϸ-��Ʒ��Ϣ",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2908,MenuName="��ϸ-��ѧ��¼",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2909,MenuName="��ϸ-������",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2910,MenuName="��ϸ-�̶��γ̱�",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2911,MenuName="��ϸ-����¼",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                    new MenuModel(){ID=2912,MenuName="��ϸ-������Ϣ",ParentID=29,Grade=3,IsAuthority=true,LinkUrl="",Sort=0,MenuImg=""},
                #endregion
                new MenuModel(){ID=30,MenuName="ѧ��������¼",ParentID=14,LinkUrl="/Manage/Student/FeedbackRecordList",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=33,MenuName="�γ���Դ��Ϣ",ParentID=15,LinkUrl="/Manage/Course/CourseResourceList",Grade=2,Sort=0,MenuImg=""},
            };
            try
            {
                string tabName = context.GetTabName(typeof(MenuModel));//��ȡ��Ӧ�����ݿ�����
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
                new AuthorityModel(){AuthorityID=1,Name="��������Ա",AddTime=DateTime.Now},
                //new AuthorityModel(){AuthorityID=2,Name="����Ա",AddTime=DateTime.Now},
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
            //ɾ�����ݿ����Ѿ����ڵ�Ȩ������
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
                new StudentCourseStatusMOD(){StatusID=-3,AddTime=DateTime.UtcNow,ChinaName="����Աȡ��",EnglishName="����Աȡ��",IsEffective=false,IsFixedValue=true,FixedValueName="����Աȡ��"},
                new StudentCourseStatusMOD(){StatusID=-2,AddTime=DateTime.UtcNow,ChinaName="��ʱȡ��",EnglishName="��ʱȡ��",IsEffective=false,IsFixedValue=true,FixedValueName="ѧ����ʱȡ��"},
                new StudentCourseStatusMOD(){StatusID=-1,AddTime=DateTime.UtcNow,ChinaName="��ǰȡ��",EnglishName="��ǰȡ��",IsEffective=false,IsFixedValue=true,FixedValueName="ѧ����ǰȡ��"},
                new StudentCourseStatusMOD(){StatusID=0,AddTime=DateTime.UtcNow,ChinaName="δ�Ͽ�",EnglishName="δ�Ͽ�",IsEffective=true,IsFixedValue=true,FixedValueName="��ԤԼδ�Ͽ�"},
                new StudentCourseStatusMOD(){StatusID=1,AddTime=DateTime.UtcNow,ChinaName="�������",EnglishName="�������",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=2,AddTime=DateTime.UtcNow,ChinaName="ѧ��ȱ��",EnglishName="ѧ��ȱ��",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=3,AddTime=DateTime.UtcNow,ChinaName="ѧ���ٵ�",EnglishName="ѧ���ٵ�",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=4,AddTime=DateTime.UtcNow,ChinaName="ѧ������ԭ��ٵ�",EnglishName="ѧ������ԭ��ٵ�",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=5,AddTime=DateTime.UtcNow,ChinaName="ѧ������",EnglishName="ѧ������",IsEffective=true,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=6,AddTime=DateTime.UtcNow,ChinaName="��ʦȱ��",EnglishName="��ʦȱ��",IsEffective=false,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=7,AddTime=DateTime.UtcNow,ChinaName="��ʦ�ٵ�",EnglishName="��ʦ�ٵ�",IsEffective=false,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=8,AddTime=DateTime.UtcNow,ChinaName="��ʦ����ԭ��ٵ�",EnglishName="��ʦ����ԭ��ٵ�",IsEffective=false,FixedValueName=""},
                new StudentCourseStatusMOD(){StatusID=9,AddTime=DateTime.UtcNow,ChinaName="��ʦ����",EnglishName="��ʦ����",IsEffective=false,FixedValueName=""},
            };
            foreach (var list in studentCourseStatusList)
            {
                var model = context.StudentCourseStatus.FirstOrDefault(d => d.StatusID == list.StatusID);
                if (model?.FixedValueName == null)
                    context.StudentCourseStatus.AddOrUpdate(list);
            }
            //StudentCourseStatusList.ForEach(s => context.StudentCourseStatus.AddOrUpdate(m => m.StatusID, s));
            context.SaveChanges();

            //����
            List<EmailAccountModel> emailAccountList = new List<EmailAccountModel>()
            {
                new EmailAccountModel(){Id=1,AddTime=DateTime.Now,EmailName="������������",EmailAddress="password@ichinesecenter.com", UserName="password@ichinesecenter.com",Password="Abc12345",SmtpServer="smtp.exmail.qq.com:587",EditTime=DateTime.Now,OperateId=1}
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
                new SystemParameMOD(){ID=1,AddTime=DateTime.UtcNow,Name="ѧ�����Ļ�ȡ���γ�����ʱ��Сʱ��",Value=1,IsEnable=true},
                new SystemParameMOD(){ID=2,AddTime=DateTime.UtcNow,Name="ѧ��״̬���ڲ��Ͽ��������",Value=14,IsEnable=true},
                new SystemParameMOD(){ID=3,AddTime=DateTime.UtcNow,Name="�ο���ʦ�Ͽ������������",Value=4,IsEnable=true},
                new SystemParameMOD(){ID=4,AddTime=DateTime.UtcNow,Name="ѧ��ѡ������ʱ��Сʱ��",Value=24,IsEnable=true},
                new SystemParameMOD(){ID=5,AddTime=DateTime.UtcNow,Name="�γ�1����ʱ�ķ�����",Value=50,IsEnable=true},
                new SystemParameMOD(){ID=6,AddTime=DateTime.UtcNow,Name="������ѧ������",Value=7,IsEnable=true},
                new SystemParameMOD(){ID=7,AddTime=DateTime.UtcNow,Name="ѡ�ν������ܱ�����ɫ",StrValue="rgb(70,130,180)",IsEnable=true},
                new SystemParameMOD(){ID=8,AddTime=DateTime.UtcNow,Name="ѡ�ν�����α�����ɫ",StrValue="rgb(60,179,113)",IsEnable=true},
                new SystemParameMOD(){ID=9,AddTime=DateTime.UtcNow,Name="ѡ�ν�����ѡ������ɫ",StrValue="rgb(240,128,128)",IsEnable=true},
                new SystemParameMOD(){ID=10,AddTime=DateTime.UtcNow,Name="ѡ�ν�������������ɫ",StrValue="rgb(255,255,255)",IsEnable=true},
                new SystemParameMOD(){ID=11,AddTime=DateTime.UtcNow,Name="ѡ�ν������������ɫ",StrValue="rgb(255,255,255)",IsEnable=true},
                new SystemParameMOD(){ID=12,AddTime=DateTime.UtcNow,Name="ѡ�ν�����ѡ������ɫ",StrValue="rgb(255,255,255)",IsEnable=true},
                new SystemParameMOD(){ID=13,AddTime=DateTime.UtcNow,Name="ѡ�ν������ܱ߿���ɫ",StrValue="#3a87ad",IsEnable=true},
                new SystemParameMOD(){ID=14,AddTime=DateTime.UtcNow,Name="ѡ�ν�����α߿���ɫ",StrValue="#3a87ad",IsEnable=true},
                new SystemParameMOD(){ID=15,AddTime=DateTime.UtcNow,Name="ѡ�ν�����ѡ�߿���ɫ",StrValue="#3a87ad",IsEnable=true},
                new SystemParameMOD(){ID=16,AddTime=DateTime.UtcNow,Name="�Ƿ��Զ������γ�",Value=1,IsEnable=true},
                new SystemParameMOD(){ID=17,AddTime=DateTime.UtcNow,Name="ѧ�����������ڵĿ�ʼʱ��Ϊ������Ʒʱ��İٷֱ�ֵ",Value=20,IsEnable=true},
                new SystemParameMOD(){ID=18,AddTime=DateTime.UtcNow,Name="ѧ�����������ڵĽ���ʱ��Ϊ��Ʒ�����Ժ����������",Value=28,IsEnable=true},
                new SystemParameMOD(){ID=19,AddTime=DateTime.UtcNow,Name="������������Чʱ��(����)",Value=30,IsEnable=true},
                new SystemParameMOD(){ID=20,AddTime=DateTime.UtcNow,Name="�������������ʼ��������˺�ID",Value=1,IsEnable=true},
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
                //DisplayName = (UTC-12:00) �������ڱ������
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

            //����������trigger_StudentProduct   �޸�EndDateʱͬ���䶯StartRechargePromptTime��EndRechargePromptTime
            try
            {
                string strSql =
                    @"if((select count(1) from sysobjects where xtype='TR' and name='trigger_StudentProduct')>0)
                      begin
                      	drop Trigger trigger_StudentProduct;
                      end";
                context.Database.ExecuteSqlCommand(strSql);
                strSql = @"Create Trigger trigger_StudentProduct 
                          On StudentProductMOD--��StudentProductMOD���д��������� 
                          for Insert,Update--Ϊʲô�¼����� 
                          As
                          --�¼���������Ҫ��������
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
