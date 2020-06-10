using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
//using static DBUtility.CustomContext;

namespace DBUtility.ORM
{
    public static class Linq
    {
        /// <summary>
        /// 数据库访问类对象集合
        /// </summary>
        private static readonly Dictionary<string, DbHelperSQLP> DbHelperSqLs = new Dictionary<string, DbHelperSQLP>();
        /// <summary>
        /// 获取数据访问类对象
        /// </summary>
        /// <param name="connectionString">数据连接字符串</param>
        /// <returns></returns>
        private static DbHelperSQLP DbHelperSql(string connectionString)
        {
            if (!DbHelperSqLs.Keys.Any(d => d.Equals(connectionString)))
            {
                DbHelperSqLs.Add(connectionString, new DbHelperSQLP(connectionString));
            }
            return DbHelperSqLs[connectionString];
        }
        /// <summary>
        /// 更新数据库表结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        public static void UpdateDataBaseTable<T>(this DbSet<T> ts) where T : class, new()
        {
            StringBuilder strSql = new StringBuilder();
            var info = GetSqlTableModelInfoModel(typeof(T));
            var tbCoulumnInfo = DbHelperSql(ts.ConnectionString).GetAllColumnInfo(info.TableName);
            List<DbParameter> parameters = new List<DbParameter>();
            #region 增加不存在的字段
            //查询数据库表中不存在的数据字段
            foreach (var coulmnInfo in info.CoulmnInfoList)
            {
                foreach (DataRow dr in tbCoulumnInfo.Rows)
                {
                    if (dr.GetValue<string>("name") == coulmnInfo.DataCoulmnName)
                    {
                        strSql.Append($"alter table [{info.TableName}] alter column [{coulmnInfo.DataCoulmnName}] [{coulmnInfo.DbType}] {(coulmnInfo.IsNotNull ? "not null" : "null")};");
                        strSql.Append($"select c.name from sysconstraints a inner join syscolumns b on a.colid=b.colid inner join sysobjects c on a.constid=c.id where a.id=object_id('{info.TableName}') and b.name='{coulmnInfo.DataCoulmnName}';");
                        if (coulmnInfo.IsDefaultValue)
                        {

                        }
                        break;
                    }
                }
                strSql.Append($"alter table [{info.TableName}] add [{coulmnInfo.DataCoulmnName}] [{coulmnInfo.DbType}] {(coulmnInfo.IsKey ? "primary key" : "")} {(coulmnInfo.IsIdentity ? "IDENTITY(1,1)" : "")} {(coulmnInfo.IsNotNull ? "not null" : "null")} {(coulmnInfo.IsDefaultValue ? "default " + parameters.AddDbParameter(coulmnInfo.DefaultValue) : "")};");
            }
            #endregion
            #region MyRegion

            #endregion
        }

