using System;
using System.Text;

namespace CSharpInterviewProgram
{
    class Program
    {
        #region .NET面试解析01-值类型与引用类型
        // https://www.cnblogs.com/anding/p/5229756.html
        /// <summary>
        /// 值类型与引用类型
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args)
        {
            Console.WriteLine("Hello World!");

            #region 参数-按引用传递
            //参数-按引用传递：
            int a1 = 10;
            DoTest(ref a1);
            Console.WriteLine("a1=" + a1); //输出：a=20 ,a的值改变了
            User user1 = new User();
            user1.Age = 10;
            DoUserTest(ref user1);
            Console.WriteLine("user.Age=" + user1.Age); //输出：user.Age=20
            /*
            out 和 ref的主要异同：
            ·out 和 ref都指示编译器传递参数地址，在行为上是相同的；
            ·他们的使用机制稍有不同，ref要求参数在使用之前要显式初始化，out要在方法内部初始化；
            ·out 和 ref不可以重载，就是不能定义Method(ref int a)和Method(out int a)这样的重载，从编译角度看，二者的实质是相同的，只是使用时有区别；
            */
            #endregion

            #region 参数-按值传递
            // 参数-按值传递：
            int a = 10;
            DoTest(a);
            Console.WriteLine("a=" + a); //输出：a=10
            User user = new User();
            user.Age = 10;
            DoUserTest(user);
            Console.WriteLine("user.Age=" + user.Age); //输出：user.Age=20
            /*
            上面的代码示例，两个方法的参数，都是按值传递
            ·对于值类型(int a) ：传递的是变量a的值拷贝副本，因此原本的a值并没有改变。
            ·对于引用类型(User user) ：传递的是变量user的引用地址（User对象实例的内存地址）拷贝副本，因此他们操作都是同一个User对象实例。
            */
            #endregion

        }

        private static void DoTest(int a)
        {
            a *= 2;
        }

        private static void DoUserTest(User user)
        {
            user.Age *= 2;
        }

        private static void DoTest( ref int a)
        {
            a *= 2;
        }

        private static void DoUserTest(ref User user)
        {
            user.Age *= 2;
        }
        #endregion

