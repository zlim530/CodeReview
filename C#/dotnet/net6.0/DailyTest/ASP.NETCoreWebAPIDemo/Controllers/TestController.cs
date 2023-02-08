using ASP.NETCoreWebAPIDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPIDemo.Controllers
{
    //[Route("api/[controller]")]
    // route 路径规则不仅仅使用 HTTP 谓词匹配，还加上方法名来匹配
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public Person GetPerson()
        {
            return new("ZLim",25);
        }

        [HttpGet]
        public Person GetPersonByAge(int Age)
        {
            if (Age <= 25)
            {
                return new("ZLim", 25);
            }
            else
            {
                return new("NewJack", 18);
            }
        }

        [HttpPost]
        public string[] PrintNote(SaveNoteRequest req)
        {
            Console.WriteLine($"{req.Title} is {req.Content}");
            return new string[] { "ok", $"{req.Title} is {req.Content}" };
        }

    }
}