        /// <summary>
        /// 获取数据集合
        /// </summary>
        /// <typeparam name="T">操作的表</typeparam>
        /// <param name="ts"></param>
        /// <param name="expression">筛选条件，表达式</param>
        /// <returns>满足条件的数据集合</returns>
        public static List<T> GetList<T>(this DbSet<T> ts, Expression<Func<T, bool>> expression) where T : class, new()
        {
            var info = GetSqlTableModelInfoModel(typeof(T));
            List<DbParameter> parameters = new List<DbParameter>();
            var whereSql = ProcessExpressionByWhere(expression, ref parameters);
            string strSql = $"Select {info.GetSqlSelectCoulumn()} from {info.TableName} {(whereSql.Length > 0 ? " where " + whereSql : "")}";
            var dataSet = DbHelperSql(ts.ConnectionString).Query(strSql, parameters.ToArray<SqlParameter>());
            if (dataSet.Tables.Count > 0)
                return dataSet.Tables[0].DataTableToList<T>();
            return new List<T>();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">操作的表</typeparam>
        /// <param name="ts"></param>
        /// <param name="expression">筛选条件，表达式</param>
        /// <returns>满足条件的数据实体</returns>
        public static T GetModel<T>(this DbSet<T> ts, Expression<Func<T, bool>> expression) where T : class, new()
        {
            var info = GetSqlTableModelInfoModel(typeof(T));
            List<DbParameter> parameters = new List<DbParameter>();
            var whereSql = ProcessExpressionByWhere(expression, ref parameters);
            string strSql = $"Select {info.GetSqlSelectCoulumn()} from {info.TableName} {(whereSql.Length > 0 ? " where " + whereSql : "")}";
            var dataSet = DbHelperSql(ts.ConnectionString).Query(strSql, parameters.ToArray<SqlParameter>());
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                return dataSet.Tables[0].Rows[0].DataRowToModel<T>();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TCustom"></typeparam>
        /// <param name="model"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static TCustom Select<T, TCustom>(this T model, Expression<Func<T, TCustom>> expression) where T : class, new() where TCustom : class
        {
            var type = typeof(TCustom);
            if (type.IsBulitinType() || !type.IsClass)
                return (TCustom)typeof(T).GetProperty(type.Name)?.GetValue(model);
            if (model == null)
                return default(TCustom);
            var properties = type.GetProperties();
            object[] values = new object[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                var value = typeof(T).GetProperty(properties[i].Name)?.GetValue(model);
                values[i] = value;
            }
            var obj = Activator.CreateInstance(type, values);
            return (TCustom)obj;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T">操作的表</typeparam>
        /// <param name="ts"></param>
        /// <param name="model">要修改保存的数据</param>
        /// <returns></returns>
        public static T Add<T>(this DbSet<T> ts, T model) where T : class, new()
        {
            var info = GetSqlTableModelInfoModel(typeof(T));
            List<DbParameter> parameters = new List<DbParameter>();
            var strSqlvalues = new StringBuilder();
            var strIdentity = string.Empty;
            foreach (var coulmnInfo in info.CoulmnInfoList)
            {
                if (!coulmnInfo.IsIdentity)
                {
                    //创建Parameter参数
                    var param = NewParameterBySqlType(SqlType.MSSql);
                    param.ParameterName = GetNoExistParameterName(parameters);
                    param.Value = coulmnInfo.GetValue(model);
                    param.DbType = GetDbType(coulmnInfo.Type);
                    parameters.Add(param);
                    strSqlvalues.Append($"{param.ParameterName},");
                }
                else
                {
                    var param = NewParameterBySqlType(SqlType.MSSql);
                    param.ParameterName = GetNoExistParameterName(parameters);
                    param.DbType = GetDbType(typeof(int));
                    param.Direction = ParameterDirection.Output;
                    parameters.Add(param);
                    strIdentity = $"select {param.ParameterName}=@@IDENTITY;";
                }
            }
            string strSql = $"Insert into {info.TableName}({info.GetSqlSelectCoulumn()}) values ({strSqlvalues.ToString(0, strSqlvalues.Length - 1)});{strIdentity}";
            int rows = DbHelperSql(ts.ConnectionString).ExecuteSql(strSql, parameters.ToArray<SqlParameter>());
            if (rows > 0 && strIdentity.Length > 0)
            {
                info.CoulmnInfoList.Last(d => d.IsIdentity).PropertyInfo.SetValue(model, parameters.Last(d => d.Direction == ParameterDirection.Output).Value);
            }
            return model;
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <typeparam name="T">操作的表</typeparam>
        /// <param name="ts"></param>
        /// <param name="model">要修改保存的数据</param>
        /// <param name="expression">筛选条件，表达式</param>
        /// <returns></returns>
        public static T Edit<T>(this DbSet<T> ts, T model, Expression<Func<T, bool>> expression) where T : class, new()
        {
            var info = GetSqlTableModelInfoModel(typeof(T));
            var strSqlSetInfo = new StringBuilder();
            List<DbParameter> parameters = new List<DbParameter>();
            var whereSql = ProcessExpressionByWhere(expression, ref parameters);
            info.CoulmnInfoList.ForEach(d =>
            {
                if (d.IsIdentity)
                    return;
                //创建Parameter参数
                var param = NewParameterBySqlType(SqlType.MSSql);
                param.ParameterName = GetNoExistParameterName(parameters);
                param.Value = d.GetValue(model);
                param.DbType = GetDbType(d.Type);
                parameters.Add(param);
                strSqlSetInfo.Append($"{d.DataCoulmnName}={param.ParameterName},");
            });
            string strSql = $"Update {info.TableName} set {strSqlSetInfo.ToString(0, strSqlSetInfo.Length - 1)} {(whereSql.Length > 0 ? " where " + whereSql : "")}";
            int rows = DbHelperSql(ts.ConnectionString).ExecuteSql(strSql, parameters.ToArray<SqlParameter>());
            if (rows > 0)
                return model;
            throw new Exception("保存数据失败");
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">操作的表</typeparam>
        /// <param name="ts"></param>
        /// <param name="expression">筛选条件，表达式</param>
        /// <returns></returns>
        public static int Delete<T>(this DbSet<T> ts, Expression<Func<T, bool>> expression) where T : class, new()
        {
            var info = GetSqlTableModelInfoModel(typeof(T));
            List<DbParameter> parameters = new List<DbParameter>();
            var whereSql = ProcessExpressionByWhere(expression, ref parameters);
            var strSql = $"Delete {info.TableName} {(whereSql.Length > 0 ? " where " + whereSql : "")}";
            int rows = DbHelperSql(ts.ConnectionString).ExecuteSql(strSql, parameters.ToArray<SqlParameter>());
            return rows;
        }

        /// <summary>
        /// 处理表达式
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string ProcessExpressionByWhere(Expression expression, ref List<DbParameter> parameters)
        {
            switch (expression)
            {
                case UnaryExpression unary://运算符
                    return ProcessExpressionByWhere(unary.Operand, ref parameters);
                case LambdaExpression lambda://Lambda表达式的情况
                    return ProcessExpressionByWhere(lambda.Body, ref parameters);
                case BinaryExpression binary://二级制运算符表达式
                    return $"({ProcessExpressionByWhere(binary.Left, ref parameters)} {GetOperStr(binary.NodeType)} {ProcessExpressionByWhere(binary.Right, ref parameters)})";
                case MemberExpression member://字段或属性
                    if (member.Expression is ParameterExpression)
                        return member.Member.Name;
                    return ProcessExpressionByWhere(Expression.Constant(GetExpressionValue(member)), ref parameters);
                case ConstantExpression constant://常数属性值
                    //创建Parameter参数
                    var param = NewParameterBySqlType(SqlType.MSSql);
                    param.ParameterName = GetNoExistParameterName(parameters);
                    param.Value = constant.Value;
                    param.DbType = GetDbType(constant.Type);
                    parameters.Add(param);
                    return param.ParameterName;
                case MethodCallExpression methodCall:
                    if (methodCall.Method.ReflectedType == typeof(string))
                    {
                        switch (methodCall.Method.Name)
                        {
                            case "Contains":
                                return $"({ProcessExpressionByWhere(methodCall.Object, ref parameters)} like '%'+{ProcessExpressionByWhere(methodCall.Arguments.FirstOrDefault(), ref parameters)}+'%')";
                            case "Equals":
                                return $"({ProcessExpressionByWhere(methodCall.Object, ref parameters)} = {ProcessExpressionByWhere(methodCall.Arguments.FirstOrDefault(), ref parameters)})";
                        }
                    }
                    throw new Exception($"暂未实现的MethodCallExpression处理方法：{methodCall.Method}");
                default:
                    throw new Exception("暂未实现的处理表达式：" + expression.GetType());
            }
        }
        /// <summary>
        /// 获取表达式中对象值
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        private static object GetExpressionValue(Expression expression)
        {
            switch (expression)
            {
                case MethodCallExpression methodCall:
                    object[] paramValues = new object[methodCall.Arguments.Count];
                    for (int i = 0; i < methodCall.Arguments.Count; i++)
                    {
                        paramValues[i] = GetExpressionValue(methodCall.Arguments[i]);
                    }
                    return methodCall.Method.Invoke(GetExpressionValue(methodCall.Object), paramValues);
                case ConstantExpression constant:
                    return constant.Value;
                case MemberExpression member:
                    return member.Member.GetValue(GetExpressionValue(member.Expression));
                default:
                    return null;
            }
        }
        /// <summary>
        /// 获取在对象中的字段或属性的值
        /// </summary>
        /// <param name="memberInfo">待取值的字段或属性</param>
        /// <param name="obj">取值的对象</param>
        /// <returns></returns>
        private static object GetValue(this MemberInfo memberInfo, object obj)
        {
            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    return fieldInfo.GetValue(obj);
                case PropertyInfo propertyInfo:
                    return propertyInfo.GetValue(obj);
                default:
                    throw new Exception($"未实现{memberInfo.GetType().FullName}类型转换的{nameof(GetValue)}方法");
            }
        }
        /// <summary>
        /// 获取集合中不存在的DbParameter名称
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string GetNoExistParameterName(List<DbParameter> parameters)
        {
            int i = parameters.Count;
            string paramName = $"@Param{i}";
            while (i >= 0)
            {
                if (!parameters.Any(d => d.ParameterName.Equals(paramName)))
                    break;
                i++;
            }
            return paramName;
        }
        /// <summary>
        /// 添加DbParameter，返回该DbParameter的ParameterName
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string AddDbParameter(this List<DbParameter> parameters, object value, Type type = null)
        {
            if (parameters == null)
                parameters = new List<DbParameter>();
            //创建Parameter参数
            var param = NewParameterBySqlType(SqlType.MSSql);
            param.ParameterName = GetNoExistParameterName(parameters);
            param.Value = value;
            param.DbType = GetDbType(type ?? value.GetType());
            parameters.Add(param);
            return param.ParameterName;
        }


        /// <summary>
        /// 获取连接符
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetOperStr(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.OrElse: return " OR ";
                case ExpressionType.Or: return "|";
                case ExpressionType.AndAlso: return " AND ";
                case ExpressionType.And: return "&";
                case ExpressionType.GreaterThan: return ">";
                case ExpressionType.GreaterThanOrEqual: return ">=";
                case ExpressionType.LessThan: return "<";
                case ExpressionType.LessThanOrEqual: return "<=";
                case ExpressionType.NotEqual: return "<>";
                case ExpressionType.Add: return "+";
                case ExpressionType.Subtract: return "-";
                case ExpressionType.Multiply: return "*";
                case ExpressionType.Divide: return "/";
                case ExpressionType.Modulo: return "%";
                case ExpressionType.Equal: return "=";
            }
            return "";
        }
        /// <summary>
        /// 是否是内置类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsBulitinType(this Type type)
        {
            return type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object;
        }
        /// <summary>
        /// 将DbParameter集合转换成DbParameter或其子类的数组对象
        /// </summary>
        /// <typeparam name="TParameter">DbParameter或其子类对象</typeparam>
        /// <param name="parameters">参数集合</param>
        /// <returns></returns>
        public static TParameter[] ToArray<TParameter>(this List<DbParameter> parameters) where TParameter : DbParameter
        {
            var sqlParames = new TParameter[parameters.Count];
            for (int i = 0; i < parameters.Count; i++)
            {
                sqlParames[i] = parameters[i] as TParameter;
            }
            return sqlParames;
        }
        /// <summary>
        /// 根据实体类型获取SqlTableModelInfoModel对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SqlTableModelInfoModel GetSqlTableModelInfoModel(Type type)
        {
            if (!type.IsClass)
                throw new Exception("仅适用于可实例化的类");
            var sqlTableModelInfo = Common.MemoryCacheHelper.GetCache(nameof(SqlTableModelInfoModel) + "_" + type.Name);
            if (sqlTableModelInfo == null)
            {
                sqlTableModelInfo = new SqlTableModelInfoModel(type);
                Common.MemoryCacheHelper.SetCache(nameof(SqlTableModelInfoModel) + "_" + type.Name, sqlTableModelInfo);
            }
            return sqlTableModelInfo as SqlTableModelInfoModel;
        }
        /// <summary>
        /// 实体转换成SQL语句临时信息
        /// </summary>
        public class SqlTableModelInfoModel
        {
            /// <summary>
            /// 根据传入的类型生成SQL相关信息实体
            /// </summary>
            /// <param name="type"></param>
            public SqlTableModelInfoModel(Type type)
            {
                if (!type.IsClass)
                    throw new Exception("仅适用于可实例化的类");
                ModelType = type;
                ModelName = ModelType.Name;
                TableName = ModelType.GetCustomAttribute<TableAttribute>(true)?.Name ?? ModelName;
                CoulmnInfoList = new List<CoulmnInfoModel>();
                foreach (var property in ModelType.GetProperties())
                {
                    CoulmnInfoList.Add(new CoulmnInfoModel(TableName, property));
                }
            }
            /// <summary>
            /// 获取该实体需要查询出来的列
            /// </summary>
            /// <returns></returns>
            internal string GetSqlSelectCoulumn()
            {
                return string.Join(",", CoulmnInfoList.Select((d, s) => $"[{d.DataCoulmnName}]"));
            }
            /// <summary>
            /// 获取DbParameters数据库映射参数
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="sqlType"></param>
            /// <returns></returns>
            public DbParameter[] GetDbParameters(object obj, SqlType sqlType)
            {
                var parames = new List<DbParameter>();
                foreach (var property in CoulmnInfoList)
                {
                    if (!property.IsIdentity)
                    {
                        if (parames.Any(d => d.ParameterName.Equals($"@{property.DataCoulmnName},", StringComparison.OrdinalIgnoreCase)))
                        {
                            var param = NewParameterBySqlType(sqlType);
                            param.ParameterName = $"@{property.DataCoulmnName}";
                            param.Value = property.GetValue(obj);
                            param.DbType = property.DbType;
                            parames.Add(param);
                        }
                    }
                }
                return parames.ToArray();
            }
            /// <summary>
            /// 实体类类型
            /// </summary>
            public Type ModelType { get; set; }
            /// <summary>
            /// 实体类名称
            /// </summary>
            public string ModelName { get; set; }
            /// <summary>
            /// 数据库表名
            /// </summary>
            public string TableName { get; set; }
            /// <summary>
            /// 该实体中所有列信息集合
            /// </summary>
            public List<CoulmnInfoModel> CoulmnInfoList { get; set; }
            /// <summary>
            /// 列信息实体
            /// </summary>
            public class CoulmnInfoModel
            {
                private readonly string _tableName;
                public CoulmnInfoModel() { }
                /// <summary>
                /// 列信息实体
                /// </summary>
                /// <param name="tableName">数据库表名</param>
                /// <param name="property">列属性</param>
                internal CoulmnInfoModel(string tableName, PropertyInfo property)
                {
                    _tableName = tableName;
                    PropertyInfo = property;
                }
                /// <summary>
                /// 获取该列的值
                /// </summary>
                /// <param name="obj"></param>
                /// <returns></returns>
                public object GetValue(object obj)
                {
                    return _propertyInfo.GetValue(obj);
                }
                /// <summary>
                /// 列信息对象
                /// </summary>
                private PropertyInfo _propertyInfo;
                /// <summary>
                /// 列名称
                /// </summary>
                public string CoulmnName { get; set; }
                /// <summary>
                /// 数据库列名称
                /// </summary>
                public string DataCoulmnName { get; set; }
                /// <summary>
                /// 是否主键
                /// </summary>
                public bool IsKey { get; set; }
                /// <summary>
                /// 是否自增长
                /// </summary>
                public bool IsIdentity { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public bool IsNotNull { get; set; }
                /// <summary>
                /// 是否有默认值
                /// </summary>
                public bool IsDefaultValue { get; set; }
                /// <summary>
                /// 默认值
                /// </summary>
                public object DefaultValue { get; set; }

                /// <summary>
                /// 实体类该列的PropertyInfo属性
                /// </summary>

                public PropertyInfo PropertyInfo
                {
                    get => _propertyInfo;
                    set
                    {
                        _propertyInfo = value;
                        CoulmnName = value.Name;
                        DataCoulmnName = value.GetCustomAttribute<ColumnAttribute>()?.Name ?? CoulmnName;
                        IsKey = string.Equals(value.Name, "Id", StringComparison.OrdinalIgnoreCase) || string.Equals(value.Name, $"{_tableName}Id", StringComparison.OrdinalIgnoreCase) || value.GetCustomAttributes(typeof(KeyAttribute), true).Any();
                        IsIdentity = IsKey && value.GetCustomAttribute<DatabaseGeneratedAttribute>()?.DatabaseGeneratedOption != DatabaseGeneratedOption.None;//主键并且未设置成不自动生成值模式
                        IsNotNull = IsKey || value.GetCustomAttribute<RequiredAttribute>()?.AllowEmptyStrings == false;//主键不能为空，设置为不能为空字段
                        var defaultValue = value.GetCustomAttribute<DefaultValueAttribute>();
                        IsDefaultValue = defaultValue != null;//是否添加默认值属性
                        DefaultValue = defaultValue?.Value;//默认值设置为多少
                        Type = value.PropertyType;
                        DbType = GetDbType(Type);
                    }
                }
                /// <summary>
                /// 该字段类型
                /// </summary>
                public Type Type { get; set; }
                /// <summary>
                /// 该字段数据库类型
                /// </summary>
                public DbType DbType { get; set; }
            }
        }
        /// <summary>
        /// 根据字段Type类型获取DbType数据库类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbType GetDbType(Type type)
        {
            SqlTypeInfo sqlTypeInfo = sqlTypeInfos.FirstOrDefault(d => type.IsEnum ? d.Type == typeof(Enum) : d.Type == type);
            if (sqlTypeInfo != null)
                return sqlTypeInfo.DbType;
            throw new Exception("未实现的Type与DbType的类型转换：" + type.Name);
            //DbType dbType;
            //if (type == typeof(string))
            //    dbType = DbType.String;
            //else if (type == typeof(int))
            //    dbType = DbType.Int32;
            //else if (type == typeof(byte[]))
            //    dbType = DbType.Binary;
            //else if (type == typeof(char))
            //    dbType = DbType.Boolean;
            //else if (type == typeof(DateTime))
            //    dbType = DbType.DateTime;
            //else if (type == typeof(decimal))
            //    dbType = DbType.Decimal;
            //else if (type == typeof(double))
            //    dbType = DbType.Double;
            //else if (type == typeof(Single))
            //    dbType = DbType.Single;
            //else if (type == typeof(Int16))
            //    dbType = DbType.Int16;
            //else if (type == typeof(byte))
            //    dbType = DbType.Byte;
            //else if (type.IsEnum)
            //    dbType = DbType.Int32;
            //else
            //    throw new Exception("未实现的Type与DbType的类型转换：" + type.Name);
            //return dbType;
        }
        /// <summary>
        /// 数据库中与C#中的数据类型对照
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static Type SqlTypeToType(string dbType)
        {
            switch (dbType.ToLower())
            {
                case "int":
                    return typeof(int);
                case "text":
                    return typeof(string);
                case "bigint":
                    return typeof(Int64);
                case "binary":
                    return typeof(byte[]);
                case "bit":
                    return typeof(bool);
                case "char":
                    return typeof(char);
                case "datetime":
                    return typeof(DateTime);
                case "decimal":
                    return typeof(decimal);
                case "float":
                    return typeof(double);
                case "image":
                    return typeof(byte[]);
                case "money":
                    return typeof(decimal);
                case "nchar":
                    return typeof(string);
                case "ntext":
                    return typeof(string);
                case "numeric":
                    return typeof(decimal);
                case "nvarchar":
                    return typeof(string);
                case "real":
                    return typeof(Single);
                case "smalldatetime":
                    return typeof(DateTime);
                case "smallint":
                    return typeof(Int16);
                case "smallmoney":
                    return typeof(decimal);
                case "timestamp":
                    return typeof(DateTime);
                case "tinyint":
                    return typeof(byte);
                case "uniqueidentifier":
                    return typeof(Guid);
                case "varbinary":
                    return typeof(byte[]);
                case "varchar":
                    return typeof(string);
                case "Variant":
                    return typeof(object);
                default:
                    throw new Exception("未识别的数据库字段类型：" + dbType);
            }
        }

        private static readonly List<SqlTypeInfo> sqlTypeInfos = new List<SqlTypeInfo>
        {
            new SqlTypeInfo(typeof(string),DbType.String,231,231,"nvarchar"),
            new SqlTypeInfo(typeof(int),DbType.Int32,56,56,"int"),
            new SqlTypeInfo(typeof(byte[]),DbType.Binary,173,173,"binary"),
            new SqlTypeInfo(typeof(bool),DbType.Boolean,104,104,"bit"),
            new SqlTypeInfo(typeof(DateTime),DbType.DateTime,61,61,"datetime"),
            new SqlTypeInfo(typeof(decimal),DbType.Decimal,106,106,"decimal"),
            new SqlTypeInfo(typeof(double),DbType.Double,62,62,"float"),
            new SqlTypeInfo(typeof(Single),DbType.Single,59,59,"real"),
            new SqlTypeInfo(typeof(Int16),DbType.Int16,52,52,"smallint"),
            new SqlTypeInfo(typeof(byte),DbType.Byte,48,48,"tinyint"),
            new SqlTypeInfo(typeof(Enum),DbType.Int32,56,56,"int"),
        };
        /// <summary>
        /// C#中数据类型与数据库字段类型关联信息表
        /// </summary>
        private class SqlTypeInfo
        {
            public SqlTypeInfo(Type type, DbType dbType, int db_xtype, int db_xusertype, string db_xtypename)
            {
                Type = type;
                DbType = dbType;
                Db_xtype = db_xtype;
                Db_xusertype = db_xusertype;
                Db_xtypename = db_xtypename;
            }
            /// <summary>
            /// C#数据类型
            /// </summary>
            public Type Type { get; }
            /// <summary>
            /// C#中数据库枚举值类型
            /// </summary>
            public DbType DbType { get; }
            /// <summary>
            /// 数据库中物理存储数据类型，为系统表systypes中xtype列
            /// </summary>
            public int Db_xtype { get; }
            /// <summary>
            /// 数据库中用户定义数据类型，为系统表systypes中xusertype列
            /// </summary>
            public int Db_xusertype { get; }
            /// <summary>
            /// 数据库中数据类型名称，为系统表systypes中name列
            /// </summary>
            public string Db_xtypename { get; }
        }

        /// <summary>
        /// 根据数据库类型初始化Parameter对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbParameter NewParameterBySqlType(SqlType type)
        {
            switch (type)
            {
                case SqlType.MSSql:
                    return new SqlParameter();
                case SqlType.MySql:
                    return new MySqlParameter();
                case SqlType.Oracle:
                    return new OracleParameter();
                case SqlType.SQLite:
                    return new SQLiteParameter();
                default:
                    throw new Exception("不支持的SQL类型");
            }
        }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum SqlType
        {
            /// <summary>
            /// MSSql
            /// </summary>
            // ReSharper disable once InconsistentNaming
            MSSql,
            /// <summary>
            /// MySql
            /// </summary>
            MySql,
            /// <summary>
            /// SQLite
            /// </summary>
            // ReSharper disable once InconsistentNaming
            SQLite,
            /// <summary>
            /// Oracle
            /// </summary>
            Oracle
        }
        /// <summary>
        /// DataTable转换成Model集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dt">数据表</param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(this DataTable dt) where T : class, new()
        {
            List<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(DataRowToModel<T>(row));
            }
            return list;
        }
        /// <summary>
        /// 行数据DataRow转换成Model实体类型数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="dataRow">行数据</param>
        /// <returns></returns>
        public static T DataRowToModel<T>(this DataRow dataRow) where T : class, new()
        {
            T t = new T();
            foreach (var property in typeof(T).GetProperties())
            {
                var obj = GetValue<object>(dataRow, property.Name);
                if (obj != null)
                {
                    //var type = property.GetType();
                    property.SetValue(t, obj);
                }
            }
            return t;
        }
        /// <summary>
        /// 获取数据行DataRow中某列的值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="dataRow">行数据</param>
        /// <param name="coulmnName">列名</param>
        /// <returns></returns>
        public static T GetValue<T>(this DataRow dataRow, string coulmnName)
        {
            return dataRow.Field<T>(coulmnName);
        }
    }
}
