using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phatra.Core.Logging;
using Phatra.CallCenter.Managers;
using Phatra.CallCenter.Data;
using System.Xml;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace CallCenter.Web.Controllers
{
    public class ServicesController : BaseCallCenterController
    {
        private ILog _log;
        private IServicesManager _servicesManager;

        public ServicesController()
        {
            this._log = new Log4NetLogger(this.GetType());
            this._servicesManager = new ServicesManager();            
        }

        public ActionResult LogCallInformation(
            string call_session,
            DateTime? call_starttime,
            string customer_phone,
            decimal? client_id,
            string account_no,
            int? priority,
            string skill,
            string agent_id,
            string agent_first_name,
            string agent_last_name,
            string agent_extension,
            string call_type)
        {
            string error = "";

            _log.Error("URI : " + Request.Url.AbsoluteUri);
                
            if (call_type != "5")
            {
                CallLogInfoItem theCallLogInfoItem = new CallLogInfoItem();

                theCallLogInfoItem.call_session = call_session;
                theCallLogInfoItem.customer_phone = customer_phone;

                if (theCallLogInfoItem.customer_phone.Length > 4)
                {
                    theCallLogInfoItem.customer_phone = "0" + theCallLogInfoItem.customer_phone;
                }

                if (client_id.HasValue)
                    theCallLogInfoItem.client_id = client_id.Value;

                theCallLogInfoItem.account_no = account_no;

                if (priority.HasValue)
                    theCallLogInfoItem.priority = priority.Value;

                if (call_starttime.HasValue)
                    theCallLogInfoItem.call_starttime = call_starttime.Value;
                //else
                //    theCallLogInfoItem.call_starttime = DateTime.Now;

                theCallLogInfoItem.skill = skill;
                theCallLogInfoItem.agent_id = agent_id;
                theCallLogInfoItem.agent_first_name = agent_first_name;
                theCallLogInfoItem.agent_last_name = agent_last_name;
                theCallLogInfoItem.agent_extension = agent_extension;
                theCallLogInfoItem.call_type = call_type;

                string result = _servicesManager.SaveCallLogLnfo(theCallLogInfoItem);

                if (result != "OK")
                {
                    error += result;
                    error += "\r\n";
                }
            }
            else
            {
                CallLogInfoItem theCallLogInfoItem = new CallLogInfoItem();
                theCallLogInfoItem.call_session = call_session;
                theCallLogInfoItem.call_endtime = DateTime.Now;


                string result = _servicesManager.UpdateEndCallLogLnfo(theCallLogInfoItem);

                if (result != "OK")
                {
                    error += result;
                    error += "\r\n";
                }
            }

            ArrayList responseItemList = new ArrayList();
            LogCallInformationServiceItem itemXMLResult;

            if (error == "")
            {
                itemXMLResult = new LogCallInformationServiceItem();
                itemXMLResult.param = "result";
                itemXMLResult.value = "success";
            }
            else
            {
                itemXMLResult = new LogCallInformationServiceItem();
                itemXMLResult.param = "result";
                itemXMLResult.value = error;
            }

            if (error != "")
            {
                _log.Error("=================== Start LogCallInformation Service Error ===================");                
                _log.Error("Error >> " + error);
                _log.Error("=================== End LogCallInformation Service Error ===================");
            }

            responseItemList.Add(itemXMLResult);

            LogCallInformationServiceItems XMLResult = new LogCallInformationServiceItems();
            XMLResult.Items = (LogCallInformationServiceItem[])responseItemList.ToArray(typeof(LogCallInformationServiceItem));

            StringWriter stringXmlResult = new UTF8StringWriter();
            XmlSerializer theXmlSerializer = new XmlSerializer(typeof(LogCallInformationServiceItems));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);
            theXmlSerializer.Serialize(stringXmlResult, XMLResult, namespaces);            

            return this.Content(stringXmlResult.ToString(), "text/xml", Encoding.UTF8);
        }
    }

    class UTF8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }
    }

}
