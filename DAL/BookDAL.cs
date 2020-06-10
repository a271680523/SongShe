///////////////////////////////////////////////////////////////////
//CreateTime	2018-1-10 16:10:15
//CreateBy 		唐翔
//Content       教材数据处理类
//////////////////////////////////////////////////////////////////
using Model;
using System;
using System.Linq;
using Common;
using System.Data.Entity;

namespace DAL
{
    /// <summary>
    /// 教材数据处理
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class BookDAL : Base.BaseDAL
    {
        private DbSet<ManagerModel> dbset{ get; set; }
        //private readonly OnlineEduContext _db = OnlineEduContext.Db;
        /// <summary>
        /// 获取教材集合
        /// </summary>
        /// <returns></returns>
        public IQueryable<v_BookMOD> GetBookList()
        {
            var data = from b in db.Book
                       join m in db.Manager on b.ManagerID equals m.ID into t1
                       from mt in t1.DefaultIfEmpty()
                       orderby b.ID descending
                       select new v_BookMOD { ID = b.ID, AddTime = b.AddTime, ManagerID = b.ManagerID, ManagerName = mt.ManagerName, Name = b.Name };
            return data;
        }
        /// <summary>
        /// 获取教材实体信息
        /// </summary>
        /// <param name="id">教材ID</param>
        /// <returns></returns>
        public BookMOD GetModel(int id)
        {
            return db.Book.FirstOrDefault(d => d.ID == id);
        }
        /// <summary>
        /// 获取教材视图实体信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public v_BookMOD GetModel_v(int id)
        {
            return GetBookList().FirstOrDefault(d => d.ID.Equals(id));
        }
        /// <summary>
        /// 添加或修改教材信息
        /// </summary>
        /// <param name="model">教材实体</param>
        /// <param name="operateId">操作员ID</param>
        /// <returns></returns>
        public Tuple<int, string> AeBook(BookMOD model, int operateId)
        {
            BookMOD book = new BookMOD();
            if (model.ID > 0)
            {
                book = GetModel(model.ID);
                if (book == null)
                    return new Tuple<int, string>(1, "教材信息不存在");
            }
            book.Name = model.Name;
            if (book.Name.IsNullOrWhiteSpace())
                return new Tuple<int, string>(1, "教材名称不能为空");
            if (book.ID > 0)
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
            else
            {
                book.AddTime = DateTime.UtcNow;
                book.ManagerID = operateId;
                db.Book.Add(book);
            }
            db.SaveChanges();
            return new Tuple<int, string>(0, (book.ID > 0 ? "修改" : "添加") + "教材信息成功");
        }
    }
}
