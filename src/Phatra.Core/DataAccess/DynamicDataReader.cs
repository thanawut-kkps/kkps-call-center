using System;
using System.Data;
using System.Dynamic;
using System.Reflection;

namespace Phatra.Core.DataAccess
{
    //public class DynamicDataReader : DynamicEnumerableDataObjectWrapper<DbDataReader>
    //{
    //    public DynamicDataReader(DbDataReader reader)
    //        : base(reader)
    //    {
    //    }

    //    public override bool TryGetMember(GetMemberBinder binder, out object result)
    //    {
    //        if (base.TryGetMember(binder, out result))
    //            return true;
    //        else
    //        {
    //            try
    //            {
    //                if (!Obj.IsDBNull(Obj.GetOrdinal(binder.Name)))
    //                    result = Obj.GetValue(Obj.GetOrdinal(binder.Name));
    //                else
    //                    result = null;
    //                return true;
    //            }
    //            catch (Exception)
    //            {
    //                result = null;
    //                return false;
    //            }
    //        }
    //    }

    //    public override bool TryGetIndex(GetIndexBinder binder,
    //           object[] indexes, out object result)
    //    {
    //        try
    //        {
    //            object index = indexes[0];
    //            if (index is int)
    //            {
    //                int intIndex = (int)index;
    //                if (!Obj.IsDBNull(intIndex))
    //                    result = Obj.GetValue(intIndex);
    //                else
    //                    result = null;
    //                return true;
    //            }
    //            else if (index is string)
    //            {
    //                string strIndex = (string)index;
    //                if (!Obj.IsDBNull(Obj.GetOrdinal(strIndex)))
    //                    result = Obj.GetValue(Obj.GetOrdinal(strIndex));
    //                else
    //                    result = null;
    //                return true;
    //            }
    //            else
    //            {
    //                result = null;
    //                return false;
    //            }
    //        }
    //        catch (Exception)
    //        {
    //            result = null;
    //            return false;
    //        }
    //    }

    //    public static implicit operator DbDataReader(DynamicDataReader reader)
    //    {
    //        return reader.Obj;
    //    }

    //    public static explicit operator DynamicDataReader(DbDataReader reader)
    //    {
    //        return new DynamicDataReader(reader);
    //    }
    //}

    /// <summary>
    /// This class provides an easy way to use object.property
    /// syntax with a DataReader by wrapping a DataReader into
    /// a dynamic object.
    /// 
    /// The class also automatically fixes up DbNull values
    /// (null into .NET and DbNUll)
    /// </summary>
    public class DynamicDataReader : DynamicObject
    {
        /// <summary>
        /// Cached Instance of DataReader passed in
        /// </summary>
        IDataReader DataReader;

        /// <summary>
        /// Pass in a loaded DataReader
        /// </summary>
        /// <param name="dataReader">DataReader instance to work off</param>
        public DynamicDataReader(IDataReader dataReader)
        {
            DataReader = dataReader;
        }

        /// <summary>
        /// Returns a value from the current DataReader record
        /// If the field doesn't exist null is returned.
        /// DbNull values are turned into .NET nulls.
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = null;

            // 'Implement' common reader properties directly
            if (binder.Name == "IsClosed")
                result = DataReader.IsClosed;
            else if (binder.Name == "RecordsAffected")
                result = DataReader.RecordsAffected;
            // lookup column names as fields
            else
            {
                try
                {
                    result = DataReader[binder.Name];
                    if (result == DBNull.Value)
                        result = null;
                }
                catch
                {
                    result = null;
                    return false;
                }
            }

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            // Implement most commonly used method
            if (binder.Name == "Read")
                result = DataReader.Read();
            else if (binder.Name == "Close")
            {
                DataReader.Close();
                result = null;
            }
            else if (binder.Name == "GetSchemaTable")
            { 
                result = DataReader.GetSchemaTable();
            }
            else if (binder.Name == "NextResult")
            {
                result = DataReader.NextResult();
            }
            else
            {
                // call other DataReader methods using Reflection (slow - not recommended)
                // recommend you use full DataReader instance
                MethodInfo theMethod = DataReader.GetType().GetMethod(binder.Name);
                result = theMethod.Invoke(DataReader, args);
                //result = ReflectionUtils.CallMethod(DataReader, binder.Name, args);
            }
            return true;
        }
    }

}
