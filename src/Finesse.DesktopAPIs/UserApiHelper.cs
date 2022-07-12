using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phatra.Core.Utilities;   
using Phatra.Finesse.DesktopAPIs.Data;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using Phatra.Core.Logging;   

namespace Phatra.Finesse.DesktopAPIs
{
    public class UserApiHelper
    {
        private string _userName;
        private string _password;
        private ILog _log;


        private Uri FinesseServerUri
        {
            get
            {
                return new Uri(ConfigurationHelper.GetAppSettingValue("FinesseServerUrl"));
            }
        }

        public UserApiHelper(string userName, string password)
        {
            _userName = userName;
            _password = password;
            _log = new Log4NetLogger(typeof(UserApiHelper));
        }

        private static UserApiHelper _instance;
        public static UserApiHelper Instance
        {
            get
            {
                if (_instance == null)
                {

                    var userName = ConfigurationHelper.GetAppSettingValue("finesseAdminUserName");
                    var password = ConfigurationHelper.GetAppSettingValue("finesseAdminPassword");
                    _instance = new UserApiHelper(userName, password);
                }
                return _instance;
            }
        }


        public User GetUser(string agentId)
        {
            User result = null;
            Uri uri = new Uri(FinesseServerUri, @"finesse/api/User/" + agentId);

            WebRequest req = WebRequest.Create(uri);
            req.Method = "GET";
            req.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("username:password"));
            req.Credentials = new NetworkCredential(this._userName, this._password);

            using (var resp = (HttpWebResponse)req.GetResponse())
            {
                using (var streamReader = new StreamReader(resp.GetResponseStream()))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(User));
                    var objText = streamReader.ReadToEnd();
                    using (TextReader textReader = new StringReader(objText))
                    {
                        result = (User)serializer.Deserialize(textReader);
                    }
                }
            }

            return result;
        }
   
    }
}
