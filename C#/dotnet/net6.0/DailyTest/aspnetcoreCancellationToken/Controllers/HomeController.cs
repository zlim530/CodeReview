using aspnetcoreCancellationToken.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace aspnetcoreCancellationToken.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            // 不需要管这个参数怎么来的，这是 ASP .NET Core 框架内部处理的，我们只负责转发即可
            // 当用户访问别的网站时此时就会停止此方法的调用
            //await DownloadHtmlAsync("https://www.baidu.com", 100 ,cancellationToken);
            // 如果我们不转发 CancellationToken 参数，则当用户已经离开此程序的服务站点，但此方法仍在执行着
            //await DownloadHtmlAsync("https://www.baidu.com", 100 ,CancellationToken.None);
            return View();
        }

        static async Task DownloadHtmlAsync(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    var resp = await client.GetAsync(url, cancellationToken);
                    var html = await resp.Content.ReadAsStringAsync();
                    Debug.WriteLine(html);
                }
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}