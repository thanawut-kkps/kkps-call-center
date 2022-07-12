using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Phatra.Finesse.DesktopAPIs;
using Phatra.Finesse.DesktopAPIs.Data;

namespace CallCenter_Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView

            User user = UserApiHelper.Instance.GetUser("pairaang");   

            return View();
        }
        
        public ActionResult GridViewPartialView() 
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView");
        }
    
    }
}