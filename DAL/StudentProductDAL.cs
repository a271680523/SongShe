using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    /// <summary>
    /// 学生产品数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class StudentProductDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取学生产品列表
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public IQueryable<StudentProductMOD> GetList(int studentId = 0)
        {
            var data = db.StudentProduct.AsQueryable();
            if (studentId > 0)
                data = data.Where(d => d.StudentID.Equals(studentId));
            return data;
        }

        /// <summary>
        /// 获取学生产品视图列表
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public IQueryable<v_StudentProductMOD> GetStudentProductList_v(int studentId = 0)
        {
            var data = from sp in db.StudentProduct
                       orderby sp.ID
                       select new v_StudentProductMOD
                       {
                           AddTime = sp.AddTime,
                           EndDate = sp.EndDate,
                           Frequency = sp.Frequency,
                           ID = sp.ID,
                           IsInstallment = sp.IsInstallment,
                           LeaveCount = sp.LeaveCount,
                           LimitDate = sp.LimitDate,
                           PriceMoney = sp.PriceMoney,
                           ProductID = sp.ProductID,
                           ProductName = sp.ProductName,
                           ProductStatus = sp.ProductStatus,
                           RestOfCourseCount = sp.RestOfCourseCount,
                           StartDate = sp.StartDate,
                           StudentID = sp.StudentID,
                           TotalCourseCount = sp.TotalCourseCount,
                           UnliquidatedMoney = sp.UnliquidatedMoney,
                           StartRechargePromptTime = sp.StartRechargePromptTime,
                           EndRechargePromptTime = sp.EndRechargePromptTime,
                           UseLeaveCount = sp.UseLeaveCount,
                           CancelProductRemark = sp.CancelProductRemark,
                           InstallmentList = db.InstallmentPayInfo.Where(d => d.StudentProductId == sp.ID).OrderBy(d => d.PayStatus).ThenBy(d => d.PayDate).ToList(),
                           SuspendSchoolingRecordList = db.StudentSuspendSchoolingRecord.Where(s => s.ProductID.Equals(sp.ID))
                       };
            if (studentId > 0)
            {
                data = data.Where(d => d.StudentID.Equals(studentId));
            }
            return data.AsQueryable();
        }
        /// <summary>
        /// 获取学生产品
        /// </summary>
        /// <param name="id">学生产品ID</param>
        /// <returns></returns>
        public StudentProductMOD GetStudentProduct(int id)
        {
            return db.StudentProduct.FirstOrDefault(d => d.ID == id);
        }
        /// <summary>
        /// 获取学生产品视图信息
        /// </summary>
        /// <param name="id">学生产品ID</param>
        /// <returns></returns>
        public v_StudentProductMOD GetModel_v(int id)
        {
            return GetStudentProductList_v().FirstOrDefault(d => d.ID.Equals(id));
        }

        /// <summary>
        /// 获取学生在读的产品
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public StudentProductMOD GetStudentProductByUsing(int studentId)
        {
            return db.StudentProduct.FirstOrDefault(p => p.StudentID.Equals(studentId) && p.ProductStatus.Equals((int)Keys.StudentProductStatus.Using));
        }
        /// <summary>
        /// 获取学生在读的产品视图数据
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <returns></returns>
        public v_StudentProductMOD GetStudentProductByUsing_v(int studentId)
        {
            return GetStudentProductList_v(studentId).FirstOrDefault(p => p.ProductStatus.Equals((int)Keys.StudentProductStatus.Using));
        }

        /// <summary>
        /// 添加学生产品信息
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="productId">产品ID</param>
        /// <param name="productName">产品名称</param>
        /// <param name="frequency">频率（周）</param>
        /// <param name="limitDate">学籍时长(周)</param>
        /// <param name="totalCourseCount">总课时</param>
        /// <param name="leaveCount">可休时长(周)</param>
        /// <param name="priceMoney">产品价格</param>
        /// <param name="isInstallment">是否分期</param>
        /// <param name="installmentList">分期记录详情</param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public Tuple<int, string> AddProduct(int studentId, int productId, string productName, int frequency, int limitDate, int totalCourseCount, int leaveCount, decimal priceMoney, bool isInstallment, List<InstallmentPayInfoModel> installmentList, int operateId)
        {
            var operateManager = new ManagerDAL().GetMOD_v(operateId);
            if (operateManager == null)
                return new Tuple<int, string>(1, "操作员信息不存在，请重新登录尝试");
            StudentMOD student = new StudentDal().GetMod(studentId);
            if (student == null || student.ID <= 0)
                return new Tuple<int, string>(1, "学生信息不存在");
            ProductMOD product = new ProductDAL().GetModel(productId);
            StudentProductMOD studentProduct = new StudentProductMOD();
            if (productId > 0 && product == null)
                return new Tuple<int, string>(1, "选择的产品不存在，请获取最新数据");
            if (productName.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "产品名称不能为空");
            if (frequency < 0)
                return new Tuple<int, string>(1, "请填写大于等于0的每周频率值");
            if (limitDate <= 0)
                return new Tuple<int, string>(1, "请填写大于0的学籍时长");
            if (totalCourseCount <= 0)
                return new Tuple<int, string>(1, "请填写大于0的总课时");
            if (leaveCount < 0)
                return new Tuple<int, string>(1, "请填写大于等于0的可休学时长");
            if (priceMoney < 0)
                return new Tuple<int, string>(1, "请填写大于等于0的产品价格");
            if (installmentList == null)
                installmentList = new List<InstallmentPayInfoModel>();
            decimal unliquidatedMoney = 0;//分期未结清金额
            if (isInstallment) //分期信息处理
            {
                if (installmentList.Count <= 0)
                    return new Tuple<int, string>(1, "分期产品至少需要一条分期记录信息");
                var previousPayDate = DateTime.UtcNow.Date;
                var previousSelectCourseDate = DateTime.UtcNow.Date;
                foreach (var installment in installmentList)
                {
                    //转换操作员时区的时间为UTC时间
                    installment.PayDate = installment.PayDate.ConvertUtcTime(operateManager.timeZone.TimeZoneInfoId);
                    installment.SelectCourseDate = installment.SelectCourseDate.ConvertUtcTime(operateManager.timeZone.TimeZoneInfoId);
                    //判断分期参数是否合理
                    if (installment.PayMoney <= 0)
                        return new Tuple<int, string>(1, "分期详情中的支付金额" + installment.PayMoney + "不是有效的金额，必须是大于0的金额");
                    if (installment.PayDate <= previousPayDate)
                        return new Tuple<int, string>(1,
                            "分期详情中的支付时间" + installment.PayDate + "不是有效的时间，必须大于当前时间且大于上一期的支付时间");
                    if (installment.SelectCourseDate <= previousSelectCourseDate)
                        return new Tuple<int, string>(1,
                            "分期详情中的限制选课时间" + installment.SelectCourseDate + "不是有效的时间，必须大于当前时间且大于上一期的限制选课时间");
                    previousPayDate = installment.PayDate;
                    previousSelectCourseDate = installment.SelectCourseDate;
                    unliquidatedMoney += installment.PayMoney;
                }
                if (priceMoney < unliquidatedMoney)
                    return new Tuple<int, string>(1, "分期金额总额不能超过产品金额");
            }
            else
                installmentList = new List<InstallmentPayInfoModel>();
            studentProduct.StudentID = studentId;
            studentProduct.ProductID = productId;
            studentProduct.ProductName = productName;
            studentProduct.Frequency = frequency;
            studentProduct.LimitDate = limitDate;
            studentProduct.TotalCourseCount = totalCourseCount;
            studentProduct.RestOfCourseCount = totalCourseCount;
            studentProduct.LeaveCount = leaveCount * 7;
            studentProduct.PriceMoney = priceMoney;
            studentProduct.ProductStatus = (int)Keys.StudentProductStatus.NoUsed;
            studentProduct.AddTime = DateTime.UtcNow;
            studentProduct.StartDate = studentProduct.AddTime;
            studentProduct.EndDate = studentProduct.AddTime.AddDays(studentProduct.LimitDate * 7);
            studentProduct.StartRechargePromptTime = studentProduct.EndDate;
            studentProduct.EndRechargePromptTime = studentProduct.EndDate;
            studentProduct.IsInstallment = isInstallment;
            //studentProduct.InstallmentList = installmentList;
            studentProduct.UnliquidatedMoney = unliquidatedMoney;
            db.Entry(studentProduct).State = EntityState.Added;
            db.SaveChanges();
            foreach (var installment in installmentList)
            {
                installment.StudentProductId = studentProduct.ID;
                db.Entry(installment).State = EntityState.Added;
            }
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("添加学生产品信息", "学生账号：" + student.LoginName + ",产品信息：" + studentProduct.ToJson(), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "学生账号" + student.LoginName + "添加产品成功");
        }
        /// <summary>
        /// 删除学生产品
        /// </summary>
        /// <param name="id">学生产品ID</param>
        /// <param name="managerId">操作管理员ID</param>
        /// <returns></returns>
        public Tuple<int, string> Delete(int id, int managerId)
        {
            StudentProductMOD studentProduct = GetStudentProduct(id);
            if (studentProduct == null)
                return new Tuple<int, string>(1, "当前学生产品信息不存在或已删除,请获取最新数据");
            if (studentProduct.ProductStatus != (int)Keys.StudentProductStatus.NoUsed || studentProduct.TotalCourseCount != studentProduct.RestOfCourseCount || studentProduct.UseLeaveCount > 0)
                return new Tuple<int, string>(1, "当前学生产品已使用，不能进行删除");
            //删除分期记录
            var installmentList = db.InstallmentPayInfo.Where(d => d.StudentProductId == studentProduct.ID).ToList();
            foreach (var installment in installmentList)
            {
                db.Entry(installment).State = EntityState.Deleted;
            }
            db.Entry(studentProduct).State = EntityState.Deleted;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("删除学生产品", "学生产品信息:" + studentProduct.ToJson(), ManagerLogType.Manager, managerId);
            return new Tuple<int, string>(0, "删除学生产品信息成功");
        }

        /// <summary>
        /// 取消学生产品(退学)
        /// </summary>
        /// <param name="id">学生产品ID</param>
        /// <param name="cancelProductRemark"></param>
        /// <param name="managerId">操作管理员ID</param>
        /// <returns></returns>
        public Tuple<int, string> Cancel(int id, string cancelProductRemark, int managerId)
        {
            try
            {
                StudentProductMOD studentProduct = GetStudentProduct(id);
                if (studentProduct == null)
                    return new Tuple<int, string>(1, "当前学生产品信息不存在或已删除,请获取最新数据");
                if (studentProduct.ProductStatus != (int)Keys.StudentProductStatus.Using && studentProduct.ProductStatus != (int)Keys.StudentProductStatus.NoUsed)
                    return new Tuple<int, string>(1, "当前学生产品还未使用，不能进行退学操作");
                if (cancelProductRemark.IsNullOrWhiteSpace())
                    return new Tuple<int, string>(1, "还未填写退学原因，请先填写");
                DateTime nowUtcTime = DateTime.Now;

                StudentCourseRecordDAL courseDal = new StudentCourseRecordDAL();
                var studentCourseList = db.StudentCourseRecord.Where(d => d.StudentID == studentProduct.StudentID && d.StartTime > nowUtcTime).ToList();
                foreach (var model in studentCourseList)
                {
                    if (model.StartTime > nowUtcTime && model.StudentCourseStatus == 0)
                    {
                        courseDal.CancelStudentCourseRecord(model.ID, studentProduct.StudentID);
                    }
                }
                studentProduct.CancelProductRemark = cancelProductRemark;
                studentProduct.ProductStatus = (int)Keys.StudentProductStatus.Cancel;
                db.SaveChanges();
                ManagerLogDAL.AddManagerLog("学生产品申请退学", "学生产品信息:" + studentProduct.ToJson(), ManagerLogType.Manager, managerId);
                return new Tuple<int, string>(0, "学生产品申请退学成功");
            }
            catch (Exception err)
            {
                ManagerLogDAL.AddManagerLog("学生产品申请退学异常", "学生产品ID:" + id, ManagerLogType.Manager, managerId);
                return new Tuple<int, string>(0, "学生产品申请退学失败，" + err.Message);
            }
        }
        /// <summary>
        /// 更新已过期学生产品状态
        /// </summary>
        /// <returns></returns>
        public Tuple<int, string> UpdateStudentProductStatus()
        {
            int updateCount = 0;
            var nowUtcTime = DateTime.UtcNow;
            var data = GetList().Where(d => d.EndDate <= nowUtcTime && (d.ProductStatus == (int)Keys.StudentProductStatus.Using));
            if (data.Any())
            {
                foreach (var model in data)
                {
                    model.ProductStatus = (int)Keys.StudentProductStatus.UseUp;
                    db.Entry(model).State = EntityState.Modified;
                    updateCount++;
                }
                db.SaveChanges();
            }
            return new Tuple<int, string>(0, "本次已更新" + updateCount + "个过期学生产品状态");
        }
        /// <summary>
        /// 判断是否有逾期未还且超过限制选课时间的产品分期记录
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="time">判断时间，默认为当前时间</param>
        /// <returns></returns>
        public bool IsOverdue(int studentId, DateTime? time = null)
        {
            DateTime overdueTime = DateTime.UtcNow;
            if (time != null)
                overdueTime = (DateTime)time;
            //判断是否有逾期未付款且超过限制选课时间的
            var overdue = from sp in db.StudentProduct
                          where sp.StudentID.Equals(studentId) && sp.IsInstallment
                          && db.InstallmentPayInfo.Any(d => d.StudentProductId == sp.ID && d.PayStatus.Equals(0) && d.PayDate <= overdueTime && d.SelectCourseDate <= overdueTime)
                          select sp;
            return overdue.Any();
            //return Overdue.FirstOrDefault().FirstOrDefault();
        }

        /// <summary>
        /// 添加学生产品的分期记录
        /// </summary>
        /// <param name="studentId">学生ID</param>
        /// <param name="studentProductId">学生产品ID</param>
        /// <param name="payMoney">该期分期金额</param>
        /// <param name="payDate">最迟支付时间</param>
        /// <param name="selectCourseDate">最迟限制选课时间</param>
        /// <param name="operateId">操作管理员</param>
        /// <returns></returns>
        public Tuple<int, string> AddInstallmentInfo(int studentId, int studentProductId, decimal payMoney, DateTime payDate, DateTime selectCourseDate, int operateId)
        {
            var studentProcut = db.StudentProduct.FirstOrDefault(d => d.StudentID == studentId && d.ID == studentProductId && d.IsInstallment);
            if (studentProcut == null)
                return new Tuple<int, string>(1, "学生产品不存在或不属于分期产品");
            var manager = new ManagerDAL().GetMOD_v(operateId);
            if (manager == null)
                return new Tuple<int, string>(1, "当前登录信息丢失，请重新登录");
            InstallmentPayInfoModel installment = new InstallmentPayInfoModel
            {
                PayStatus = 0,
                StudentProductId = studentProcut.ID,
                PayMoney = payMoney,
                PayDate = payDate.ConvertUtcTime(manager.timeZone.TimeZoneInfoId),
                SelectCourseDate = selectCourseDate.ConvertUtcTime(manager.timeZone.TimeZoneInfoId)
            };
            //转换操作员时区的时间为UTC时间
            //判断分期参数是否合理
            if (installment.PayMoney <= 0)
                return new Tuple<int, string>(1, "分期详情中的支付金额" + payMoney + "不是有效的金额，必须是大于0的金额");
            studentProcut.UnliquidatedMoney += installment.PayMoney;
            db.Entry(installment).State = EntityState.Added;
            db.Entry(studentProcut).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("添加分期付款记录", "学生产品分期记录信息:" + installment.ToJson(), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "成功添加分期付款信息");
        }

        /// <summary>
        /// 分期记录改为已结清状态
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentProductId"></param>
        /// <param name="installmentId"></param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public Tuple<int, string> SettleInstallmentInfo(int studentId, int studentProductId, int installmentId, int operateId)
        {
            var studentProcut = db.StudentProduct.FirstOrDefault(d => d.StudentID == studentId && d.ID == studentProductId && d.IsInstallment);
            if (studentProcut == null)
                return new Tuple<int, string>(1, "学生产品不存在或不属于分期产品");
            InstallmentPayInfoModel model = db.InstallmentPayInfo.FirstOrDefault(d => d.StudentProductId == studentProductId && d.Id == installmentId);
            if (model == null || model.PayStatus != 0)
                return new Tuple<int, string>(1, "该分期记录信息不存在或不处于未支付状态");
            model.PayStatus = 1;
            studentProcut.UnliquidatedMoney -= model.PayMoney;
            db.Entry(model).State = EntityState.Modified;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("结清分期付款记录", "学生产品分期记录信息:" + model.ToJson(), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "已将该条分期记录更改为已结清状态");
        }

        /// <summary>
        /// 分期记录改为取消状态
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentProductId"></param>
        /// <param name="installmentId"></param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public Tuple<int, string> CancelInstallmentInfo(int studentId, int studentProductId, int installmentId, int operateId)
        {
            var studentProcut = db.StudentProduct.FirstOrDefault(d => d.StudentID == studentId && d.ID == studentProductId && d.IsInstallment);
            if (studentProcut == null)
                return new Tuple<int, string>(1, "学生产品不存在或不属于分期产品");
            InstallmentPayInfoModel model = db.InstallmentPayInfo.FirstOrDefault(d => d.StudentProductId == studentProductId && d.Id == installmentId);
            if (model == null || model.PayStatus != 0)
                return new Tuple<int, string>(1, "该分期记录信息不存在或不处于未支付状态");
            model.PayStatus = 2;
            studentProcut.UnliquidatedMoney -= model.PayMoney;
            db.Entry(model).State = EntityState.Modified;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("取消分期付款记录", "学生产品分期记录信息:" + model.ToJson(), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "已将该条分期记录更改为取消状态");
        }
    }
}
