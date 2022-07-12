using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phatra.Core.Web.Mvc;

namespace CallCenter.Web.Controllers
{
    public class BaseCallCenterController : BaseController
    {       
        protected override ActionResult BusinessErrorDetailView(ControllerBase controller, Exception exception)
        {
            return View("_ModelStateInvalid");
        }

        protected override string UnHandlerErrorDetailViewName
        {
            get
            {
                return "_UnHandlerErrorDetail";
            }
        }

        protected override string ModelStateInvalidViewName
        {
            get
            {
                return "_ModelStateInvalid";
            }
        }

    }
}
