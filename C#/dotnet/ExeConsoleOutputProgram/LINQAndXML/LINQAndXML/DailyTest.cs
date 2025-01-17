﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LINQAndXML
{
    public class DailyTest
    {
        static void Main0(string[] args)
        {
            string str = "a -b+c";
            string[] s = str.Split(new char[] { '-', '+' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(s[0]);// a
            Console.WriteLine(s[1]);// b
            Console.WriteLine(s[2]);// c
            Console.WriteLine(str[1]);
            Console.WriteLine(str[2]);// -

        }

        static void Main1(String[] args)
        {
            List<string> list = new List<string>();
            //int y = 10;
            //int x = y == 0 ? 1 : y;
            Func<int, int> del = x => x + 1;
            Expression<Func<int, int>> exp = x => x + 1;// 表达式目录树 exp 引用描述表达式 x => x + 1 的数据结构
            var del2 = exp.Compile();
            Console.WriteLine(del(1));
            Console.WriteLine(del2(1));
        }

        static void Main2(string[] args)
        {
            string[] fruits = { "apricot", "organe" , "banana" , "mango" , "apple" , "grape" , "strawberry" };
            IOrderedEnumerable<string> sortedFruits1 = fruits.OrderByDescending(fruit => fruit.Length).ThenBy(fruit => fruit);// 按照字符串本身的排序原则：如果长度一致则根据(首字母的)ASCII值进行排序
            foreach (var fruit in sortedFruits1)
            {
                Console.WriteLine(fruit);
            }
            /*
            apple
            grape
            mango
            banana
            organe
            apricot
            strawberry

            strawberry
            apricot
            banana
            organe
            apple
            grape
            mango
             */
        }

        static void Main3(string[] args)
        {
            // for (int i = 0; i < 5; i++)
            // {
            //     System.Console.WriteLine(i);
            // }

            int x = 0;
            int y = 1;
            int z = 0;
            for (int i = 0; i < 2; i++)
            {
                z = x + y;
                x = y;
                y = z;
            }
            System.Console.WriteLine(x);
            System.Console.WriteLine(y);
            System.Console.WriteLine(z);

            System.Console.WriteLine(Fibo(8));

        }


        static int Fibo(int n)
        {
            if (n<=1)
            {
                return n;
            }
            int first = 0;
            int second = 1;
            int sum = 0;

            for (int i = 0; i < n; i++)
            {
                sum = first + second;
                first = second;
                second = sum;
            }
            return first;
        }
        
        static void Main(string[] args)
        {
            // Create a list of strings.
            var salmons = new List<string>();
            salmons.Add("chinook");
            salmons.Add("coho");
            salmons.Add("pink");
            salmons.Add("sockeye");

            // Iterate through the list.
            foreach (var salmon in salmons)
            {
                Console.Write(salmon + " ");
            }
            // Output: chinook coho pink sockeye
            
            System.Console.WriteLine();
            // System.Console.WriteLine(System.Diagnostics.StopWatch);
            System.Console.WriteLine(Environment.TickCount);    // 2937562
            System.Console.WriteLine(DateTime.Now.Ticks);       // 637437047833694566
            System.Console.WriteLine(DateTime.UtcNow.Ticks);    // 637436759833789768
            System.Console.WriteLine(DateTime.Now);             // 2020/12/16 8:39:17 - 带时区：东八区
            System.Console.WriteLine(DateTime.UtcNow);          // 2020/12/16 0:39:17 - 不带时区

        }

        static void Main5(string[] args)
        {
            #region C#特殊字符之@：逐字字符串标识符字符

            string[] @for = { "John", "James", "Joan", "Jamie" };
            for (int ctr = 0; ctr < @for.Length; ctr++)
            {
            Console.WriteLine($"Here is your gift, {@for[ctr]}!");
            }

            string filename1 = @"c:\documents\files\u0066.txt";
            string filename2 = "c:\\documents\\files\\u0066.txt";

            Console.WriteLine(filename1);
            Console.WriteLine(filename2);

            string s1 = "He said, \"This is the last \u0063hance\x0021\"";
            string s2 = @"He said, ""This is the last \u0063hance\x0021""";

            Console.WriteLine(s1);
            Console.WriteLine(s2);

            #endregion


            #region C#特殊字符之$：内插的字符串字符
            // 从 C# 8.0 开始，可以按任意顺序使用 $ 和 @ 标记：$@"..." 和 @$"..." 均为有效的内插逐字字符串。 在早期 C# 版本中，$ 标记必须出现在 @ 标记之前。
            string name = "Mark";
            var date = DateTime.Now;

            // Composite formatting:
            Console.WriteLine("Hello, {0}! Today is {1}, it's {2:HH:mm} now.", name, date.DayOfWeek, date);
            // String interpolation:
            Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
            // Both calls produce the same output that is similar to:
            // Hello, Mark! Today is Wednesday, it's 19:40 now.
            
            Console.WriteLine($"|{"Left",-7}|{"Right",7}|");

            const int FieldWidthRightAligned = 20;
            Console.WriteLine($"{Math.PI,FieldWidthRightAligned} - default formatting of the pi number");
            // F3 表示小数点后面保留3位小数
            Console.WriteLine($"{Math.PI,FieldWidthRightAligned:F3} - display only three decimal digits of the pi number");
            // Expected output is:
            // |Left   |  Right|
            //     3.14159265358979 - default formatting of the pi number
            //                3.142 - display only three decimal digits of the pi number

            /* 
            特殊字符
            要在内插字符串生成的文本中包含大括号 "{" 或 "}"，请使用两个大括号，即 "{{" 或 "}}"。 有关详细信息，请参阅转义大括号。
            因为冒号（“:”）在内插表达式项中具有特殊含义，为了在内插表达式中使用条件运算符，请将表达式放在括号内。
            以下示例演示如何将大括号含入结果字符串中，以及如何在内插表达式中使用条件运算符：
             */
            name = "Horace";
            int age = 34;
            Console.WriteLine($"He asked, \"Is your name {name}?\", but didn't wait for a reply :-{{");
            Console.WriteLine($"{name} is {age} year{(age == 1 ? "" : "s")} old.");
            // Expected output is:
            // He asked, "Is your name Horace?", but didn't wait for a reply :-{
            // Horace is 34 years old.

            double speedOfLight = 299792.458;
            FormattableString message = $"The speed of light is {speedOfLight:N3} km/s.";

            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("nl-NL");
            string messageInCurrentCulture = message.ToString();

            var specificCulture = System.Globalization.CultureInfo.GetCultureInfo("en-IN");
            string messageInSpecificCulture = message.ToString(specificCulture);

            string messageInInvariantCulture = FormattableString.Invariant(message);

            // -10 表示占位符
            Console.WriteLine($"{System.Globalization.CultureInfo.CurrentCulture,-10} {messageInCurrentCulture}");
            Console.WriteLine($"{specificCulture,-10} {messageInSpecificCulture}");
            Console.WriteLine($"{"Invariant",-10} {messageInInvariantCulture}");
            // Expected output is:
            // nl-NL      The speed of light is 299.792,458 km/s.
            // en-IN      The speed of light is 2,99,792.458 km/s.
            // Invariant  The speed of light is 299,792.458 km/s.

            #endregion

        }

    }
}


#region 后端项目搭建
// namespace CoreBackend.Api.Controllers
// {
//     [Route("api/[controller]")]
//     public class ProductController : Controllers
//     {
//         /*
//          使用[Route("api/[controller]")], 它使得整个Controller下面所有action的uri前缀变成了"/api/product", 其中[controller]表示XxxController.cs中的Xxx(其实是小写).

//         也可以具体指定, [Route("api/product")], 这样做的好处是, 如果ProductController重构以后改名了, 只要不改Route里面的内容, 那么请求的地址不会发生变化.

//         然后在GetProducts方法上面, 写上HttpGet, 也可以写HttpGet(). 它里面还可以加参数,例如: HttpGet("all"), 那么这个Action的请求的地址就变成了 "/api/product/All".
//         */
//         [HttpGet]
//         public JsonResult GetProducts()
//         {
//             return new JsonResult(new List<Product>
//             {
//                 new Product
//                 {
//                     Id = 1,
//                     Name = "Mike",
//                     Price = 2.5f
//                 },
//                 new Product
//                 {
//                     Id = 2,
//                     Name = "Brand",
//                     Price = 4.5f
//                 }
//             }); ;
//         }

//         [Route("{id}",Name = "GetProduct")]
//         public IActionResult GetProduct(int id)
//         {
//             var product = ProductService ... (x => x.Id == id);
//             if (product == null)
//             {
//                 return NotFound();
//             }
//             return OK(product);
//         }

//         /* 
//         [HttpPost] 表示请求的谓词是Post. 加上Controller的Route前缀, 那么访问这个Action的地址就应该是: 'api/product'

//         后边也可以跟着自定义的路由地址, 例如 [HttpPost("create")], 那么这个Action的路由地址就应该是: 'api/product/create'.

//         [FromBody] , 请求的body里面包含着方法需要的实体数据, 方法需要把这个数据Deserialize成ProductCreation, [FromBody]就是干这些活的.

//         客户端程序可能会发起一个Bad的Request, 导致数据不能被Deserialize, 这时候参数product就会变成null. 所以这是一个客户端发生的错误, 程序为让客户端知道是它引起了错误, 就应该返回一个Bad Request 400 (Bad Request表示客户端引起的错误)的 Status Code.

//         传递进来的model类型是 ProductCreation, 而我们最终操作的类型是Product, 所以需要进行一个Map操作, 目前还是挨个属性写代码进行Map吧, 以后会改成Automapper.

//         返回 CreatedAtRoute: 对于POST, 建议的返回Status Code 是 201 (Created), 可以使用CreatedAtRoute这个内置的Helper Method. 它可以返回一个带有地址Header的Response, 这个Location Header将会包含一个URI, 通过这个URI可以找到我们新创建的实体数据. 这里就是指之前写的GetProduct(int id)这个方法. 但是这个Action必须有一个路由的名字才可以引用它, 所以在GetProduct方法上的Route这个attribute里面加上Name="GetProduct", 然后在CreatedAtRoute方法第一个参数写上这个名字就可以了, 尽管进行了引用, 但是Post方法走完的时候并不会调用GetProduct方法. CreatedAtRoute第二个参数就是对应着GetProduct的参数列表, 使用匿名类即可, 最后一个参数是我们刚刚创建的数据实体. 

//         运行程序试验一下, 注意需要在Headers里面设置Content-Type: application/json.
//          */
//         public IActionResult Post([FormBody] ProductCreation product)
//         {
//             if (product == null)
//             {
//                 return BadRequest();
//             }

//             var maxId = ProductService.Max(x => x.Id);
//             var newProduct = new Product{
//                 Id = ++maxId;
//                 Name = product.Name,
//                 Price = product.Price
//             };
//             ProductService.Add(newProduct);
//             return CreatedAtRoute("GetProduct",new {id = newProduct.Id},newProduct);
//         }

//         /* 
//         put应该用于对model进行完整的更新. 

//         首先最好还是单独为Put写一个Dto Model, 尽管属性可能都是一样的, 但是也建议这样写, 实在不想写也可以.

//         ProducModification.cs
        
//         按照Http Put的约定, 需要一个id这样的参数, 用于查找现有的model.

//         由于Put做的是完整的更新, 所以把ProducModification整个Model作为参数.

//         进来之后, 进行了一套和POST一模一样的验证, 这地方肯定可以改进, 如果验证逻辑比较复杂的话, 到处写同样验证逻辑肯定是不好的, 所以建议使用FluentValidation.

//         然后, 把ProductModification的属性都映射查询找到给Product, 这个以后用AutoMapper来映射.

//         返回: PUT建议返回NoContent(), 因为更新是客户端发起的, 客户端已经有了最新的值, 无须服务器再给它传递一次, 当然了, 如果有些值是在后台更新的, 那么也可以使用Ok(xxx)然后把更新后的model作为参数一起传到前台.
//          */
//         public IActionResult Put(int id,[FormBody] ProductModification product){
//             if (product == null)
//             {
//                 return BadRequest();
//             }

//             if (product.Name == "产品")
//             {
//                 ModelState.AddModelError("Name","产品的名称不可以是‘产品’二字");
//             }

//             if (!ModelState.IsValid)
//             {
//                 return BadRequet(ModelState);
//             }

//             var model = ProductService.SingleOrDefault(x => x.Id == id);
//             if (model == null)
//             {
//                 return NotFound();
//             }

//             model.Name = product.Name;
//             model.Price = product.Price;

//             // return Ok(model);
//             return NoContent();
//         }


//     }

// }



// namespace CoreBackend.Api.Dtos
// {
//     public class ProductCreation
//     {
//         /* 
//         这些Data Annotation (理解为用于验证的注解), 可以在System.ComponentModel.DataAnnotation找到, 例如[Required]表示必填, [MinLength]表示最小长度, [StringLength]可以同时验证最小和最大长度, [Range]表示数值的范围等等很多.

//         [Display(Name="xxx")]的用处是, 给属性起一个比较友好的名字.

//         其他的验证注解都有一个属性叫做 ErrorMessage (string), 表示如果验证失败, 就会把ErrorMessage的内容添加到错误结果里面去. 这个ErrorMessage可以使用参数, {0}表示Display的Name属性, {1}表示当前注解的第一个变量, {2}表示当前注解的第二个变量.
//          */
//         [Display(Name = "产品名称")]
//         [Required(ErrorMessage = "{0}是必填项")]
//         // [MinLength(2,ErrorMessage = "{0}的最小长度是{1}")]
//         // [MaxLength(10,ErrorMessage = "{0}的长度不可以超过{1}")]
//         [StringLength(10,MinimumLength = 2,ErrorMessage = "{0}的长度应该不小于{2},不大于{1}")]
//         public string Name {get; set;}

//         [Display(Name = "价格")]
//         [Range(0,Double.MaxValue,ErrorMessage = "{0}的值必须大于{1}")]
//         public float Price { get;set;}
//     }
// }
#endregion