using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace FirstApplication.Models.Filters
{
    public class MyLogActionFilter : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ResultExecutingContext filterContext)
        //{

          //  Log("OnActionExecuting", filterContext.RouteData);
           //base.OnActionExecuting(filterContext);
        //}

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];

            var message = String.Format(
               "{0} controller:{1} action:{2}", methodName, controllerName, actionName);

            Debug.WriteLine(message, "Action Filter Log");
        }
    }
}