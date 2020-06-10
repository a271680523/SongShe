using System;
using System.Data.Entity;

namespace DBUtility.ORM
{
    /// <summary>
    /// 核心上下文对象
    /// </summary>
    public class CoreContext
    {
        /// <summary>
        /// 数据库名称或连接字符串。
        /// </summary>
        protected internal string nameOrConnectionString { get; }
        private CoreContext()
        {
        }
        /// <summary>
        /// 可以将给定字符串用作将连接到的数据库的名称或连接字符串来构造一个新的上下文实例。
        /// </summary>
        /// <param name="nameOrConnectionString">数据库名称或连接字符串。</param>
        public CoreContext(string nameOrConnectionString)
        {
            this.nameOrConnectionString = nameOrConnectionString;
            foreach (var property in GetType().GetProperties())
            {
                property.SetValue(this, Activator.CreateInstance(property.PropertyType, this));
            }
            UpdateDataBase();
        }
        /// <summary>
        /// 在完成对派生上下文的模型的初始化后，并在该模型已锁定并用于初始化上下文之前，将调用此方法。虽然此方法的默认实现不执行任何操作，但可在派生类中重写此方法，这样便能在锁定模型之前对其进行进一步的配置。
        /// </summary>
        /// <param name="modelBuilder">定义要创建的上下文的模型的生成器。</param>
        protected virtual void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        // ReSharper disable once InconsistentNaming
        protected internal void UpdateDataBase()
        {
            foreach (var property in GetType().GetProperties())
            {
                if (property.PropertyType.IsGenericType)
                {

                }
            }
        }
    }
    /// <summary>
    /// DbSet 表示上下文中给定类型的所有实体的集合或可从数据库中查询的给定类型的所有实体的集合。
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DbSet<TEntity> : System.Data.Entity.DbSet<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 核心上下文对象
        /// </summary>
        private CoreContext coreContext { get; }
        private DbSet() { }
        public DbSet(CoreContext coreContext)
        {
            this.coreContext = coreContext;
        }
        public override TEntity Add(TEntity entity) { return Linq.Add(this, entity); }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString => coreContext.nameOrConnectionString;
    }
}
