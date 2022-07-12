using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Phatra.Core.Utilities
{
    public class ConfigurationHelper
    {
        public static string GetAppSettingValue(string key)
        {
            if (ConfigurationManager.AppSettings[key] == null)
            {
                throw new ConfigurationErrorsException("AppSettings name '" + key + "' was not configured in web.config");
            }
            return ConfigurationManager.AppSettings[key].ToString();
        }
    }
}
