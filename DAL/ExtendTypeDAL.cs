using Model;
using System;
using System.Linq;
using Common;

namespace DAL
{
    public class ExtendTypeDAL : Base.BaseDAL
    {
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取扩展类型集合
        /// </summary>
        /// <param name="typeId">所属类型Id</param>
        /// <returns></returns>
        public IQueryable<ExtendTypeModel> GetList(Keys.ExtendTypeId typeId)
        {
            var data = (from et in db.ExtendType
                        orderby et.Id
                        select et).AsQueryable();
            if (typeId > 0)
                data = data.Where(d => d.TypeId == typeId);
            return data;
        }
        /// <summary>
        /// 获取可用的扩展类型集合
        /// </summary>
        /// <param name="typeId">所属类型Id</param>
        /// <returns></returns>
        public IQueryable<ExtendTypeModel> GetEnableList(Keys.ExtendTypeId typeId)
        {
            var data = GetList(typeId).Where(d => d.IsEnable.Equals(true));
            return data;
        }
        /// <summary>
        /// 获取扩展类型信息
        /// </summary>
        /// <param name="Id">扩展类型ID</param>
        /// <returns></returns>
        public ExtendTypeModel GetModel(int Id)
        {
            var model = db.ExtendType.FirstOrDefault(d => d.Id.Equals(Id));
            return model;
        }

        /// <summary>
        /// 获取扩展类型信息
        /// </summary>
        /// <param name="id">扩展类型ID</param>
        /// <param name="typeId">所属类型</param>
        /// <returns></returns>
        public ExtendTypeModel GetModel(int id, Keys.ExtendTypeId typeId)
        {
            var model = db.ExtendType.FirstOrDefault(d => d.Id.Equals(id) && d.TypeId == typeId);
            return model;
        }
        /// <summary>
        /// 添加或修改扩展类型信息
        /// </summary>
        /// <param name="id">扩展类型ID</param>
        /// <param name="name">名称</param>
        /// <param name="typeId">所属类型</param>
        /// <param name="isEnable">是否可用</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns></returns>
        public Tuple<int, string> AeModel(int id, string name, Keys.ExtendTypeId typeId, bool isEnable, int operateId)
        {
            ExtendTypeModel model = new ExtendTypeModel();
            if (id > 0)
            {
                model = GetModel(id);
                if (model == null)
                    return new Tuple<int, string>(1, "要修改的扩展类型信息不存在");
                if (model.TypeId != typeId)
                    return new Tuple<int, string>(1, "修改扩展类型信息不允许更改所属类型");
            }
            if (name.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "扩展类型名称不能为空");
            model.Name = name;
            model.AddTime = DateTime.UtcNow;
            model.IsEnable = isEnable;
            model.OperateId = operateId;
            model.TypeId = typeId;
            db.Entry(model).State = id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return new Tuple<int, string>(0, "扩展信息保存成功");
        }
    }
}
