using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Linq
{
    public class CustomTable<T> : IQueryable<T>
    {
        private Type _ElementType = null;
        private Expression _Expression = null;
        private IQueryProvider _Provider = null;

        public Type ElementType => _ElementType;

        public Expression Expression => _Expression;

        public IQueryProvider Provider => _Provider;

        public CustomTable(Expression expression, IQueryProvider provider)
        {
            _ElementType = typeof(T);
            _Expression = expression;
            _Provider = provider ?? throw new Exception("provider can't be null");
        }
        public CustomTable()
            : this(null, new CustomProvider())
        {
            _Expression = Expression.Constant(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<T>>(Expression)).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override string ToString()
        {
            return _Provider.ToString();
        }
    }
}
