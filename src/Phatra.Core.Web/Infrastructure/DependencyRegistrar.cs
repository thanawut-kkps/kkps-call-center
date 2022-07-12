using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phatra.Core.Infrastructure.DependencyManagement;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web;
using Phatra.Core.Web.Fakes;
using System.Configuration;
using Phatra.Core.Web;
using Phatra.Core.Logging;

namespace Phatra.Core.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            // Logging 
            builder.RegisterModule<LoggingModule>();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
    
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
