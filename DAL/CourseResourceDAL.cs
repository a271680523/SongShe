///////////////////////////////////////////////////////////////////
//CreateTime	2018年7月16日17:57:19
//CreateBy 		唐
//Content       课程资源数据处理类
//////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Base;
using Model;

namespace DAL
{
    /// <summary>
    /// 课程资源数据处理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class CourseResourceDAL : BaseDAL
    {
        /// <summary>
        /// 获取课程资源实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CourseResourceModel GetModel(int id)
        {
            var model = db.CourseResource.FirstOrDefault(d => d.Id.Equals(id));
            return model;
        }
        /// <summary>
        /// 获取课程资源集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<CourseResourceModel> GetList()
        {
            var data = from d in db.CourseResource
                       orderby d.Sort descending, d.AddTime
                       select d;
            return data;
        }

        /// <summary>
        /// 添加或修改课程资源信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="fileSize"></param>
        /// <param name="url"></param>
        /// <param name="extendTypeId"></param>
        /// <param name="effectiveTime"></param>
        /// <param name="sort"></param>
        /// <param name="operateId"></param>
        /// <returns></returns>
        public Tuple<int, string> AeModel(int id, string name, string description, string fileSize, string url,
            int? extendTypeId, DateTime? effectiveTime, int sort, int operateId)
        {
            CourseResourceModel model = new CourseResourceModel();
            if (id > 0)
            {
                model = GetModel(id);
                if (model == null)
                    return new Tuple<int, string>(1, "数据不存在，请获取最新数据");
            }
            ManagerModel operateManager = new ManagerDAL().GetMod(operateId);
            if (operateManager == null)
                return new Tuple<int, string>(1, "操作员信息异常，请获取最新数据");
            if (extendTypeId != null)
            {
                ExtendTypeModel extendType = new ExtendTypeDAL().GetModel((int)extendTypeId);
                if (extendType == null || extendType.TypeId != Common.Keys.ExtendTypeId.课程资源类型)
                    return new Tuple<int, string>(1, "资源类型不存在，请获取最新数据");
            }
            model.Name = name;
            model.Description = description;
            model.FileSize = fileSize;
            model.Url = Common.StaticCommon.GetResponseUrl(url);
            model.ExtendTypeId = extendTypeId;
            model.EffectiveTime = effectiveTime;
            model.Sort = sort;
            model.OperateId = operateId;
            model.AddTime = DateTime.UtcNow;
            db.Entry(model).State = id > 0 ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return new Tuple<int, string>(0, "课程资源保存成功");
        }
        /// <summary>
        /// 删除课程资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Tuple<int, string> Delete(int id)
        {
            CourseResourceModel model = GetModel(id);
            if (model != null)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return new Tuple<int, string>(0, "操作成功");
        }

    }
}
