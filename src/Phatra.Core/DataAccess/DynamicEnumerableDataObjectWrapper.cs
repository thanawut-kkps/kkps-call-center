using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Phatra.Core.DataAccess
{
    public abstract class DynamicEnumerableDataObjectWrapper<T> :
           DynamicDataObjectWrapper<T>, IEnumerable
           where T : IEnumerable
    {
        public DynamicEnumerableDataObjectWrapper(T obj)
            : base(obj)
        {
        }

        public virtual IEnumerator GetEnumerator()
        {
            return Obj.GetEnumerator();
        }
    }
}
