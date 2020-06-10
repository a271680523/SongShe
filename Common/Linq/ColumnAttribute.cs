using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Linq
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class ColumnAttribute : Attribute
    {
        private string _ColumnName;
        private DataType _ColumnType = DataType.String;
        public string ColumnName { get { return _ColumnName; } }
        public DataType ColumnType
        {
            get { return _ColumnType; }
            set { _ColumnType = value; }
        }
        public ColumnAttribute(string columnName)
        {
            _ColumnName = columnName;
        }
    }
}
