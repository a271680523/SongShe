using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Linq
{
    internal static class Table2Entity
    {
        static object ConvertFromDataRow(DataRow dr, Type type)
        {
            object o = null;
            if (!type.IsDefined(typeof(TableAttribute), false))
            {
                List<object> paralist = new List<object>();
                PropertyInfo[] pi = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                foreach (PropertyInfo p in pi)
                {
                    if (!dr.Table.Columns.Contains(p.Name))
                        throw new Exception("");
                    object value = Convert.ChangeType(dr[p.Name], p.PropertyType);
                    paralist.Add(value);
                }
                o = Activator.CreateInstance(type, paralist.ToArray());
            }
            else
            {
                o = Activator.CreateInstance(type);
                PropertyInfo[] pi = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                foreach (PropertyInfo p in pi)
                {
                    if (!dr.Table.Columns.Contains(MapHelper.GetColumnName(type, p.Name)))
                        throw new Exception("");
                    object value = Convert.ChangeType(dr[MapHelper.GetColumnName(type, p.Name)], p.PropertyType);
                    p.SetValue(o, value, null);
                }
            }
            return o;
        }
        public static object ConvertFromTable(DataTable dt, Type type)
        {
            var t = typeof(List<>).MakeGenericType(type);
            object obj = Activator.CreateInstance(t);
            MethodInfo add = t.GetMethod("Add");
            foreach (DataRow dr in dt.Rows)
            {
                add.Invoke(obj, new object[] { ConvertFromDataRow(dr, type) });
            }
            return obj;
        }
    }
}
