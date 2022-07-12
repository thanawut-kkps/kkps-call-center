using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Phatra.Core.Web.Constants;

namespace System.Web.Mvc
{
    public static partial class FormatExtension
    {
        public static string ToDisplay(this DateTime? source)
        {
            return source.HasValue ? source.Value.ToString(DateTimeConst.Format) : string.Empty;   
        }
    }
}