using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Phatra.Finesse.DesktopAPIs.Data
{
    [XmlRoot("User")]
    public class User
    {
        [XmlElement("dialogs")]
        public string Dialogs { get; set; }
        [XmlElement("extension")]
        public string Extension { get; set; }
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [XmlElement("lastName")]
        public string LastName { get; set; }
        [XmlElement("loginId")]
        public string LoginId { get; set; }
        [XmlElement("loginName")]
        public string LoginName { get; set; }
        [XmlElement("pendingState")]
        public string PendingState { get; set; }
        [XmlElement("reasonCodeId")]
        public string ReasonCodeId { get; set; }
         //public Role[] roles { get; set; }

        [XmlElement("state")]
        public string State { get; set; }
    }
}
