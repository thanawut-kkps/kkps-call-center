using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.Web.ViewModels
{
    public class BaseViewModel
    {
        public string AgentId { get; set; }
        public string CallSession { get; set; }
    }
}