using System;
using System.Data.Entity;
using System.Linq;
using Model;
using Common;

namespace DAL
{
    // ReSharper disable once InconsistentNaming
    public class ProductDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取产品实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductMOD GetModel(int id)
        {
            return db.Product.FirstOrDefault(d => d.ID == id);
        }

        /// <summary>
        /// 获取产品集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<ProductMOD> GetList()
        {
            return db.Product.AsQueryable();
        }
        /// <summary>
        /// 添加或修改产品信息
        /// </summary>
        /// <param name="model">产品信息</param>
        /// <param name="operateId">操作者</param>
        /// <returns></returns>
        public Tuple<int, string> AeModel(ProductMOD model, int operateId)
        {
            ProductMOD product = new ProductMOD();
            if (model.ID > 0)
            {
                product = GetModel(model.ID);
                if (product == null)
                    return new Tuple<int, string>(1, "产品信息不存在，请获取最新数据");
            }
            product.ProductName = model.ProductName;
            product.Frequency = model.Frequency;
            product.LimitDate = model.LimitDate;
            product.CourseCount = model.CourseCount;
            product.LeaveCount = model.LeaveCount;
            product.PriceMoney = model.PriceMoney;
            product.IsEnable = model.IsEnable;
            if (product.ProductName.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "产品名称不能为空");
            if (product.Frequency < 0)
                return new Tuple<int, string>(1, "请填写大于等于0的每周频率值");
            if (product.LimitDate <= 0)
                return new Tuple<int, string>(1, "请填写大于0的学籍时长");
            if (product.CourseCount <= 0)
                return new Tuple<int, string>(1, "请填写大于0的总课时");
            if (product.LeaveCount < 0)
                return new Tuple<int, string>(1, "请填写大于等于0的可休学时长");
            if (product.PriceMoney < 0)
                return new Tuple<int, string>(1, "请填写大于等于0的产品价格");
            if (product.ID > 0)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                ManagerLogDAL.AddManagerLog("修改产品信息", product.ToJson(), ManagerLogType.Manager, operateId);
                return new Tuple<int, string>(0, "修改产品信息成功");
            }
            product.AddTime = DateTime.UtcNow;
            db.Product.Add(product);
            db.SaveChanges();
            ManagerLogDAL.AddManagerLog("添加产品信息", product.ToJson(), ManagerLogType.Manager, operateId);
            return new Tuple<int, string>(0, "添加产品信息成功");
        }
        /// <summary>
        /// 删除产品信息实体
        /// </summary>
        /// <param name="id">产品Id</param>
        /// <returns></returns>
        public Tuple<int, string> DeleteModel(int id)
        {
            var model = GetModel(id);
            if (model == null)
                return new Tuple<int, string>(1, "产品不存在或已删除");
            db.Product.Remove(model);
            db.SaveChanges();
            return new Tuple<int, string>(0, "产品信息已删除");
        }
    }
}
