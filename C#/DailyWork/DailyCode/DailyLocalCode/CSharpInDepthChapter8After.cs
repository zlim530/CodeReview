using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

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
        /// 扩展方法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello,World!");
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