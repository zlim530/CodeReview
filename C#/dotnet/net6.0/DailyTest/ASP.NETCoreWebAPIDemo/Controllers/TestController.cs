using ASP.NETCoreWebAPIDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPIDemo.Controllers
{
    //[Route("api/[controller]")]
    // route 路径规则不仅仅使用 HTTP 谓词匹配，还加上方法名来匹配
    // 强制要求控制器中不同的操作用不同的方法名
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

        /// <summary>
        /// 如果控制器中存在一个没有添加 [HttpGet]/[HttpPost] 等特性的 public 方法，swagger 启动会报错，可以使用
        /// [ApiExplorerSettings(IgnoreApi = true)] 特性来解决
        /// 注意：是 swagger 启动会报错：Failed to load API definition.，但是程序本身是没有问题的：可以通过 URL 地址正常访问
        /// </summary>
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public void Test()
        //{
        //    Console.WriteLine("Hello");
        //}

        [HttpPost]
        public string[] PrintNote(SaveNoteRequest req)
        {
            Console.WriteLine($"{req.Title} is {req.Content}");
            return new string[] { "ok", $"{req.Title} is {req.Content}" };
        }

        [HttpPut("{id}")]
        public string UpdateUser([FromRoute]int id, User user)
        {
            return "Update " + id + " done " + user.Name;
        }

        /// <summary>
        /// 使用 QueryString 的方法来获得参数
        /// 如果不写则默认使用 QueryString 来获得参数的值；如果名字一致，只要为参数添加[FromQuery]即可；而如果名字不一致，[FromQuery(Name = 名字)]
        /// 此方法请求的 URL：https://xxxx:xxxx/api/Test/Add?i=1&j=2
        /// </summary>
        [HttpGet]
        public int Add(int i, int j)
        {
            return i + j;
        }

        [HttpGet]
        public User GetUsers([FromQuery(Name = "name")]string userName, int id)
        {
            return new User(id + 5, userName + "wa");
        }

        /// <summary>
        /// 在[HttpGet]、[HttpPost]等中使用占位符，比如{schoolName}，捕捉路径中的内容，从而供Action方法的参数使用。
        /// 捕捉的值会被自动赋值给Action中同名的参数；如果名字不一致，可以用[FromRoute(Name="名字")]
        /// 此方法请求的 URL：https://xxxx:xxxx/api/Test/Muilt/1/2
        /// </summary>
        [HttpGet("{i}/{j}")]
        public int Muilt(int i, int j)
        {
            return i * j;
        }

        [HttpGet("students/school/{shcoolName}/class/{classNo}")]
        public Person GetStudents(string shcoolName, [FromRoute(Name ="classNo")] int classNum)
        { 
            return new Person(shcoolName + "的扛把子", classNum + 6);
        }


        /// <summary>
        /// Web API中Action方法的返回值如果是普通数据类型，那么返回值就会默认被序列化为Json格式
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public User GetUser() 
        {
            return new User(1,"NewJack");
        }

        /// <summary>
        /// 通过 Id 获取用户
        /// 如果明确的知道返回值的类型，可以使用 ActionResult<T> 泛型类型
        /// 不知道则可以使用 IActionResult 类型
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<User> GetUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id 必须是正数");
            }
            else if (id == 1)
            {
                return new User(1,"NumberOne");
            }
            else
            {
                return NotFound("用户不存在！");
            }
        }

    }
}
