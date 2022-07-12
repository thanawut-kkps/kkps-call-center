using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Phatra.Core.Logging;
using Phatra.Core.Web.Infrastructure;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using CallCenter.Web.ViewModels;

namespace CallCenter.Web.SignalR
{
    public class NotificationHub : Hub
    {
        private static readonly ConcurrentDictionary<string, ConnectedUserViewModel> Users = new ConcurrentDictionary<string, ConnectedUserViewModel>();

        private ILog log = new Log4NetLogger(typeof(NotificationHub));

        private HttpContextBase _httpContext;
        public HttpContextBase HttpContext
        {
            get
            {
                if (_httpContext == null)
                {
                    _httpContext = WebContext.Current.Resolve<HttpContextBase>();
                }
                return _httpContext;
            }
        }

        public static List<string> GetOnlineUsers()
        {
            return Users.Select(u => u.Key).ToList();
        }

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            var user = Users.GetOrAdd(userName, s =>
            {
                log.Debug("[CONNECTED] User:'{0}' has connected to system ... ", userName);

                return new ConnectedUserViewModel()
                {
                    Name = userName,
                    ConnectionIds = new HashSet<string>()
                };
            });

            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
                //Clients.AllExcept(user.ConnectionIds.ToArray()).userConnected(userName);
            }

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {

            string userName = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;

            ConnectedUserViewModel user;
            Users.TryGetValue(userName, out user);

            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

                    if (!user.ConnectionIds.Any())
                    {
                        ConnectedUserViewModel removedUser;
                        Users.TryRemove(userName, out removedUser);

                        log.Debug("[DISCONNECTED] User:'{0}' has disconnected from system ... ", userName);

                        // You might want to only broadcast this info if this 
                        // is the last connection of the user and the user actual is 
                        // now disconnected from all connections.
                        //Clients.Others.userDisconnected(userName);
                    }
                }
            }

            return base.OnDisconnected();
        }

        public void NotifyToOther(string message)
        {
            // Call the broadcastMessage method to update clients.
            log.Debug("{0}:{1}:{2}", HttpContext.User.Identity.Name, Context.ConnectionId, message);
            Clients.Others.nofityInfoMessage(message);
        }

    }
}