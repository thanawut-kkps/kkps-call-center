using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Phatra.Core.Infrastructure.DependencyManagement;
using Phatra.Core.Caching;
using Phatra.CallCenter.Managers;

namespace CallCenter.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, Phatra.Core.Infrastructure.ITypeFinder typeFinder)
        {
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().SingleInstance();
            // ================= Manager =========================
            builder.RegisterType<CrmManager>().As<ICrmManager>().InstancePerLifetimeScope();
            builder.RegisterType<ServicesManager>().As<IServicesManager>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; }
        }
    }
}