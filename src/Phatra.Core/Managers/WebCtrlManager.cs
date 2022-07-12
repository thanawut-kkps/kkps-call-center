using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using Phatra.Core.Utilities;
using System.Data;
using Phatra.Core.Logging;

namespace Phatra.Core.Managers
{
    public class WebCtrlManager : BaseManager
    {

        private static string WebCtrlDBAlias = "WebCtrlDB";
        private string _WebCtrlsConnectionString;

        static ILog log = new Log4NetLogger(typeof(WebCtrlManager));

        public WebCtrlManager(string connectionString)
        {
            _WebCtrlsConnectionString = connectionString;
        }


        private static WebCtrlManager _WebCtrlManager;
        public static WebCtrlManager Instance
        {
            get 
            {
                if (_WebCtrlManager == null)
                {
                    var defaultConnectionString = ConfigurationHelper.GetAppSettingValue(WebCtrlDBAlias);
                    if (string.IsNullOrWhiteSpace(defaultConnectionString))
                    {
                        throw new ConfigurationErrorsException("AppSettings name 'WebCtrlDB' was not configured in web.config");
                    }
                    _WebCtrlManager = new WebCtrlManager(defaultConnectionString);
                }
                return _WebCtrlManager;
            }
        }

        public bool HasPagePrivilage(string url)
        {
            var sqlHeler = new SqlHelper(_WebCtrlsConnectionString);
            var a = sqlHeler.ExecuteScalar("USP_IsPriv", url).ToString();
            if (a != "Y")
            {
                return false;
            }
            return true;
        }

        public string GetNoPrivilegePage()
        {
            var sqlHeler = new SqlHelper(_WebCtrlsConnectionString);
            return sqlHeler.ExecuteScalar("USP_Gen_Url", "WebCtrl", "NoPriv.aspx").ToString();
        }

        public string GetMenuStep(string url)
        {
            var sqlHeler = new SqlHelper(_WebCtrlsConnectionString);
            return sqlHeler.ExecuteScalar("USP_get_MenuStep", url).ToString();
        }

        public string GetPageLabel(string url)
        {
            var sqlHeler = new SqlHelper(_WebCtrlsConnectionString);
            return sqlHeler.ExecuteScalar("USP_get_Label", url).ToString();
        }

        public bool IsProduction()
        {
            var sqlHeler = new SqlHelper(_WebCtrlsConnectionString);
            var strRestult = sqlHeler.ExecuteScalar(CommandType.Text, "SELECT [dbo].[UFN_Util_IsProdServer]()").ToString();
            if (strRestult == "N")
            {
                return false;
            }
            return true;

        }
        public bool IsUAT()
        {
            var sqlHeler = new SqlHelper(_WebCtrlsConnectionString);
            string strRestult = string.Empty;

            try
            {
                strRestult = sqlHeler.ExecuteScalar(CommandType.Text, "SELECT [dbo].[UFN_Util_IsUAT2Server]()").ToString();
                if (strRestult == "N")
                {
                    strRestult = sqlHeler.ExecuteScalar(CommandType.Text, "SELECT [dbo].[UFN_Util_IsUATServer]()").ToString();
                    if (strRestult == "N")
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                throw;
            }

            return true;

        }
    }
}
