using FirstApplication.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstApplication.Controllers
{
    [MyLogActionFilter]
    public class FilterTestController : Controller
    {
        // GET: FilterTest
        [OutputCache(Duration = 15)]
        public string index()
        {
            return "This is ASP.NET MVC Filters Tutorial";
        }
        [OutputCache(Duration = 20)]
        [ActionName("CurrentTime")]
        public string GetCurrentTime()
        {
            return TimeString();
            //return DateTime.Now.ToString("T");
        }

        [NonAction]
        public string TimeString()
        {
            return "Time is " + DateTime.Now.ToString("T");
        }

        public ActionResult Search(string name = "No name Entered")
        {
            var input = Server.HtmlEncode(name);
            return Content(input);
        }
    }
}