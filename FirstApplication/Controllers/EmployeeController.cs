using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstApplication.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Search(string name = "No name Entered")
        {
            var input = Server.HtmlEncode(name);
            return Content(input);
        }
        [HttpGet]
        public ActionResult Search()
        {
            var input = "Another Search Action";
            return Content(input);
        }
    }
}