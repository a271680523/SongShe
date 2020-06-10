using Model;
using System;
using System.Linq;
using Common;
using System.Data.Entity;

namespace DAL
{
    [Localization]
    // ReSharper disable once InconsistentNaming
    public class ManagerDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        public ManagerDAL()
        {
            
        }

        /// <summary>
        /// 获取管理员视图集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_ManagerMOD> GetList_v()
        {
            var data = from m in db.Manager
                       join a in db.Authority on m.AuthorityID equals a.AuthorityID into t1
                       from ma in t1.DefaultIfEmpty()
                       join m1 in db.Manager on m.Operate equals m1.ID into t2
                       from mam in t2.DefaultIfEmpty()
                       join tz in db.TimeZone on m.TimeZone equals tz.ID into t3
                       from tzt in t3.DefaultIfEmpty()
                       orderby m.ID descending
                       select new v_ManagerMOD
                       {
                           ID = m.ID,
                           LoginName = m.LoginName,
                           ManagerName = m.ManagerName,
                           Phone = m.Phone,
                           Address = m.Address,
                           Email = m.Email,
                           EnglishName = m.EnglishName,
                           LandlinePhone = m.LandlinePhone,
                           Skype = m.Skype,
                           Wechat = m.Wechat,
                           AuthorityID = m.AuthorityID,
                           AddTime = m.AddTime,
                           IsAdmin = m.IsAdmin,
                           Operate = m.Operate,
                           TimeZone = m.TimeZone,
                           AuthorityName = ma.Name,
                           OperateName = mam.ManagerName,
                           timeZone = tzt,
                       };
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取管理员集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<ManagerModel> GetList()
        {
            return db.Manager.AsQueryable();
        }

        /// <summary>
        /// 添加或修改管理员信息
        /// </summary>
        public Tuple<int, string> AeModelLoginInfo(int id, string loginName, string managerName, string englishName, string phone, string password, int authorityId, bool isAdmin, int timeZoneId, int operateId)
        {
            try
            {
                ManagerModel model = GetMod(id) ?? new ManagerModel();
                if (id > 0 && model.ID <= 0)
                    return new Tuple<int, string>(1, "选择修改的管理员信息不存在");
                if (GetList().Any(d => d.LoginName.Equals(loginName) && d.ID != model.ID))
                    return new Tuple<int, string>(1, "已存在该登陆账号");
                model.LoginName = loginName;
                model.ManagerName = managerName;
                model.EnglishName = englishName;
                model.Phone = phone;
                model.AuthorityID = authorityId;
                if (model.ID <= 0)
                    model.Password = password;
                TimeZoneModel timeZone = new TimeZoneDAL().GetModel(timeZoneId);
                if (timeZone == null)
                    return new Tuple<int, string>(1, "请选择所属时区");
                model.TimeZone = timeZoneId;
                model.IsAdmin = isAdmin;
                Tuple<bool, string> validInfo = ValidModel(model);
                if (!validInfo.Item1)
                {
                    return new Tuple<int, string>(1, validInfo.Item2);
                }
                if (!password.IsNullOrEmpty())
                {
                    if (password.Length < 6)
                        return new Tuple<int, string>(1, "请输入至少六位数以上的登陆密码");
                    model.Password = RSAHelper.Encrypt(password);
                }
                if (model.ID <= 0)
                {
                    model.AddTime = DateTime.UtcNow;
                    model.Operate = operateId;
                    db.Manager.Add(model);
                    db.SaveChanges();
                    ManagerLogDAL.AddManagerLog("添加管理员信息", model.ToJson(), ManagerLogType.Manager, operateId);
                    return new Tuple<int, string>(0, "添加管理员信息成功");
                }
                else
                {
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    ManagerLogDAL.AddManagerLog("修改管理员信息", model.ToJson(), ManagerLogType.Manager, operateId);
                    return new Tuple<int, string>(0, "修改管理员信息成功");
                }
            }
            catch (Exception err)
            {
                return new Tuple<int, string>(1, "数据保存失败," + err.Message);
            }
        }

        /// <summary>
        /// 修改管理员基本信息
        /// </summary>
        /// <param name="id">管理员ID</param>
        /// <param name="managerName">中文名</param>
        /// <param name="englishName">英文名</param>
        /// <param name="phone">移动电话</param>
        /// <param name="landlinePhone">座机电话</param>
        /// <param name="skype">Skype</param>
        /// <param name="wechat">微信号</param>
        /// <param name="email">邮箱</param>
        /// <param name="address">联系地址</param>
        /// <returns></returns>
        public Tuple<int, string> EditModelBaseInfo(int id, string managerName, string englishName, string phone,
            string landlinePhone, string skype, string wechat, string email, string address)
        {
            try
            {
                ManagerModel model = GetMod(id);
                if (model == null)
                    return new Tuple<int, string>(1, "管理员信息不存在");
                if (managerName.IsNullOrWhiteSpace())
                    return new Tuple<int, string>(1, "请输入中文名");
                if (englishName.IsNullOrWhiteSpace())
                    return new Tuple<int, string>(1, "请输入英文名");
                if ((!email.IsNullOrWhiteSpace()) && (!email.IsEmail()))
                    return new Tuple<int, string>(1, "请输入正确的邮箱号");
                model.ManagerName = managerName;
                model.EnglishName = englishName;
                model.Phone = phone ?? "";
                model.LandlinePhone = landlinePhone ?? "";
                model.Skype = skype ?? "";
                model.Wechat = wechat ?? "";
                model.Email = email ?? "";
                model.Address = address ?? "";
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return new Tuple<int, string>(0, "基本信息修改保存成功");
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(1, "基本信息保存失败，" + ex.Message);
            }
        }

        /// <summary>
        /// 获取管理员信息(视图)
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public v_ManagerMOD GetMOD_v(int managerId)
        {
            var data = GetList_v();
            return data.FirstOrDefault(d => d.ID == managerId);
        }
        /// <summary>
        /// 获取管理员信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ManagerModel GetMod(int id)
        {
            return db.Manager.FirstOrDefault(d => d.ID == id);
        }
        /// <summary>
        /// 根据登录名获取管理员信息
        /// </summary>
        /// <param name="loginName">登录名，账号和手机号均可</param>
        /// <returns></returns>
        public ManagerModel GetModelByLoginName(string loginName)
        {
            return db.Manager.FirstOrDefault(d => d.LoginName.Equals(loginName) || d.Phone.Equals(loginName));
        }

        /// <summary>
        /// 验证实体数据
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        private Tuple<bool, string> ValidModel(ManagerModel manager)
        {
            bool isValid = false;
            string validMsg = "";
            if (manager == null)
                validMsg = "当前无数据";
            else if (manager.LoginName.IsNullOrWhiteSpace())
                validMsg = "请输入登陆账号";
            else if (manager.ManagerName.IsNullOrWhiteSpace())
                validMsg = "请输入管理员中文名";
            else if (manager.EnglishName.IsNullOrWhiteSpace())
                validMsg = "请输入管理员英文名";
            else if (manager.Phone.IsNullOrWhiteSpace())
                validMsg = "请输入管理员手机号";
            else if (manager.ID <= 0 && manager.Password.IsNullOrEmpty())
                validMsg = "请输入管理员登陆密码";
            else
                isValid = true;
            return new Tuple<bool, string>(isValid, validMsg);
        }
        /// <summary>
        /// 删除管理员信息
        /// </summary>
        /// <param name="id">管理员Id</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteManager(int id)
        {
            ManagerModel managerModel = new ManagerDAL().GetMod(id);
            if (managerModel == null)
                return new Tuple<int, string>(1, "管理员信息不存在或已删除");
            db.Entry(managerModel).State = EntityState.Deleted;
            db.SaveChanges();
            return new Tuple<int, string>(0, "管理员信息删除成功");
        }
    }
}
