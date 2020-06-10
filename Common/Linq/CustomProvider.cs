using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Linq
{
    public class CustomProvider : IQueryProvider
    {
        private string sql = "";
        private int count = 0;

        private string tableName = "";
        private string selector = "";
        private string where = "";

        private Type _PreType = null;
        private Type _ElementType = null;
        public IQueryable<T> CreateQuery<T>(Expression expression)
        {
            _ElementType = typeof(T);
            SetQueryText(expression);
            count++;
            return new CustomTable<T>(expression, this);
        }
        public IQueryable CreateQuery(Expression expression)
        {
            _ElementType = expression.Type.GetGenericArguments()[0];
            SetQueryText(expression);
            count++;
            object[] args = new object[] { expression, this };
            return (IQueryable)Activator.CreateInstance(typeof(CustomTable<>).MakeGenericType(_ElementType), args);
        }

        public T Execute<T>(Expression expression)
        {
            return (T)ExecuteSql(expression);
        }
        public object Execute(Expression expression)
        {
            return ExecuteSql(expression);
        }
        private void SetQueryText(Expression expression)
        {
            MethodCallExpression call = (MethodCallExpression)expression;
            Expression first = call.Arguments[0];
            Expression second = call.Arguments[1];
            SetTableName(first);
            if (call.Method.Name == "Select")
            {
                where = " ";
            }
            else if (call.Method.Name == "Where")
            {
                selector = "select " + "t" + count + ".*  ";
            }
            ProcessExpression(second);

            sql = selector + " from " + tableName + " " + where;
        }
        private void SetTableName(Expression expression)
        {
            if (expression is ConstantExpression)
            {
                _PreType = expression.Type.GetGenericArguments()[0];
                tableName = MapHelper.GetTableName(_PreType) + " as t" + count + " ";
            }
            if (expression is MethodCallExpression)
            {
                _PreType = expression.Type.GetGenericArguments()[0];
                tableName = "( " + sql + " ) as t" + count + " ";
            }
        }
        void ProcessExpression(Expression expression)
        {
            if (expression is UnaryExpression tmp)
            {
                ProcessExpression(tmp.Operand);
            }
            if (expression is LambdaExpression lambdaExpression)
            {
                ProcessExpression(lambdaExpression.Body);
            }
            if (expression is BinaryExpression binaryExpression)
            {
                ProcessBinary(binaryExpression);
            }
            if (expression is NewExpression newExpression)
            {
                ProcessNew(newExpression);
            }
        }
        void ProcessBinary(BinaryExpression expression)
        {
            string membername = "";
            string propertyname = "";
            object value = "";
            string ope = "";
            if (expression.Left is BinaryExpression || expression.Right is BinaryExpression)
            {
                throw new Exception("only be one binary");
            }
            if (expression.Left is MemberExpression tmp1)
            {
                propertyname = tmp1.Member.Name;
                membername = MapHelper.GetColumnName(_PreType, propertyname);
            }
            if (expression.Right is ConstantExpression tmp)
            {
                value = tmp.Value;
            }
            if (expression.NodeType == ExpressionType.Equal)
            {
                ope = " = ";
            }
            if (expression.NodeType == ExpressionType.LessThan)
            {
                ope = " < ";
            }
            if (expression.NodeType == ExpressionType.GreaterThan)
            {
                ope = " > ";
            }
            Type type = MapHelper.GetColumnType(_PreType, propertyname);
            switch (type.Name)
            {
                case "Int32":
                case "Single":
                case "Double":
                    where += " where t" + count + "." + membername + ope + value;
                    break;
                case "String":
                case "DateTime":
                    where += " where t" + count + "." + membername + ope + "'" + value + "'";
                    break;

            }
        }
        void ProcessNew(NewExpression expression)
        {
            selector = "select ";
            List<string> newName = new List<String>();
            List<string> oldName = new List<string>();
            foreach (MemberInfo mi in expression.Members)
            {
                newName.Add(mi.Name);
            }
            foreach (MemberExpression arg in expression.Arguments)
            {
                oldName.Add(arg.Member.Name);
            }
            for (int i = 0; i < oldName.Count; i++)
            {
                if (newName[i] == oldName[i])
                {
                    selector += "t" + count + "." + MapHelper.GetColumnName(_PreType, oldName[i]) + ",";
                }
                else
                {
                    selector += "t" + count + "." + MapHelper.GetColumnName(_PreType, oldName[i]) + " as " + newName[i] + " ,";
                }
            }
            selector = selector.Substring(0, selector.Length - 1);
        }
        private object ExecuteSql(Expression expression)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=DuoDuo;Integrated Security=True")) //这里写死了数据库连接
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(sql, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            return Table2Entity.ConvertFromTable(ds.Tables[0], _ElementType); ;
        }
        public override string ToString()
        {
            return sql;
        }
    }
}
