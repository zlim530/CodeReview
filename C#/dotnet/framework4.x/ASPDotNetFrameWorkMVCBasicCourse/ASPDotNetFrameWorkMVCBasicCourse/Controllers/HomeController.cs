using ASPDotNetFrameWorkMVCBasicCourse.Models;
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
            Request.Files["file"].SaveAs(Request.MapPath("~/uploads/" + Request.Files["file"].FileName));
            return Content("Ok");
        }

        public ActionResult ResponseData()
        {
            // Response.Write: 向客户端输出内容
            // Response.Write("Hello,World!");
            // Response.Redirect: 重定向
            // Response.Redirect("https://www.baidu.com");
            return Content("");
        }

        public ActionResult RequestHeader()
        {
            Response.Headers["Hello"] = "World";
            return Content(Request.Headers["token"]);
        }

        public ActionResult SessionData()
        {
            /*
            * Session 会话：数据保存在服务器中，存储少量且重要的数据比如账户
            * Session 是一个键值对
            * Session 的存活时间 20min
            * Session 销毁：Abandon/Clear
            */
            Session["user"] = Request.Form["user"];
            return Content($"会话中的数据是：{Session["user"]}");
        }

        public ActionResult GetSession()
        {
            return Content($"会话中的数据是：{Session["user"]}");
        }

        public ActionResult ClearSession()
        {
            Session.Abandon();
            return Content("Cleared.");
        }

        public ActionResult CookieSave()
        {
            // 时效性设置
            Response.Cookies.Add(new HttpCookie("token")
            { 
                Value = "zlim530",
                Expires = DateTime.Now.AddDays(1)
            });
            return Content("OK!");
        }

        public ActionResult CookieGet()
        {
            return Content(Request.Cookies["token"].Value);
        }

        public ActionResult CookieClear()
        {
            Response.Cookies.Add(new HttpCookie("token")
            { 
                Expires = DateTime.Now.AddDays(-1)
            });
            return Content("OK.");
        }

        public ActionResult ApplicationData()
        {
            HttpContext.Application["user"] = "123";
            return Content("");
        }

        public ActionResult ApplicationGet()
        {
            return Content(HttpContext.Application["user"].ToString());
        }

        public ActionResult ServerDemo()
        {
            return Content("");
        }

        public ActionResult ShowDemo()
        {
            return Content("This is content.");
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


        public ActionResult Demo()
        {
            // 一般存放一些不主要的数据
            ViewBag.Content = "This is Data of DemoController Index Action.";
            ViewBag.ZName = "Tim";
            ViewBag.ZAge = 20;
            ViewData["Age"] = 111;
            return View();
        }

        public ActionResult ShowData()
        {

            return View("ShowData2", new Student()
            { 
                Id = 1,
                Name = "Tom",
                Age = 20
            });
        }

    }
}