using Model;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class OnlineEduInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<OnlineEduContext>
    {
        protected override void Seed(OnlineEduContext context)
        {
            var MenuList = new List<MenuModel>()
            {
                new MenuModel(){ID=1,MenuName="系统管理",ParentID=0,LinkUrl="/Manage",Grade=1,Sort=0,MenuImg=""},
                new MenuModel(){ID=2,MenuName="菜单列表",ParentID=1,LinkUrl="/Manage/Menu/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=3,MenuName="权限列表",ParentID=1,LinkUrl="/Manage/Authority/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=4,MenuName="日志列表",ParentID=1,LinkUrl="/Manage/ManagerLog/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=5,MenuName="管理员列表",ParentID=1,LinkUrl="/Manage/Manager/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=6,MenuName="系统管理",ParentID=0,LinkUrl="/Manage",Grade=1,Sort=0,MenuImg=""},
                new MenuModel(){ID=7,MenuName="菜单列表",ParentID=6,LinkUrl="/Manage/Menu/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=8,MenuName="权限列表",ParentID=6,LinkUrl="/Manage/Authority/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=9,MenuName="日志列表",ParentID=6,LinkUrl="/Manage/ManagerLog/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=10,MenuName="管理员列表",ParentID=6,LinkUrl="/Manage/Manager/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=11,MenuName="系统管理",ParentID=0,LinkUrl="/Manage",Grade=1,Sort=0,MenuImg=""},
                new MenuModel(){ID=12,MenuName="菜单列表",ParentID=11,LinkUrl="/Manage/Menu/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=13,MenuName="权限列表",ParentID=11,LinkUrl="/Manage/Authority/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=14,MenuName="日志列表",ParentID=11,LinkUrl="/Manage/ManagerLog/Index",Grade=2,Sort=0,MenuImg=""},
                new MenuModel(){ID=15,MenuName="管理员列表",ParentID=11,LinkUrl="/Manage/Manager/Index",Grade=2,Sort=0,MenuImg=""},
            };
            MenuList.ForEach(s => context.Menu.Add(s));
            context.SaveChanges();

            var AuthorityList = new List<AuthorityModel>()
            {
                new AuthorityModel(){AuthorityID=1,Name="超级管理员",AddTime=DateTime.Now},
                new AuthorityModel(){AuthorityID=2,Name="管理员",AddTime=DateTime.Now},
            };
            AuthorityList.ForEach(s => context.Authority.Add(s));
            context.SaveChanges();

            var AuthorityItemList = new List<AuthorityItemModel>()
            {
                new AuthorityItemModel(){ID=1,AuthorityID=1,MenuID=1,AddTime=DateTime.Now},
                new AuthorityItemModel(){ID=2,AuthorityID=1,MenuID=2,AddTime=DateTime.Now},
                new AuthorityItemModel(){ID=3,AuthorityID=1,MenuID=3,AddTime=DateTime.Now},
                new AuthorityItemModel(){ID=4,AuthorityID=1,MenuID=4,AddTime=DateTime.Now},
                new AuthorityItemModel(){ID=5,AuthorityID=1,MenuID=5,AddTime=DateTime.Now},
                new AuthorityItemModel(){ID=6,AuthorityID=2,MenuID=1,AddTime=DateTime.Now},
                new AuthorityItemModel(){ID=7,AuthorityID=2,MenuID=5,AddTime=DateTime.Now},
            };
            AuthorityItemList.ForEach(s => context.AuthorityItem.Add(s));
            context.SaveChanges();

            var ManagerList = new List<ManagerModel>
            {
                new ManagerModel{ID=1, LoginName="admin",ManagerName="admin",Phone="18888888888",Password="123456",AuthorityID=1,AddTime=DateTime.Now,IsAdmin=true,Operate=0},
                new ManagerModel{ID=2,LoginName="test",ManagerName="test",Phone="17777777777",Password="123456",AuthorityID=2,AddTime=DateTime.Now,IsAdmin=true,Operate=1},
                new ManagerModel{ID=3,LoginName="ceshi",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=1,AddTime=DateTime.Now,IsAdmin=true,Operate=2},
                new ManagerModel{ID=4,LoginName="ceshi1",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=2,AddTime=DateTime.Now,IsAdmin=true,Operate=3},
                new ManagerModel{ID=5,LoginName="ceshi2",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=2,AddTime=DateTime.Now,IsAdmin=true,Operate=0},
                new ManagerModel{ID=6,LoginName="ceshi3",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=1,AddTime=DateTime.Now,IsAdmin=true,Operate=0},
                new ManagerModel{ID=7,LoginName="ceshi4",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=2,AddTime=DateTime.Now,IsAdmin=true,Operate=0},
                new ManagerModel{ID=8,LoginName="ceshi5",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=1,AddTime=DateTime.Now,IsAdmin=true,Operate=0},
                new ManagerModel{ID=9,LoginName="ceshi6",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=1,AddTime=DateTime.Now,IsAdmin=true,Operate=0},
                new ManagerModel{ID=10,LoginName="ceshi7",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=2,AddTime=DateTime.Now,IsAdmin=true,Operate=0},
                new ManagerModel{ID=11,LoginName="ceshi8",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=1,AddTime=DateTime.Now,IsAdmin=true,Operate=0},
                new ManagerModel{ID=12,LoginName="ceshi9",ManagerName="ceshi",Phone="16666666666",Password="123456",AuthorityID=1,AddTime=DateTime.Now,IsAdmin=true,Operate=0},
            };
            ManagerList.ForEach(s => context.Manager.Add(s));
            context.SaveChanges();


        }
    }
}