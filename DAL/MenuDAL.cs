using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Model;
using Common;

namespace DAL
{
    /// <summary>
    /// 菜单处理类
    /// </summary>
    public class MenuDal : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取菜单集合
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetMenuList()
        {
            return db.Menu.ToList();
        }
        /// <summary>
        /// 获取菜单视图集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_MenuModel> GetList_v()
        {
            var data = from m in db.Menu
                       join m1 in db.Menu on m.ParentID equals m1.ID into t1
                       from mm1 in t1.DefaultIfEmpty()
                       select new v_MenuModel()
                       {
                           ID = m.ID,
                           MenuName = m.MenuName,
                           ParentID = m.ParentID,
                           ParentName = mm1.MenuName ?? "",
                           LinkUrl = m.LinkUrl,
                           Grade = m.Grade,
                           Sort = m.Sort,
                           IsAuthority = m.IsAuthority,
                           MenuImg = m.MenuImg
                       };
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取菜单实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuModel GetModel(int id)
        {
            return db.Menu.FirstOrDefault(d => d.ID == id);
        }
        /// <summary>
        /// 添加或修改菜单信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public Tuple<int, string> AeModel(MenuModel model, List<int> actionIdList, int operateId)
        {
            try
            {
                MenuModel menu = new MenuModel();
                if (model.ID > 0)
                {
                    menu = GetModel(model.ID);
                    if (menu == null || menu.ID <= 0)
                        return new Tuple<int, string>(1, "选择修改的菜单信息不存在");
                }
                menu.MenuName = model.MenuName;
                menu.LinkUrl = model.LinkUrl;
                menu.ParentID = model.ParentID;
                menu.Sort = model.Sort;
                //处理菜单所拥有的操作方法
                if (menu.MenuActionInfoList == null)
                    menu.MenuActionInfoList = new List<MenuActionInfoModel>();
                menu.MenuActionInfoList.RemoveAll(d => d.ActionInfoId > 0);
                if (actionIdList?.Count > 0)
                {
                    menu.MenuActionInfoList = new List<MenuActionInfoModel>();
                    foreach (var actionId in actionIdList)
                    {
                        if (!menu.MenuActionInfoList.Any(d => d.ActionInfoId == actionId))
                        {
                            MenuActionInfoModel menuAction = new MenuActionInfoModel()
                            {
                                MenuId = menu.ID,
                                ActionInfoId = actionId
                            };
                            menu.MenuActionInfoList.Add(menuAction);
                        }
                    }
                }
                Tuple<bool, string> validInfo = ValidModel(menu);
                if (!validInfo.Item1)
                    return new Tuple<int, string>(1, validInfo.Item2);
                menu.Grade = menu.ParentID > 0 ? 2 : 1;
                if (menu.ID <= 0)
                    menu = db.Menu.Add(menu);
                else
                    db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                ManagerLogDAL.AddManagerLog((menu.ID > 0 ? "修改" : "添加") + "菜单信息", menu.ToJson(), ManagerLogType.Manager, operateId);
                return new Tuple<int, string>(0, (menu.ID > 0 ? "修改" : "添加") + "菜单信息成功");
            }
            catch (Exception err)
            {
                return new Tuple<int, string>(1, "数据保存失败," + err.Message);
            }
        }
        /// <summary>
        /// 验证菜单信息
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        private Tuple<bool, string> ValidModel(MenuModel menu)
        {
            bool isValid = false;
            string validMsg = "";
            if (menu.MenuName.IsNullOrWhiteSpace())
                validMsg = "菜单名称不能为空";
            else
                isValid = true;
            if (isValid && menu.ParentID > 0)
            {
                MenuModel parentMenu = GetModel(menu.ParentID);
                if (parentMenu == null || parentMenu.ID <= 0)
                {
                    isValid = false;
                    validMsg = "选择的上级菜单不存在";
                }
            }
            return new Tuple<bool, string>(isValid, validMsg);
        }
        /// <summary>
        /// 获取程序操作方法入口信息集合
        /// </summary>
        /// <param name="parentId">上级操作方法ID</param>
        /// <returns></returns>
        public IQueryable<ActionInfoModel> GetActionInfoList(int? parentId = null)
        {
            var data = from a in db.ActionInfo
                       orderby a.Id
                       select a;
            return data;
        }
        /// <summary>
        /// 获取程序操作方法信息实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionInfoModel GetActionInfo(int id)
        {
            var data = GetActionInfoList();
            return data.FirstOrDefault(d => d.Id == 0);
        }

        //public Tuple<int, string> EditActionInfo(int id,string name,string remark,int parentId)
        //{

        //}
    }
}
