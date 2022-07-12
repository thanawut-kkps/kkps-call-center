using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phatra.Core.Logging;

namespace CallCenter.Web.Controllers
{
    public class CommonController : BaseCallCenterController
    {
        private ILog _log;

        public CommonController(ILog log)
        {
            _log = log;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendClientErrorMessage(string _errorMessage)
        {
            var decodedMessage = HttpUtility.HtmlDecode(_errorMessage);
            _log.Error("[JAVASCRIPT Error][Browser:{0} Version:{1}][{2}]:{3}", Request.Browser.Browser, Request.Browser.Version, this.User.Identity.Name, decodedMessage);
            return Json(new { valid = true, message = decodedMessage });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendClientMessageToServer(string _message)
        {
            var decodedMessage = HttpUtility.HtmlDecode(_message);
            _log.Warn("[Browser:{0} Version:{1}][{2}]:{3}", Request.Browser.Browser, Request.Browser.Version, this.User.Identity.Name, decodedMessage);
            return Json(new { valid = true, message = decodedMessage });
        }


    }
}
