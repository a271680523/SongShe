///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-10 16:10:15
//CreateBy 		唐翔
//Content       权限数据处理类
//////////////////////////////////////////////////////////////////
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Data.Entity;
using static Model.v_AuthorityMOD;

namespace DAL
{
    /// <summary>
    /// 权限数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class AuthorityDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        public AuthorityModel GetModel(int id = 0)
        {
            return db.Authority.FirstOrDefault(d => d.AuthorityID == id);
        }
        /// <summary>
        /// 获取权限视图信息
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        public v_AuthorityMOD GetAuthority(int id = 0)
        {
            v_AuthorityMOD authority = new v_AuthorityMOD();
            var model = new AuthorityModel();
            if (id > 0)
                model = db.Authority.FirstOrDefault(d => d.AuthorityID == id);
            var menuExtend = from m in db.Menu
                             join ai in db.AuthorityItem
                             on new { MenuID = m.ID, model.AuthorityID }
                             equals new { ai.MenuID, ai.AuthorityID } into t1
                             from mai in t1
                             select new MenuExtendModel()
                             {
                                 ID = m.ID,
                                 MenuName = m.MenuName,
                                 Grade = m.Grade,
                                 IsAuthority = m.IsAuthority,
                                 ParentID = m.ParentID,
                                 Sort = m.Sort,
                                 LinkUrl = m.LinkUrl,
                                 MenuImg = m.MenuImg,
                             };
            authority.Authority = model;
            authority.MenuExtend = menuExtend.Distinct().OrderByDescending(m => m.Sort).ToList();
            authority.MenuActionInfoList = db.MenuActionInfo.Where(d => menuExtend.Any(m => m.ID == d.MenuId)).ToList();
            return authority;
        }
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public IQueryable<AuthorityModel> GetList()
        {
            return db.Authority.AsQueryable();
        }

        /// <summary>
        /// 添加或修改权限信息
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <param name="name">权限名称</param>
        /// <param name="menuIdList">菜单ID集合</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns></returns>
        public Tuple<int, string> AeAuthority(int id, string name, List<int> menuIdList, int operateId)
        {
            if (menuIdList == null || menuIdList.Count <= 0)
                return new Tuple<int, string>(1, "请选择要拥有的权限");
            AuthorityModel model = new AuthorityModel();
            if (id > 0)
            {
                model = GetModel(id);
                if (model == null || model.AuthorityID <= 0)
                    return new Tuple<int, string>(1, "选择修改的权限信息不存在");
            }
            model.Name = name;
            if (model.Name.IsNullOrWhiteSpace())
            {
                return new Tuple<int, string>(1, "权限名称不能为空，请重新输入");
            }
            List<AuthorityItemModel> authorityItemList = new List<AuthorityItemModel>();
            foreach (var menuId in menuIdList)
            {
                if (menuId > 0)
                {
                    MenuModel menu = new MenuDal().GetModel(menuId); ;
                    if (menu != null && menu.ID > 0 && authorityItemList.All(ai => ai.MenuID != menu.ID))
                    {
                        if (menu.ParentID > 0 && authorityItemList.All(ai => ai.MenuID != menu.ParentID))
                            authorityItemList.Add(new AuthorityItemModel() { MenuID = menu.ParentID, AuthorityID = model.AuthorityID, AddTime = DateTime.UtcNow });
                        authorityItemList.Add(new AuthorityItemModel() { MenuID = menu.ID, AuthorityID = model.AuthorityID, AddTime = DateTime.UtcNow });
                    }
                }
            }
            if (model.AuthorityID <= 0)
            {
                model.AddTime = DateTime.UtcNow;
                model = db.Authority.Add(model);
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
            }
            else
            {
                db.Entry(model).State = EntityState.Modified;
            }
            db.AuthorityItem.RemoveRange(db.AuthorityItem.Where(m => m.AuthorityID.Equals(model.AuthorityID)));
            foreach (var authorityItem in authorityItemList)
            {
                authorityItem.AuthorityID = model.AuthorityID;
                authorityItem.AddTime = DateTime.UtcNow;
                authorityItem.ID = db.AuthorityItem.Add(authorityItem).ID;
            }
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog((id > 0 ? "修改" : "添加") + "权限信息", "Authority:" + model.ToJson() + ";AuthorityItem:" + authorityItemList.ToJson(), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "修改权限信息成功");
        }
        /// <summary>
        /// 删除权限信息
        /// </summary>
        /// <param name="id">权限ID</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteModel(int id)
        {
            AuthorityModel model = GetModel(id);
            if (model == null)
                return new Tuple<int, string>(1, "权限信息不存在或已删除");
            db.Authority.Remove(model);
            db.AuthorityItem.RemoveRange(db.AuthorityItem.Where(ai => ai.AuthorityID.Equals(model.AuthorityID)));
            db.SaveChanges();
            return new Tuple<int, string>(0, "权限信息删除成功");
        }
    }
}
