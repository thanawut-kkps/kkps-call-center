using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallCenter.Web.ViewModels
{
    public class ConnectedUserViewModel
    {
        public string Name { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }
}