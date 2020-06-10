using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Linq
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class TableAttribute : Attribute
    {
        private string _TableName;
        public string TableName => _TableName;

        public TableAttribute(string tableName)
        {
            _TableName = tableName;
        }
    }

}
