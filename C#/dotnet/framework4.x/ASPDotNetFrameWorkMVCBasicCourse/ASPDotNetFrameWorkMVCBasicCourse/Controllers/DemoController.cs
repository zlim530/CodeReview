using System.Web.Mvc;

namespace ASPDotNetFrameWorkMVCBasicCourse.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            ViewBag.Content = "This is Data of DemoController Index Action.";

            return View();
            //return Content("This is Data of DemoController Index Action.");
        }
    }
}