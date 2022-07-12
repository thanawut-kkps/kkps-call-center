using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phatra.Core.Utilities
{
    public class DbConvertor
    {
        public static string ToString(object dbValue)
        {
            if (DBNull.Value.Equals(dbValue))
                return string.Empty;


            return Convert.ToString(dbValue);
        }

        public static decimal ToDecimal(object dbValue)
        {
            if (DBNull.Value.Equals(dbValue))
                return 0;

            return Convert.ToDecimal(dbValue);
        }

        public static decimal? ToDecimalNullable(object dbValue)
        {
            if (DBNull.Value.Equals(dbValue))
                return null;

            return Convert.ToDecimal(dbValue);
        }

        public static DateTime ToDateTime(object dbValue)
        {
            if (DBNull.Value.Equals(dbValue))
                return DateTime.MinValue;

            return Convert.ToDateTime(dbValue);
        }

        public static DateTime? ToDateTimeNullable(object dbValue)
        {
            if (DBNull.Value.Equals(dbValue))
                return null;

            return Convert.ToDateTime(dbValue);
        }

        public static int ToInt(object dbValue)
        {
            if (DBNull.Value.Equals(dbValue))
                return 0;

            return Convert.ToInt32(dbValue);
        }

        public static int? ToIntNullable(object dbValue)
        {
            if (DBNull.Value.Equals(dbValue))
                return null;

            return Convert.ToInt32(dbValue);
        }
    }
}
