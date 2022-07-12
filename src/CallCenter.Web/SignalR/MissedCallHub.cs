using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CallCenter.Web.SignalR
{
    public class MissedCallHub : Hub
    {
        public void NotifyOutboundCall(string phoneNumber)
        {
            Clients.Others.notifyStartingOutboundCall(phoneNumber);
        }
    }
}