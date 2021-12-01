using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPDotNetFrameWorkMVCBasicCourse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*
            *   内置对象    Request     Response    Session     Cookie      Application     Server
            *               请求          响应      会话          客户端数据    当前网站对象     服务器对象
            *               
            *   Request 服务器接收客户端数据
            *   Request.Query：get 请求，浏览器地址栏 ? 后的字符串
            *       http://localhost:6143/?name=Tim&age=23
            *   Request.Form：post 请求，表单数据
            *   Request.MapPath()：将虚拟路径转换为物理路径
            *   Request.Files：post 请求的文件（文件上传）
            */
            return Content($"{Request.QueryString["name"]} - {Request.QueryString["age"]}");
            //return View();
        }

        public ActionResult PostData()
        {
            return Content(Request.Form["loginname"]);
        }

        public ActionResult FileData()
        {
            // SaveAs 方法需要物理路径
            Request.Files["file"].SaveAs(Request.MapPath("~/uploads" + Request.Files["file"].FileName));
            return Content("Ok");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}