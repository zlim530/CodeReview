using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DailyLocalCode;

namespace CSharpInDepthChapter8After
{
    public class CSharpInDepthChapter8After
    {
        /// <summary>
        /// 对象初始化程序（object initializers）
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args)
        {
            Person tom1 = new Person();
            tom1.Age = 9;
            tom1.Name = "Tom";

            Person tom2 = new Person("Tom");
            tom2.Age = 9;

            // C# 3 之后：在每一行末尾，用大括号括的就是对象初始化程序。
            // 对象初始化程序（object initializers）
            Person tom3 = new Person() {Name = "Tom", Age = 9};
            // 注意在声明 tom4 时，我们省略了构造函数的圆括号。如果类型有一个无参的构造函数，就可以使用这种简写方式，这正是在编译完的代码中调用的。
            Person tom4 = new Person {Name = "Tom", Age = 9};
            Person tom5 = new Person("Tom") {Age = 9};

            // Person tom = new Person();
            // tom.Age = 9;
            // tom.Home.Country = "UK";
            // tom.Home.Town = "Reading";

            Person tom = new Person("Tom")
            {
                Age = 9,
                Home = {Country = "UK", Town = "Reading"}
            };
        }

        /// <summary>
        /// 集合初始化程序（collection initializer）与匿名对象初始化
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args)
        {
            // C# 2:要么是传入一个现有的集合，要么是先创建空列表，再重复调用 Add
            List<string> names = new List<string>();
            names.Add("Holly");
            names.Add("Jon");
            names.Add("Tom");
            names.Add("Robin");
            names.Add("William");
            // C# 3:集合初始化程序（collection initializer），利用它你使用和数组初始化程序一样的语法，但支持任意集合，而且更灵活
            var names2 = new List<string>
            {
                "Holly","Jon","Tom",
                "Robin","William"
            };

            /*
             * 集合初始化列表并非只能应用于列表。任何实现了 IEnumerable 的类型，只要它为初始化
                列表中出现的每个元素都提供了一个恰当的公有的 Add 方法，就可以使用这个特性。Add方法可
                以接受多个参数，只需把值放到另一对大括号中。最常见的应用就是创建字典，例如，假定你要
                创建一个将名字映射到年龄的字典，可以使用以下代码：
             */
            Dictionary<string, int> nameAgeMap = new Dictionary<string, int>
            {
                {"Holly",36},
                {"Jon",36},
                {"Tom",9}
            };

            Person tom = new Person // 调用无参构造函数
            {
                Name = "Tom",   // 直接设置属性
                Age = 9,
                Home = { Town = "Reading", Country = "UK"},                         // 初始化嵌入对象
                Friends =
                {
                    new Person{Name = "Alberto"},
                    // 用更进一步的对象初始化程序来初始化集合
                    new Person("Max"),
                    new Person{Name = "Zak",Age = 7},
                    new Person("Ben"),
                    new Person("Alice")
                    {
                        Age = 9,
                        Home = { Town = "Twyford",Country = "UK"}
                    },
                }
            };
            
            /*
             * 在任何一个给定的程序集中，如果两个
            匿名对象初始化程序包含相同数量的属性，且这些属性具有相同的名称和类型，而且以相同的顺
            序出现，就认为它们是同一个类型。换言之，如果在某个初始化程序中交换了 Name 和 Age 的顺序，
            就会出现两个不同的类型。
             */
            var family = new[]
            {
                new {Name = "Holly",Age = 36},
                new {Name = "Jon",Age = 36},
                new {Name = "Tom",Age = 9},
                new {Name = "Robin",Age = 6},
                new {Name = "William",Age = 6},
            };
            int totalAge = 0;
            foreach (var person in family)
            {
                totalAge += person.Age;
            }
            Console.WriteLine($"Total age:{totalAge}");

            List<Person> families = new List<Person>
            {
                new Person{Name = "Holly", Age = 36},
                new Person{Name = "Jon", Age = 36},
                new Person{Name = "Tom", Age = 9},
                new Person{Name = "Robin", Age = 6},
                new Person{Name = "William", Age = 6},
            };
            var converted = families.ConvertAll(delegate(Person person)
            {
                return new {person.Name, IsAdult = (person.Age >= 18)};
            });
            foreach (var person in converted) 
            {
                Console.WriteLine($"{person.Name} is an adult? {person.IsAdult}");
            }
        }

        /// <summary>
        /// LINQ to Objects
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args)
        {
            /*
             * LINQ to Objects处理的是同一个进程中的数据序列。
             * 相比之下，像LINQ to SQL这样的provider将工作交给“进程外”的系统（比如数据库）去处理。
             */
            var films = new List<Film>
            {
                new Film{Name = "Jaw",Year = 1975},
                new Film{Name = "Singing in the Rain",Year = 1952},
                new Film{Name = "Some like it Hot",Year = 1959},
                new Film{Name = "The Wizard of Oz",Year = 1939},
                new Film{Name = "It's a Wonderful Life",Year = 1946},
                new Film{Name = "American Beauty",Year = 1999},
                new Film{Name = "High Fidelity",Year = 2000},
                new Film{Name = "The Usual Suspects",Year = 1995}
            };
            Action<Film> print = film => Console.WriteLine($"Name={film.Name}, Year={film.Year}");
            films.ForEach(print);
            
            Console.WriteLine("===年份小于1960年的电影：");
            // 创建过滤后的列表
            films.FindAll(film => film.Year < 1960).ForEach(print);
            Console.WriteLine("===按照电影名称进行排序：");
            // 对原始列表排序：
            films.Sort((f1,f2)=> f1.Name.CompareTo(f2.Name));
            films.ForEach(print);
        }

        /// <summary>
        /// 表达式树与 Lambda 表达式
        /// </summary>
        /// <param name="args"></param>
        static void Main3(string[] args)
        {
            Expression firstArg = Expression.Constant(2);
            Expression secondArg = Expression.Constant(3);
            Expression add = Expression.Add(firstArg,secondArg);
            Console.WriteLine(add);// (2 + 3)

            Func<int> compiled = Expression.Lambda<Func<int>>(add).Compile();
            Console.WriteLine(compiled());// 5

            /*
             * 我们知道，Lambda表达式能显式或隐式地转换成恰当的委托实例。然而，这并非唯一能进行的转换。
             * 还可以要求编译器通过你的Lambda表达式构建一个表达式树，在执行时创建Expression<TDelegate> 的一个实例。
             * 例如，以下代码展示了用一种精简得多的方式创建“返回5”的表达式，然后编译这个表达式，并调用编译得到的委托。
             */
            Expression<Func<int>> return5 = () => 5;
            /*public TDelegate Compile() 将表达式树描述的 lambda 表达式编译为可执行代码，并生成 lambda 表达式的委托。
             Returns：一个 TDelegate 类型的委托，它表示由 Expression<TDelegate> 描述的已编译的 lambda 表达式。*/
            Func<int> compiled2 = return5.Compile();
            Console.WriteLine(compiled2());// 5
            /*注意：
             并非所有Lambda表达式都能转换成表达式树。
             不能将带有一个语句块（即使只有一个 return 语句）的Lambda转换成表达式树
             只有对单个表达式进行求值的Lambda才可以。
             表达式中还不能包含赋值操作，因为在表达式树中表示不了这种操作。
             */

            /*public bool StartsWith(string value):确定此字符串实例的开头是否与指定的字符串匹配，value是要比较的字符串；
             Returns：如果 true 与字符串的开头匹配，否则为 false*/
            Expression<Func<string, string, bool>> expression = (x, y) => x.StartsWith(y);
            var compiled3 = expression.Compile();
            Console.WriteLine(compiled3("First","Second"));// False
            Console.WriteLine(compiled3("First","Fir"));// True
            
            // 代码清单9-10 用代码来构造一个方法调用表达式树
            // 1构造方法调用的各个部件
            // 方法本身（ MethodInfo ）
            MethodInfo method = typeof(string).GetMethod("StartsWith",new []{typeof(string)});
            // 方法的目标（也就是调用 StartsWith 的字符串）
            var target = Expression.Parameter(typeof(string), "x");
            // 参数列表（本例只有一个参数）
            var methodArg = Expression.Parameter(typeof(string), "y");
            Expression[] methodArgs = new[] {methodArg};
            // 2从以上部件创建 CallExpression
            Expression call = Expression.Call(target,method,methodArgs);
            // 3将 Call 转换成 Lambda 表达式
            var lambdaParameters = new[] { target,methodArg};
            var lambda = Expression.Lambda<Func<string, string, bool>>(call, lambdaParameters);
            /*
             * 将方法调用构造成一个表达式之后 ，接着需要把它转换成Lambda表达式 ，并绑定参数。
             * 我们重用了作为方法调用（部件）信息而创建的参数表达式的值（ ParameterExpression ）
                ：创建Lambda表达式时指定的参数顺序就是最终调用委托时使用的参数顺序。
             */
            var compiled4 = lambda.Compile();
            Console.WriteLine(compiled4("First","Second"));
            Console.WriteLine(compiled4("First","Fir"));
            
        }

        /// <summary>
        /// 扩展方法的语法
        /// </summary>
        /// <param name="args"></param>
        static void Main4(string[] args)
        {
            #region 使用工具类中的普通静态方法复制一个流
            // 用 StreamUtil 将 Web 响应流复制到一个文件
            //WebRequest request = WebRequest.Create("http://manning.com");
            //using (WebResponse response = request.GetResponse())
            //using (Stream responseStream = response.GetResponseStream())
            //using (FileStream output = File.Create("response.dat"))
            //{
            //    StreamUtil.Copy(responseStream, output);
            //}
            #endregion

            #region 使用 Stream 类的扩展方法复制一个流
            //WebRequest request2 = WebRequest.Create("http://manning.com");
            //using (WebResponse response2 = request2.GetResponse())
            //using (Stream responseStream2 = response2.GetResponseStream())
            //using (FileStream output = File.Create("response2.dat"))
            //{
            //    /*
            //    事实上，编译器已将 MyCopyTo 调用转换成对普通静态方法 StreamUtil.MyCopyTo 的调用。
            //    调用时，会将 responseStream 的值作为第一个实参的值传递（然后是 output ，跟平常一样）。 
            //    */
            //    responseStream2.MyCopyTo(output);
            //}
            #endregion

            #region 在空引用上调用扩展方法
            object y = null;
            /*
            如果 IsNull 是一个普通的实例方法， 这一行就会抛出一个异常。但是，这里的 null 是 IsNull 的实参。
            在扩展方法问世前，y.Isnull()这样的写法虽然可读性更好，却不合法，只能采用 NullUtil.IsNull(y) 这样的写法。 
            */
            Console.WriteLine(y.IsNull());// True
            y = new object();
            Console.WriteLine(y.IsNull());// False
            #endregion
        }

        /// <summary>
        /// .NET 3.5 中的扩展方法
        /// </summary>
        /// <param name="args"></param>
        /*
        在框架中，扩展方法最大的用途就是为LINQ服务。有的LINQ提供器包含了几个供辅助的扩展方法，但有两个类特别醒目， 
        Enumerable 和 Queryable ，两者都在 System.Linq 命名空间中。
        在这两个类中，含有许许多多的扩展方法： Enumerable 的大多数扩展的是 IEnumerable<T> ，
        Queryable 的大多数扩展的是 IQueryable<T> 。 
        IQueryable<T> 的作用将在第12章讲述，目前让我们将重点放在 Enumerable 上。
        */
        static void Main5(string[] args)
        {
            // 用 Enumerable.Range 打印数字0~9
            var collection = Enumerable.Range(0,10);
            foreach (var element in collection)
            {
                Console.WriteLine(element);
            }
        
            Console.WriteLine("--------------ReverseCollection----------------");
            // 用 Reverse 方法来反转一个集合
            var reverseCollection = Enumerable.Range(0, 10).Reverse();
            foreach (var element in reverseCollection)
            {
                Console.WriteLine(element);
            }
            
            Console.WriteLine("--------------OddCollection----------------");
            // 用 Lambda 表达式作为 Where 方法的参数，从而只保留奇数            
            var oddCollection = Enumerable.Range(0, 10)
                                                            .Where(x => x % 2 != 0)
                                                            .Reverse();
            foreach (var element in oddCollection)
            {
                Console.WriteLine(element);
            }
            
            Console.WriteLine("--------------SquareRootCollection----------------");
            // 用 Lambda 表达式和匿名类型进行投影：select
            var squareRootCollection = Enumerable.Range(0, 10)
                                                                                    .Where(x => x % 2 != 0)
                                                                                    .Reverse()
                                                                                    .Select(x => new {Original = x, SquareRoot = Math.Sqrt(x)});
            foreach (var element in squareRootCollection)
            {
                Console.WriteLine($"sqrt({element.Original}) = {element.SquareRoot}");
            }
            
            Console.WriteLine("--------------OrderByCollection----------------");
            // 根据两个属性对序列进行排序
            var orderedCollection = Enumerable.Range(-5, 11)
                .Select(x => new {Original = x, Square = x * x})
                .OrderBy(x => x.Square)
                .ThenBy(x => x.Original);
            foreach (var element in orderedCollection)
            {
                Console.WriteLine(element);
            }
            //“主”排序属性是 Square ，但对于平方值相同的两个值，负的原始值总是排在正的原始值前面
            /*
             需要注意的是，排序不会改变原有集合——它返回的是新的序列，所产生的数据与输入序列相同，当然除了顺序。
            这与 List<T>.Sort 和 Array.Sort 不同，它们会改变列表或数组中元素的顺序。LINQ操作符是无副作用的：
            它们不会影响输入，也不会改变环境。除非你迭代的是一个自然状态序列（如从网络流中读取数据）或使用含有副
            作用的委托参数。这是函数式编程的方法，可以使代码更加可读、可测、可组合、可预测、健壮并且线程安全。
             */

        }

        #region 立即求值、惰性求值与延迟执行
        /*
         框架提供的扩展方法会尽量尝试对数据进行“流式”（stream）或者说“管道”（pipe）传输。
     要求一个迭代器提供下一个元素时，它通常会从它链接的迭代器获取一个元素，处理那个元素，
     再返回符合要求的结果，而不用占用自己更多的存储空间。执行简单的转换和过滤操作时，
     这样做非常简单，可用的数据处理起来也非常高效。但是，对于某些操作来说，比如反转或排序，
     就要求所有数据都处于可用状态，所以需要加载所有数据到内存来执行批处理。缓冲和管道传输方式，
     这两者的差别很像是加载整个DataSet读取数据和用一个DataReader来每次处理一条记录的差别。
     使用LINQ时务必想好真正需要的是什么，一个简单的方法调用可能会严重影响性能。
        流式传输（streaming）也叫惰性求值（lazy evaluation），缓冲传输（bufferring）也叫热情
     求值（eager evaluation）。例如， Reverse 方法使用了延迟执行（deferred execution），它在第一次调用
     MoveNext 之前不做任何事情。但随后却热切地（eagerly）对数据源求值。
         */
        //Considering a function
        int Computation(int index)
        {
            return index;
        }
        
        //1.Immediate execution
        IEnumerable<int> GetComputation(int maxIndex)
        {
            var result = new int[maxIndex];
            for (int i = 0; i < maxIndex; i++)
            {
                result[i] = Computation(i);
            }
            return result;
        }
        /*
         ·When the function is called is executed times Computation maxIndex
         ·GetEnumerator returns a new instance of the enumerator doing nothing more.
         ·Each call to put the the value stored in the next Array cell in the member of the and 
            that's all.MoveNext Current IEnumerator
         Cost: Big upfront, Small during enumeration (only a copy)
         */
        
        //2.Deferred but eager execution
        IEnumerable<int> GetComputation2(int maxIndex)
        {
            var result = new int[maxIndex];
            for (int i = 0; i < maxIndex; i++)
            {
                result[i] = Computation(maxIndex);
            }

            foreach (var value in result)
            {
                yield return value;
            }
        }
        /*
         ·When the function is called an instance of an auto generated class(called "enumerable object" in the spec)
            implementing is created and a copy of the argument () is stored in it.IEnumerable maxIndex
         ·GetEnumerator returns a new instance of the enumerator doing nothing more.
         ·The first call to executes maxIndex times the compute method, store the result in an array
            and will return the first value.MoveNext Current
         ·Each subsequent call to will put in a value stored in the array.MoveNext Current
         Cost: nothing upfront, Big when the enumeration start, Small during enumeration (only a copy)
         */
        
        //3.Deferred and lazy execution
        IEnumerable<int> GetComputation3(int maxIndex)
        {
            for (int i = 0; i < maxIndex; i++)
            {
                yield return Computation(i);
            }
        }
        /*
         ·When the function is called the same thing as the lazy execution case happens.
         ·GetEnumerator returns a new instance of the enumerator doing nothing more.
         ·Each call to execute once to code, put the value in and let the caller immediately 
            act on the result.MoveNext Computation Current
        Most of linq use deferred and lazy execution but some functions can't be so like sorting.
        Cost: nothing upfront, Moderate during enumeration (the computation is executed there)
         */
        /*
         To summarize
            ·Immediate mean that the computation/execution is done in the function and finished 
                once the function return. (Fully eager evaluation as most C# code does)
            ·Deferred/Eager mean that most of the work will be done on the first or when the 
                instance is created (For it is when is called)MoveNext IEnumerator IEnumerable GetEnumerator
            ·Deferred/Lazy mean that the work will be done each time is called but nothing before.MoveNext
         */
        #endregion

        /// <summary>
        /// LINQ 简单的开始：选择元素
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello,World!");

            var query = from user in SampleData.AllUsers
                                            select user;
            foreach (var user in query)
            {
                Console.WriteLine(user);
            }
        }
        
    }

    public static class Test
    {
        public static bool IsNull(this object x)
        {
            return x == null;
        }
    }

    /*
    并不是任何方法都能作为扩展方法使用——它必须具有以下特征：
    ·它必须在一个非嵌套的、非泛型的静态类中（所以必须是一个静态方法）；
    ·它至少要有一个参数；
    ·第一个参数必须附加 this 关键字作为前缀；
    ·第一个参数不能有其他任何修饰符（比如 out 或 ref ）；
    ·第一个参数的类型不能是指针类型。
    */
    public static class StreamUtil
    {
        const int BufferSize = 8192;

        /// <summary>
        /// 普通的静态方法
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void Copy(Stream input, Stream output)
        {
            byte[] buffer = new byte[BufferSize];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        /// <summary>
        /// 将静态类中的“普通”静态方法转换成扩展方法具体需要做的事情——只需添加 this 关键字
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void MyCopyTo(this Stream input, Stream output)
        {
            byte[] buffer = new byte[BufferSize];
            int read;
            while ((read = input.Read(buffer,0,buffer.Length)) > 0)
            {
                output.Write(buffer,0,read);
            }
        }

        public static byte[] ReadFully(this Stream input)
        {
            using (MemoryStream tempStream = new MemoryStream())
            {
                //Copy(input, tempStream);
                input.MyCopyTo(tempStream);
                return tempStream.ToArray();
            }
        }
    }

    class Film
    {
        public string Name { get; set; }
        public int Year { get; set; }
    }

    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }

        private List<Person> friends = new List<Person>();
        public List<Person> Friends
        {
            get { return friends; }
        }

        private Location home = new Location();
        public Location Home
        {
            get { return home; }
        }

        public Person()
        {
            
        }

        public Person(string name)
        {
            Name = name;
        }
    }

    public class Location
    {
        public string Country { get; set; }
        public string Town { get; set; }
    }
}