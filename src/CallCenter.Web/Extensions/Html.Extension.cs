using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;


namespace System.Web.Mvc
{
    public static partial class HtmlExtension
    {
        public static string GetAssemblyVersion(this HtmlHelper helper)
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}