using System;
using System.Dynamic;
using System.Reflection;

namespace Phatra.Core.DataAccess
{
    public abstract class DynamicDataObjectWrapper<T> : DynamicObject
    {
        protected T Obj { get; private set; }
        protected Type ObjType { get; private set; }

        public DynamicDataObjectWrapper(T obj)
        {
            this.Obj = obj;
            this.ObjType = obj.GetType();
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder,
               object[] args, out object result)
        {
            try
            {
                result = ObjType.InvokeMember(binder.Name,
                  BindingFlags.InvokeMethod | BindingFlags.Instance |
                  BindingFlags.Public, null, Obj, args);
                return true;

            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            PropertyInfo property = ObjType.GetProperty(binder.Name,
               BindingFlags.Instance | BindingFlags.Public);
            if (property != null)
            {
                result = property.GetValue(Obj, null);
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            PropertyInfo property = ObjType.GetProperty(binder.Name,
                   BindingFlags.Instance | BindingFlags.Public);
            if (property != null)
            {
                property.SetValue(Obj, value, null);
                return true;
            }
            else
                return false;
        }
    }
}
