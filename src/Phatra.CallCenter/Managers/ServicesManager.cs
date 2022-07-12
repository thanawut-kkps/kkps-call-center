using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using Phatra.Core.Managers;
using Phatra.CallCenter.Data;
using Phatra.Core.Utilities;

using Phatra.Core;

namespace Phatra.CallCenter.Managers
{
    public class ServicesManager : BaseManager, IServicesManager
    {
        private static SqlHelper _sqlHelper;

        private SqlHelper SqlConnection
        {
            get
            {
                if (_sqlHelper == null)
                {
                    var strConn = ConfigurationManager.ConnectionStrings["ExtServiceDATABASE"].ToString();
                     
                    if (string.IsNullOrWhiteSpace(strConn))
                    {
                        throw new Exception("AppSettings name 'ExtServiceDATABASE' was not configured in web.config");
                    }

                    _sqlHelper = new SqlHelper(strConn);
                }

                return _sqlHelper;                
            }
        }

        public string SaveCallLogLnfo(CallLogInfoItem data)
        {
            String msg = "";
            try
            {
                var msgObj = SqlConnection.ExecuteScalar("dbo.usp_cc_save_call_log_info",
                                                                        data.call_session,
                                                                        data.call_starttime,
                                                                        data.customer_phone,
                                                                        data.client_id,
                                                                        data.account_no,
                                                                        data.priority,
                                                                        data.skill,
                                                                        data.agent_id,
                                                                        data.agent_first_name,
                                                                        data.agent_last_name,
                                                                        data.agent_extension,
                                                                        data.call_type);

                msg = msgObj != null ? msgObj.ToString() : "0";
                msg = msg == "0" ? "OK" : msg;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Error!") >= 0)
                    msg = ex.Message.Substring(ex.Message.IndexOf("Error!"));
                else
                    msg = ex.Message;
            }

            return msg;
        }

        public string UpdateEndCallLogLnfo(CallLogInfoItem data)
        {
            String msg = "";
            try
            {
                var msgObj = SqlConnection.ExecuteScalar("dbo.usp_cc_update_end_call_log_info",
                                                                        data.call_session,
                                                                        data.call_endtime);

                msg = msgObj != null ? msgObj.ToString() : "0";
                msg = msg == "0" ? "OK" : msg;
            }
            catch (Exception ex)
            {
                if (ex.Message.IndexOf("Error!") >= 0)
                    msg = ex.Message.Substring(ex.Message.IndexOf("Error!"));
                else
                    msg = ex.Message;
            }

            return msg;
        }        
    }

    public interface IServicesManager
    {
        string SaveCallLogLnfo(CallLogInfoItem data);
        string UpdateEndCallLogLnfo(CallLogInfoItem data);                
    }
}
