using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPDotNetFrameWorkMVCBasicCourse.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Demo()
        {

            ViewBag.Content = "This is Data of DemoController Index Action.";

            return View();
        }
    }
}