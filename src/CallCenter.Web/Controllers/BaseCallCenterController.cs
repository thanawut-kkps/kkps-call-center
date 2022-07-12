using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phatra.Core.Web.Mvc;
using BootstrapSupport;

namespace CallCenter.Web.Controllers
{
    public class BaseCallCenterController : BaseController
    {
        public void Attention(string message)
        {
            AddItemToTempData(Alerts.ATTENTION, message);
        }

        public void Success(string message)
        {
            AddItemToTempData(Alerts.SUCCESS, message);
        }

        public void Information(string message)
        {
            AddItemToTempData(Alerts.INFORMATION, message);
        }

        private void AddItemToTempData(string key, string value)
        {
            if (TempData.ContainsKey(key))
            {
                TempData[key] += "<br/>" + value;
            }
            else 
            {
                TempData.Add(key, value);
            }
        }

        public void Error(string message)
        {
            AddItemToTempData(Alerts.ERROR, message);
        }

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
