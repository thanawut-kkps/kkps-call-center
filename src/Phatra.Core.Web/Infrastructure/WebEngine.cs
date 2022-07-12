using System;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Phatra.Core.Infrastructure;
using Phatra.Core.Configuration;
using Phatra.Core.Web.Infrastructure.DependencyManagement;
using Phatra.Core.Infrastructure.DependencyManagement;

namespace Phatra.Core.Web.Infrastructure
{
    public class WebEngine : BaseEngine
    {
        protected override void SetDependencyResolver(IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        protected override ITypeFinder CreateTypeFinder(IEngineConfig config)
        {
            return new WebAppTypeFinder(config);
        }

        protected override IContainerManager CreateContainerManager(IContainer container)
        {
            return new WebContainerManager(container);
        }
    }
}