        #region .NET面试解析02-拆箱与装箱
        // https://www.cnblogs.com/anding/p/5236739.html
        /// <summary>
        /// 拆箱与装箱
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args)
        {
            int i = 5;
            object obj = i;         // 1次装箱
            IFormattable ftt = i;   // 2次装箱
            Console.WriteLine(System.Object.ReferenceEquals(i, obj));   // 3次装箱
            Console.WriteLine(System.Object.ReferenceEquals(i, ftt));   // 4次装箱
            Console.WriteLine(System.Object.ReferenceEquals(ftt, obj));
            Console.WriteLine(System.Object.ReferenceEquals(i, (int)obj));// 5次装箱，1次拆箱 -> 拆完箱又需要将拆完箱后的值类型装箱为引用类型 object：6次装箱
            Console.WriteLine(System.Object.ReferenceEquals(i, (int)ftt));// 7次装箱，2次拆箱 -> 拆完箱又需要将拆完箱后的值类型装箱为引用类型 object：8次装箱
        }
	    #endregion

        #region .NET面试解析03-string与字符串操作
        // https://www.cnblogs.com/anding/p/5240313.html
        /// <summary>
        /// string与字符串操作
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // var s1 = "123";
            // var s2 = s1 + "abc";
            // Console.WriteLine(s2);   //输出：123abc
            // Console.WriteLine(string.IsInterned(s2) ?? "NULL");   //输出：NULL。因为“123abc”没有驻留

            // string.Intern(s2);   //主动驻留字符串
            // Console.WriteLine(string.IsInterned(s2) ?? "NULL");   //输出：123abc

            // StringBuilder sb1 = new StringBuilder();
            // Console.WriteLine("Capacity={0}; Length={1};", sb1.Capacity, sb1.Length); //输出：Capacity=16; Length=0;   //初始容量为16 
            // sb1.Append('a', 12);    //追加12个字符
            // Console.WriteLine("Capacity={0}; Length={1};", sb1.Capacity, sb1.Length); //输出：Capacity=16; Length=12;  
            // sb1.Append('a', 20);    //继续追加20个字符，容量倍增了
            // Console.WriteLine("Capacity={0}; Length={1};", sb1.Capacity, sb1.Length); //输出：Capacity=32; Length=32;  
            // sb1.Append('a', 41);    //追加41个字符，新容量=32+41=73
            // Console.WriteLine("Capacity={0}; Length={1};", sb1.Capacity, sb1.Length); //输出：Capacity=73; Length=73;  

            // StringBuilder sb2 = new StringBuilder(80); //设置一个合适的初始容量
            // Console.WriteLine("Capacity={0}; Length={1};", sb2.Capacity, sb2.Length); //输出：Capacity=80; Length=0;
            // sb2.Append('a', 12);
            // Console.WriteLine("Capacity={0}; Length={1};", sb2.Capacity, sb2.Length); //输出：Capacity=80; Length=12;
            // sb2.Append('a', 20);
            // Console.WriteLine("Capacity={0}; Length={1};", sb2.Capacity, sb2.Length); //输出：Capacity=80; Length=32;
            // sb2.Append('a', 41);
            // Console.WriteLine("Capacity={0}; Length={1};", sb2.Capacity, sb2.Length); //输出：Capacity=80; Length=73;

            string s1 = "123";
            string s2 = s1 + "abc";
            string s3 = "123abc";
            Console.WriteLine(s2 == s3);
            Console.WriteLine(s2.GetHashCode());
            Console.WriteLine(s3.GetHashCode());
            Console.WriteLine(Object.ReferenceEquals(s2, s3));
            s2 = string.Intern(s2);// 手动将 s2 Intern 即手动将 s2 放入字符串常量池中；并且检查了字符串常量池发现 Key("123abc") 已经存在故返回了现有对象的堆内存地址
            Console.WriteLine(Object.ReferenceEquals(s2, s3));
            // 因此此时再判断 s2, s3 结果为 True：即两者指向堆内存字符串常量池的同一个对象

            Console.WriteLine(Reverse1("123456"));
            Console.WriteLine(Reverse2("123456"));
            Console.WriteLine(Reverse3("123456"));

            object a = "123";
            object b = "123";
            Console.WriteLine(System.Object.Equals(a,b));
            Console.WriteLine(System.Object.ReferenceEquals(a,b));
            string sa = "123";
            Console.WriteLine(System.Object.Equals(a, sa));
            Console.WriteLine(System.Object.ReferenceEquals(a, sa));
            
        }
        
        /// <summary>
        /// 使用 StringBuilder
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse1(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("参数不合法！");
            }

            var sb = new StringBuilder(str.Length);
            for (int index = str.Length - 1; index >= 0; index--)
            {
                sb.Append(str[index]);
            }
            return sb.ToString();
        }
        
        /// <summary>
        /// 使用 char 数组与第三个变量
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse2(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("参数不合法！");
            }
            char[] chars = str.ToCharArray();
            var begin = 0;
            var end = chars.Length - 1;
            char tempChar;
            while (begin < end)
            {
                tempChar = chars[begin];
                chars[begin] = chars[end];
                chars[end] = tempChar;
                begin++;
                end--;
            }
            
            var strResult = new string(chars);
            return strResult;
        }

        /// <summary>
        /// 使用 Array.Reverse 静态方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Reverse3(string str)
        {
            var chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        #endregion

        #region .NET面试解析04-类型、方法与继承
        // https://www.cnblogs.com/anding/p/5248973.html
        /// <summary>
        /// 类型、方法与继承
        /// </summary>
        /// <param name="args"></param>
        static void Main4(string[] args)
        {
            Console.WriteLine("Hello,World!");
        }
        #endregion

    }

    class User
    {
        public int Age { get; set; }
    }
}
