using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Linq
{
    public static class MapHelper
    {
        public static string GetTableName(Type type)
        {
            if (!type.IsDefined(typeof(TableAttribute), false)) throw new Exception("");
            TableAttribute ta = Attribute.GetCustomAttribute(type, typeof(TableAttribute)) as TableAttribute;
            return ta.TableName;

        }
        public static string GetColumnName(Type type, string propertyName)
        {
            PropertyInfo pi = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi == null) throw new Exception("");
            if (!pi.IsDefined(typeof(ColumnAttribute), false)) return propertyName;

            ColumnAttribute ca = Attribute.GetCustomAttribute(pi, typeof(ColumnAttribute)) as ColumnAttribute;
            return ca.ColumnName;
        }
        public static Type GetColumnType(Type type, string propertyName)
        {
            PropertyInfo pi = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi == null) throw new Exception("");
            if (!pi.IsDefined(typeof(ColumnAttribute), false)) return pi.PropertyType;

            ColumnAttribute ca = Attribute.GetCustomAttribute(pi, typeof(ColumnAttribute)) as ColumnAttribute;
            return SwithType(ca.ColumnType);
        }
        static Type SwithType(DataType dtype)
        {
            Type type = null;
            switch (dtype)
            {
                case DataType.String:
                    type = typeof(String);
                    break;
                case DataType.Int:
                    type = typeof(Int32);
                    break;
                case DataType.DateTime:
                    type = typeof(DateTime);
                    break;
                case DataType.Float:
                    type = typeof(float);
                    break;
                case DataType.Double:
                    type = typeof(double);
                    break;
            }
            return type;
        }
    }
}
