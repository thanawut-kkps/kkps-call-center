using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phatra.CallCenter.Data
{
    public class CallLogInfoItem
    {
        public string call_session { get; set; }
        public string customer_phone { get; set; }
        public decimal? client_id { get; set; }
        public string account_no { get; set; }
        public int? priority { get; set; }
        public string skill { get; set; }
        public string agent_id { get; set; }
        public string agent_first_name { get; set; }
        public string agent_last_name { get; set; }
        public string agent_extension { get; set; }
        public string call_type { get; set; }
        public DateTime? call_starttime { get; set; }
        public DateTime? call_endtime { get; set; }
    }
}
