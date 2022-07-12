using System;
using System.Web.Mvc;
using System.Linq;
using Phatra.Core.Utilities;
using Phatra.Core.Exceptions;
using System.Web;
using System.IO.Compression;
using Phatra.Core.Logging;

namespace Phatra.Core.Web.Mvc
{
    public abstract class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            // Do anything to intercept the Initialize event.
            base.Initialize(requestContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Request.IsAjaxRequest() && !filterContext.IsChildAction)
            {

                var actionName = (string)filterContext.RouteData.Values["Action"];
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                Logger.Debug("[{0}] is executing action {1}.{2}", this.User.Identity.Name, controllerName, actionName);
            }
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            //this.CompresssResponse();
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            var viewName = string.Empty;
            var message = string.Empty;
            string errorMessage = string.Empty;
            Logger.Debug("************* Start OnException ({0}) ***************************", this.User.Identity.Name);

            errorMessage = ExtractExceptionToErrorMessage(filterContext.Exception);

            if (Request.IsAjaxRequest())
            {
                if (this.IsBusinessException(filterContext.Exception))
                {
                    Logger.Debug("Error is Business Exception.");
                    //message = this.RenderPartialViewToString(this.ModelStateInvalidViewName);
                    message = errorMessage;
                    Logger.Debug("HTML String view render to browser is::" + message);
                }
                else
                {
                    Logger.Debug("Error is Unhandle or Technical Exception .");
                    //message = this.RenderPartialViewToString(this.UnHandlerErrorDetailViewName);
                    message = errorMessage;
                    Logger.Debug("HTML String view render to browser is::" + message);
                }

                //filterContext.Result= Json(new { valid = false, message = message });
                filterContext.Result = new JsonResult() { JsonRequestBehavior = JsonRequestBehavior.AllowGet, Data = new { valid = false, message = message } };
            }
            else
            {
                viewName = this.ViewBag.ViewName;
                var model = this.ViewBag.Model;
                if (this.IsBusinessException(filterContext.Exception))
                {
                    Logger.Debug("No Ajax Request Error is Business Exception.");
                    if (viewName == null || model == null)
                        filterContext.Result = BusinessErrorDetailView(filterContext.Controller, filterContext.Exception);
                    else
                        filterContext.Result = View(viewName, model);
                }
                else
                {
                    Logger.Debug("No Ajax Request Error is Unhandle or Technical Exception .");
                    Logger.Debug("ErrorMessage is: " + errorMessage);

                    if (ControllerContext.IsChildAction)
                    {
                        filterContext.Result = PartialView(this.UnHandlerErrorDetailViewName);
                    }
                    else
                    {
                        filterContext.Result = View(this.UnHandlerErrorDetailViewName);
                    }
                }
            }

            Logger.Debug("************* End OnException ***************************");

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            filterContext.HttpContext.Response.StatusCode = 200;
            base.OnException(filterContext);

        }

        protected bool IsBusinessException(object exception)
        {
            if (exception.GetType() == typeof(Phatra.Core.Exceptions.BaseBusinessException)) return true;
            if (exception.GetType().BaseType == typeof(Phatra.Core.Exceptions.BaseBusinessException)) return true;
            if (exception is Phatra.Core.Exceptions.BaseBusinessException) return true;
            return false;
        }

        protected ActionResult ModelStateInValid(string viewName, object model)
        {
            if (Request.IsAjaxRequest())
            {
                this.AddModelStateError();
                return Json(new { valid = false, message = this.RenderPartialViewToString("_ModelStateInvalid", model) });
            }
            return View(viewName, model);
        }

        protected ActionResult ModelStateInValid(object model)
        {
            return this.ModelStateInValid(this.RouteData.Values["action"].ToString(), model);
        }

        protected virtual void AddModelStateError()
        {
            var modelError = (from m in ModelState where m.Value.Errors.Count > 0 select m).ToList();
            foreach (var model in modelError)
            {
                if (model.Key == string.Empty) continue;
                var error = ModelState[model.Key].Errors;
                var countError = error.Count;
                Logger.Debug("The invalid data are the following:");
                Logger.Debug(model.Key + " Error is ");
                for (int i = 0; i < countError; i++)
                {
                    Logger.Debug(error[i].ErrorMessage);
                    ModelState.AddModelError(string.Empty, error[i].ErrorMessage);
                }
            }
        }

        protected string ExtractExceptionToErrorMessage(Exception exception)
        {
            string errorMessage = string.Empty;
            var message = string.Empty;
            if (this.IsBusinessException(exception))
            {
                BaseBusinessException buEx = (BaseBusinessException)exception;
                errorMessage = buEx.ErrorMessage;
                Logger.Debug("BusinessException: " + buEx.ErrorMessage);
                Logger.Debug("Exception.ToString(): " + exception.ToString());
            }
            else
            {
                //Other error or Technical error
                errorMessage = exception.Message;

                //Logger.Error("Other Exception: " + errorMessage);
                Logger.Error("Exception.ToString(): " + exception.ToString());
            }

            ModelState.AddModelError(string.Empty, errorMessage);
            this.AddModelStateError();

            return errorMessage;
        }

        protected abstract ActionResult BusinessErrorDetailView(ControllerBase controller, Exception exception);

        protected abstract string UnHandlerErrorDetailViewName
        {
            get;
        }

        protected abstract string ModelStateInvalidViewName
        {
            get;
        }

        ILog _log;

        private ILog Logger
        {
            get
            {
                if (_log == null)
                {
                    _log = LoggingModule.GetLogger(this.GetType());
                }
                return _log;
            }
        }

        private void CompresssResponse(ActionExecutedContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;

            string acceptEncoding = request.Headers["Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();

            HttpResponseBase response = filterContext.HttpContext.Response;

            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }

    }
}
