using Common;
using Model;
///////////////////////////////////////////////////////////////////
//CreateTime	2018-2-8 14:49:08
//CreateBy 		唐翔
//Content       学生信息数据处理类
//////////////////////////////////////////////////////////////////
using System;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// 学生信息数据处理类
    /// </summary>
    public class StudentDal : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取学生集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<StudentMOD> GetList()
        {
            var data = from s in db.Student
                       select s;
            return data;
        }

        /// <summary>
        /// 获取学生视图集合
        /// </summary>
        /// <param name="supervisorId">主管老师ID</param>
        /// <param name="startTime">开始注册时间</param>
        /// <param name="endTime">结束注册时间</param>
        /// <param name="search">关键字搜索</param>
        /// <param name="status">学生状态</param>
        /// <returns></returns>
        public IQueryable<v_StudentMOD> GetStudentDataList_v(int supervisorId = 0, DateTime? startTime = null, DateTime? endTime = null, string search = "", int status = -100)
        {
            var nowUtcTime = DateTime.UtcNow;
            var data = from s in db.Student
                       join m in db.Manager on s.ManagerID equals m.ID into t1
                       from m1 in t1.DefaultIfEmpty()
                       join sup in db.Manager on s.Supervisor equals sup.ID into t2
                       from sup1 in t2.DefaultIfEmpty()
                       join sm in db.Manager on s.SellerManager equals sm.ID into t4
                       from smt in t4.DefaultIfEmpty()
                       join cc in db.Manager on s.CourseConsultant equals cc.ID into t5
                       from cct in t5.DefaultIfEmpty()
                       join tz in db.TimeZone on s.TimeZone equals tz.ID into t3
                       from tzt in t3.DefaultIfEmpty()
                       join lm in db.Manager on s.LastEditManagerID equals lm.ID into t6
                       from lmt in t6.DefaultIfEmpty()
                       orderby s.ID descending
                       select new v_StudentMOD
                       {
                           ID = s.ID,
                           AddTime = s.AddTime,
                           ChinaName = s.ChinaName,
                           EnglishName = s.EnglishName,
                           LoginName = s.LoginName,
                           ManagerID = s.ManagerID,
                           Phone = s.Phone,
                           Supervisor = s.Supervisor,
                           TimeZone = s.TimeZone,
                           ManagerName = m1.ManagerName,
                           IsFreeProbation = s.IsFreeProbation,
                           FreeProbationCount = s.FreeProbationCount,
                           SupervisorName = sup1.ManagerName,
                           SellerManager = s.SellerManager,
                           SellerManagerName = smt.ManagerName,
                           CourseConsultant = s.CourseConsultant,
                           CourseConsultantName = cct.ManagerName,
                           Producting = (from sp in db.StudentProduct where sp.StudentID == s.ID select new v_StudentMOD.StudentProductModel { ID = sp.ID, ProductStatus = sp.ProductStatus, ProductName = sp.ProductName, EndTime = sp.EndDate, StartRechargePromptTime = sp.StartRechargePromptTime, EndRechargePromptTime = sp.EndRechargePromptTime }).OrderBy(sp => sp.ProductStatus).FirstOrDefault(),
                           InstallmentProductCount = (from sp in db.StudentProduct where sp.StudentID == s.ID && sp.IsInstallment && sp.UnliquidatedMoney > 0 select new { sp.ID }).Count(),//分期产品数量
                           LastCourseTime = (DateTime?)(from sp in db.StudentCourseRecord where sp.StudentID == s.ID select sp).OrderByDescending(sc => sc.StartTime).FirstOrDefault().StartTime,//最后一次上课时间
                           SuspendSchooling = (from ss in db.StudentSuspendSchoolingRecord where ss.StartTime <= DateTime.UtcNow && ss.EndTime >= DateTime.UtcNow select ss).FirstOrDefault(),//休学记录
                           timeZone = tzt,
                           s_WorkPhone = s.s_WorkPhone,
                           s_HomePhone = s.s_HomePhone,
                           s_FamilyPhone = s.s_FamilyPhone,
                           s_WorkEmail = s.s_WorkEmail,
                           s_BackupEmail = s.s_BackupEmail,
                           s_FamilyEmail = s.s_FamilyEmail,
                           s_Skype = s.s_Skype,
                           s_Wechat = s.s_Wechat,
                           Country = s.Country,
                           Six = s.Six,
                           Birthday = s.Birthday,
                           TabooTopic = s.TabooTopic,
                           LearningAttitude = s.LearningAttitude,
                           IsNonage = s.IsNonage,
                           LearningMethod = s.LearningMethod,
                           LearningContent = s.LearningContent,
                           Vitae = s.Vitae,
                           City = s.City,
                           Hobbies = s.Hobbies,
                           IsToChina = s.IsToChina,
                           FamilyRelationship = s.FamilyRelationship,
                           FamilyName = s.FamilyName,
                           NativeLanguage = s.NativeLanguage,
                           LearningNeeds = s.LearningNeeds,
                           BackupEmail = s.BackupEmail,
                           Character = s.Character,
                           Email = s.Email,
                           FamilyEmail = s.FamilyEmail,
                           Skype = s.Skype,
                           FamilyPhone = s.FamilyPhone,
                           HomePhone = s.HomePhone,
                           Wechat = s.Wechat,
                           WorkEmail = s.WorkEmail,
                           WorkPhone = s.WorkPhone,
                           CourseManagerRemark = s.CourseManagerRemark,
                           CourseManagerTheDemand = s.CourseManagerTheDemand,
                           LastEditManagerID = s.LastEditManagerID,
                           LastEditTime = s.LastEditTime,
                           LearningAbility = s.LearningAbility,
                           LastEditManagerName = lmt.ManagerName,
                           RecommendStudentId = s.RecommendStudentId,
                           RecommendStudentLoginName = s.RecommendStudentId > 0 ? db.Student.FirstOrDefault(d => d.ID.Equals(s.RecommendStudentId)).LoginName ?? "" : "",
                           CourseConsultantSupervisorId = s.CourseConsultantSupervisorId,
                           CourseConsultantSupervisorManagerName = s.CourseConsultantSupervisorId > 0 ? db.Manager.FirstOrDefault(d => d.ID.Equals(s.CourseConsultantSupervisorId)).ManagerName : null,
                           FtEmailContent = s.FtEmailContent,
                           FtLanguageLevel = s.FtLanguageLevel,
                           FtLearningNeeds = s.FtLearningNeeds,
                           FtEditTime = s.FtEditTime,
                           FtEditManagerId = s.FtEditManagerId,
                           FtEditManagerName = s.FtEditManagerId > 0 ? db.Manager.FirstOrDefault(d => d.ID.Equals(s.FtEditManagerId)).ManagerName : ""
                       };
            if (startTime != null)
                data = data.Where(d => d.AddTime >= startTime);
            if (endTime != null)
                data = data.Where(d => d.AddTime <= endTime);
            if (supervisorId > 0)
            {
                data = data.Where(d => d.Supervisor.Equals(supervisorId));
            }
            if (!search.IsNullOrWhiteSpace())
                data = data.Where(d => d.LoginName.Contains(search) || d.Phone.Contains(search) || d.ChinaName.Contains(search) || d.EnglishName.Contains(search));
            if (status != -100)
            {
                switch ((Keys.StduentStatus)status)
                {
                    case Keys.StduentStatus.NoFreeProbation://注册未试课
                        data = data.Where(s => s.Producting == null && s.IsFreeProbation.Equals(false));
                        break;
                    case Keys.StduentStatus.FreeProbationAndNoProduct://试课未购买
                        data = data.Where(s => s.IsFreeProbation.Equals(true) && s.Producting == null);
                        break;
                    case Keys.StduentStatus.SuspendSchooling://休学中
                        data = data.Where(s => s.SuspendSchooling != null);
                        break;
                    case Keys.StduentStatus.LongTimeNoCourse://长期不上课
                        DateTime longTimeNoCourseTime = DateTime.UtcNow.AddDays(-SystemParameDAL.GetSystemParameValue(Keys.SystemParameId.LongTimeNoCourseId));
                        data = data.Where(s => s.LastCourseTime <= longTimeNoCourseTime);
                        break;
                    case Keys.StduentStatus.ProductEnd://续费期,学生最近的产品剩余时间不超过20%到超出结束时间四周时间内为续费期
                        data = data.Where(s => s.Producting != null && s.Producting.StartRechargePromptTime <= nowUtcTime && s.Producting.EndRechargePromptTime >= nowUtcTime);
                        break;
                    case Keys.StduentStatus.Installmenting://分期未付款
                        data = data.Where(s => s.InstallmentProductCount > 0);
                        break;
                    default:
                        data = data.Where(d => d.Producting.ProductStatus == status);
                        break;
                }
            }
            return data;
        }

        /// <summary>
        /// 保存修改的学生基础信息
        /// </summary>
        /// <param name="model">要修改的学生实体信息</param>
        /// <param name="operateId">操作员ID</param>
        /// <param name="operateType">操作员类型<para>0超级管理员 1主管老师 2课程顾问</para></param>
        /// <returns></returns>
        public Tuple<int, string> EditBaseInfoSave(StudentMOD model, int operateId, int operateType)
        {
            StudentMOD student = GetMod(model.ID);
            if (student == null || student.ID <= 0)
                return new Tuple<int, string>(1, "学生信息不存在");
            switch (operateType)
            {
                case 0:
                    break;
                case 1:
                    if (student.Supervisor != operateId)
                        return new Tuple<int, string>(1, "非该学生的主管老师，不能修改学生基本信息");
                    break;
                case 2:
                    if (student.CourseConsultant != operateId)
                        return new Tuple<int, string>(1, "非该学生的课程顾问老师，不能修改学生基本信息");
                    break;
                default:
                    return new Tuple<int, string>(1, "权限不足，不能修改学生基本信息");
            }
            //if (model.Email.IsNullOrWhiteSpace() && model.Phone.IsNullOrWhiteSpace())
            //    return new Tuple<int, string>(1, "Please enter a valid phone or email");
            //if (!model.Phone.IsNullOrWhiteSpace())
            //{
            //    if (!model.Phone.IsNumber())
            //        return new Tuple<int, string>(1, "Please enter a valid phone or email");
            //}
            //if (!model.Email.IsNullOrWhiteSpace())
            //{
            //    if (!model.Email.IsEmail())
            //        return new Tuple<int, string>(1, "Please enter a valid phone or email");
            //}
            student.Email = model.Email;
            student.Phone = model.Phone;
            student.ChinaName = model.ChinaName;
            student.EnglishName = model.EnglishName;
            student.Six = model.Six;
            student.Birthday = model.Birthday;
            student.IsNonage = model.IsNonage;
            student.Country = model.Country;
            student.City = model.City;
            student.NativeLanguage = model.NativeLanguage;
            student.IsToChina = model.IsToChina;
            student.FamilyName = model.FamilyName;
            student.Character = model.Character;
            student.LearningAttitude = model.LearningAttitude;
            student.Hobbies = model.Hobbies;
            student.TabooTopic = model.TabooTopic;
            student.FamilyRelationship = model.FamilyRelationship;
            student.Vitae = model.Vitae;
            student.LearningNeeds = model.LearningNeeds;
            student.LearningContent = model.LearningContent;
            student.LearningMethod = model.LearningMethod;
            student.HomePhone = model.HomePhone;
            student.WorkPhone = model.WorkPhone;
            student.FamilyPhone = model.FamilyPhone;
            student.WorkEmail = model.WorkEmail;
            student.Skype = model.Skype;
            student.BackupEmail = model.BackupEmail;
            student.FamilyEmail = model.FamilyEmail;
            student.Wechat = model.Wechat;
            student.s_HomePhone = model.s_HomePhone;
            student.s_WorkPhone = model.s_WorkPhone;
            student.s_FamilyPhone = model.s_FamilyPhone;
            student.s_WorkEmail = model.s_WorkEmail;
            student.s_Skype = model.s_Skype;
            student.s_BackupEmail = model.s_BackupEmail;
            student.s_FamilyEmail = model.s_FamilyEmail;
            student.s_Wechat = model.s_Wechat;
            student.CourseManagerTheDemand = model.CourseManagerTheDemand;
            student.LearningAbility = model.LearningAbility;
            student.CourseManagerRemark = model.CourseManagerRemark;

            student.LastEditManagerID = operateId;
            student.LastEditTime = DateTime.UtcNow;

            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("修改学生信息", student.ToJson(), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "修改学生信息成功");
        }

        /// <summary>
        /// 添加或修改学生账号信息
        /// </summary>
        public Tuple<int, string> AeLoginInfoSave(StudentMOD model, int managerId)
        {
            var bllManager = new ManagerDAL();
            StudentMOD student = new StudentMOD();
            if (model.ID > 0)
            {
                student = GetMod(model.ID);
                if (student == null || student.ID <= 0)
                    return new Tuple<int, string>(1, "This account does not exist.");//当前学生信息不存在，请获取最新数据
            }
            if (model.LoginName.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "登陆账号必填，请输入");
            if (model.Email.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "邮箱必填，请输入");
            if (model.Phone.IsNullOrWhiteSpace())
                model.Phone = "";
            model.LoginName = model.LoginName?.Trim() ?? "";
            model.Phone = model.Phone?.Trim() ?? "";
            model.Email = model.Email?.Trim() ?? "";
            if (!model.LoginName.IsNullOrWhiteSpace())
            {
                if (model.LoginName.Length < 6 || model.LoginName.Length > 18 || (!model.LoginName.IsLettersOrNumberAndIndexLetters()))
                    return new Tuple<int, string>(1, "账号由6-18位字母数字下划线组成,且第一位必须是字母");
                if (db.Student.Any(s => s.LoginName.Equals(model.LoginName) && s.ID != model.ID))
                    return new Tuple<int, string>(1, "登录账号已存在");
            }
            if (!model.Phone.IsNullOrWhiteSpace())
            {
                if (model.Phone.Length > 0 && !model.Phone.IsNumber())
                    return new Tuple<int, string>(1, "请输入正确的手机号");
                if (db.Student.Any(s => s.Phone.Equals(model.Phone) && s.ID != model.ID))
                    return new Tuple<int, string>(1, "手机号已存在");
            }
            if (!model.Email.IsNullOrWhiteSpace())
            {
                if (!model.Email.IsEmail())
                    return new Tuple<int, string>(1, "请输入正确的邮箱号");
                if (db.Student.Any(s => s.Email.Equals(model.Email) && s.ID != model.ID))
                    return new Tuple<int, string>(1, "邮箱号已存在");
            }
            TimeZoneModel timeZone = new TimeZoneDAL().GetModel(model.TimeZone);
            if (timeZone == null || timeZone.ID <= 0)
                return new Tuple<int, string>(1, "请选择所处的时区");

            if (model.ChinaName.IsNullOrWhiteSpace())
                model.ChinaName = model.LoginName;
            //return new Tuple<int, string>(1, "中文名不能为空");
            if (model.EnglishName.IsNullOrWhiteSpace())
                model.EnglishName = model.LoginName;
            //return new Tuple<int, string>(1, "英文名不能为空");
            if (model.Password.IsNullOrWhiteSpace() && model.ID <= 0)
                return new Tuple<int, string>(1, "登陆密码不能为空");
            if (!model.Password.IsNullOrWhiteSpace()&&model.Password.PasswordLevel() < 3)
                return new Tuple<int, string>(1, "登录密码强度不够，需设置成同时包含大写字母小写字母和字母的6-18位字符");
            if (model.Supervisor > 0)
            {
                ManagerModel supervisor = bllManager.GetMod(model.Supervisor);
                if (supervisor == null || supervisor.ID <= 0)
                    return new Tuple<int, string>(1, "主管老师不存在");
            }
            if (model.CourseConsultantSupervisorId > 0)
            {
                ManagerModel courseConsultantSupervisor = bllManager.GetMod(model.CourseConsultantSupervisorId);
                if (courseConsultantSupervisor == null || courseConsultantSupervisor.ID <= 0)
                    return new Tuple<int, string>(1, "课程顾问主管不存在");
            }
            if (model.CourseConsultant > 0)
            {
                ManagerModel courseConsultant = bllManager.GetMod(model.CourseConsultant);
                if (courseConsultant == null || courseConsultant.ID <= 0)
                    return new Tuple<int, string>(1, "课程顾问不存在");
            }
            if (model.SellerManager > 0)
            {
                ManagerModel sellerManager = bllManager.GetMod(model.SellerManager);
                if (sellerManager == null || sellerManager.ID <= 0)
                    return new Tuple<int, string>(1, "学生助理不存在");
            }
            if (model.RecommendStudentId > 0)
            {
                StudentMOD recommendStudent = GetMod(model.RecommendStudentId);
                if (recommendStudent == null)
                    return new Tuple<int, string>(1, "选择的推荐人不存在");
            }
            student.TimeZone = model.TimeZone;
            student.LoginName = model.LoginName;
            student.Phone = model.Phone;
            student.Email = model.Email;
            student.ChinaName = model.ChinaName;
            student.EnglishName = model.EnglishName;
            student.Supervisor = model.Supervisor;
            student.SellerManager = model.SellerManager;
            student.CourseConsultantSupervisorId = model.CourseConsultantSupervisorId;
            student.CourseConsultant = model.CourseConsultant;
            student.RecommendStudentId = model.RecommendStudentId;
            if (!model.Password.IsNullOrWhiteSpace())
                student.Password = RSAHelper.Encrypt(model.Password);
            if (student.ID <= 0)
            {
                student.AddTime = DateTime.UtcNow;
                student.ManagerID = managerId;
                db.Student.Add(student);
                db.SaveChanges();
                ManagerLogDAL.AddManagerLog("添加学生信息成功", model.ToJson(), ManagerLogType.Manager, managerId);
                return new Tuple<int, string>(0, "添加学生信息成功");
            }
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("修改学生登陆信息成功", model.ToJson(), ManagerLogType.Manager, managerId);
            return new Tuple<int, string>(0, "修改学生登陆信息成功");
        }
        /// <summary>
        /// 学生修改基本信息
        /// </summary>
        public Tuple<int, string> EditBaseInfoByStudent(int studentId, int iTimeZone, string email, string phone, string sWorkPhone, string sHomePhone, string sFamilyPhone, string sEmail, string sBackupEmail, string sFamilyEmail, string sSkype, string sWechat)
        {
            TimeZoneModel timeZone = new TimeZoneDAL().GetModel(iTimeZone);
            if (timeZone == null)
            {
                return new Tuple<int, string>(1, "Please seleted a time zone");
            }
            if (email.IsNullOrWhiteSpace() && phone.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "Please enter a valid phone or email");
            if (!phone.IsNullOrWhiteSpace())
            {
                if (phone.Length > 0 && !phone.IsNumber())
                    return new Tuple<int, string>(1, "Please enter a valid phone or email");
            }
            if (!email.IsNullOrWhiteSpace())
            {
                if (!email.IsEmail())
                    return new Tuple<int, string>(1, "Please enter a valid phone or email");
            }
            StudentMOD student = new StudentDal().GetMod(studentId);
            student.Email = email;
            student.Phone = phone;
            student.s_WorkPhone = sWorkPhone;
            student.s_HomePhone = sHomePhone;
            student.s_FamilyPhone = sFamilyPhone;
            student.s_WorkEmail = sEmail;
            student.s_BackupEmail = sBackupEmail;
            student.s_FamilyEmail = sFamilyEmail;
            student.s_Skype = sSkype;
            student.s_Wechat = sWechat;
            student.TimeZone = iTimeZone;
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("学生修改基本信息", "TimeZone:" + timeZone + ",s_WorkPhone:" + sWorkPhone + ",s_HomePhone:" + sHomePhone + ",s_FamilyPhone:" + sFamilyPhone + ",s_Email:" + sEmail + ",s_BackupEmail:" + sBackupEmail + ",s_FamilyEmail:" + sFamilyEmail + ",s_Skype:" + sSkype + ",s_Wechat:" + sWechat, ManagerLogType.Student, studentId);
            return new Tuple<int, string>(0, "Saved");//保存基本信息成功
        }
        /// <summary>
        /// 修改学生FT信息
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="courseConsultant">课程顾问老师ID</param>
        /// <param name="ftEmailContent">邮件内容</param>
        /// <param name="ftLanguageLevel">汉语水平</param>
        /// <param name="ftLearningNeeds">学习需求</param>
        /// <param name="operateId">操作员Id</param>
        /// <returns></returns>
        public Tuple<int, string> EditFtInfo(int studentId, int courseConsultant, string ftEmailContent, string ftLanguageLevel, string ftLearningNeeds, int operateId)
        {
            StudentMOD model = GetMod(studentId);
            if (model == null)
                return new Tuple<int, string>(1, "学生信息不存在");
            if (courseConsultant > 0 && model.CourseConsultant != courseConsultant)
                return new Tuple<int, string>(1, "无权限修改此信息");
            model.FtEmailContent = ftEmailContent;
            model.FtLanguageLevel = ftLanguageLevel;
            model.FtLearningNeeds = ftLearningNeeds;
            model.FtEditManagerId = operateId;
            model.FtEditTime = DateTime.UtcNow;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return new Tuple<int, string>(0, "学生Ft信息修改保存成功");
        }
        /// <summary>
        /// 获取学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StudentMOD GetMod(int id)
        {
            return db.Student.FirstOrDefault(d => d.ID.Equals(id));
        }

        /// <summary>
        /// 获取登录者学生信息
        /// </summary>
        /// <param name="loginName">匹配账号、手机号、邮箱号</param>
        /// <returns></returns>
        public StudentMOD GetLoginMod(string loginName)
        {
            if (loginName.IsNullOrWhiteSpace())
                return null;
            return db.Student.FirstOrDefault(s => s.LoginName == (loginName) || s.Email.Equals(loginName));// || s.Phone.Equals(loginName) || s.Email.Equals(loginName)
        }


        /// <summary>
        /// 获取学生信息(视图)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public v_StudentMOD GetMOD_v(int id)
        {
            return GetStudentDataList_v().FirstOrDefault(d => d.ID.Equals(id));
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="reNewPassword"></param>
        /// <returns></returns>
        public Tuple<int, string> EditPassword(int studentId, string oldPassword, string newPassword, string reNewPassword)
        {
            if (oldPassword.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "Please fill in the current password");
            if (newPassword.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "Please fill in the new password again");
            if (newPassword.PasswordLevel() < 3)
                return new Tuple<int, string>(1, "密码中必须同时包含大写字母、小写字母、数字");
            if (reNewPassword.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "Please fill in the confirm new password again");
            if (newPassword != reNewPassword)
                return new Tuple<int, string>(1, "The password entered twice is different");
            StudentMOD student = GetMod(studentId);
            if (student == null)
                return new Tuple<int, string>(1, "Current information does not exist");
            if (RSAHelper.Encrypt(oldPassword) != student.Password)
                return new Tuple<int, string>(1, "The current password is incorrect");
            student.Password = RSAHelper.Encrypt(newPassword);
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("学生修改密码", "", ManagerLogType.Student, studentId);
            return new Tuple<int, string>(0, "Update password success");
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        public Tuple<int, string> ResetPassword(int studentId, string newPassword)
        {
            if (newPassword.PasswordLevel() < 3)
                return new Tuple<int, string>(1, "密码中必须同时包含大写字母、小写字母、数字");
            StudentMOD student = GetMod(studentId);
            student.Password = RSAHelper.Encrypt(newPassword);
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("学生重置密码", "", ManagerLogType.Student, studentId);
            return new Tuple<int, string>(0, "Reset password success");
        }
    }
}
