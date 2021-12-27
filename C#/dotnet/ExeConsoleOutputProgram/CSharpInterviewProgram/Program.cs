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
        static void Main3(string[] args)
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
        static void Main(string[] args)
        {
            var a = new A();
            a.Print();  // 输出 A
            var b1 = new B1();
            b1.Print(); // 输出 B1 
            var b2 = new B2();
            b2.Print(); // 输出 B2
            Console.WriteLine();

            A ab1 = new B1();
            ab1.Print(); // 输出 B1 
            // 只要子类对父类中的虚方法进行了重写，那么不管引用类型变量是父类还是子类，只要实例为子类的实例，那么我们调用 Print 方法永远都只会是跟子类实例类型相关的那个版本而且是最新的那个版本（如果存在祖父、父类、孙子类的情况，那么祖父变量引用孙子类的实例对象，那么也只会调用孙子类的 Print 方法）
            A ab2 = new B2(); // 输出 A
            // 由于子类 B2 中并没有对父类 A 中的虚方法进行 override 重写而是选择的 new 也即隐藏，这会导致 B2 类中有两个 Print 方法，一个是从 A 继承的 base.Print()，一个是自己申明的 this.Print()
            // 可以理解Wie ab2 作为 A 类型的变量引用了 B2 的实例对象，当调用 Print 方法时本应该顺着继承链往下（一直到 B2）找到 Print 方法的具体实现，但是由于 B2 没有 override，所以它找不下去，只能调用 A 类里面的 Print 方法
            ab2.Print();
            Console.WriteLine();

            A abb = new BB1();
            abb.Print(); // 输出 BB1
            B1 bbb1 = new BB1();
            bbb1.Print(); // 输出 BB1
            var bb1 = new BB1();
            bb1.Print(); // 输出 BB1
            Console.WriteLine();

            A abb2 = new BB2();
            abb2.Print(); // 输出 B1
            B1 bbb2 = new BB2();
            bbb2.Print(); // 输出 B1
            var bb2 = new BB2();
            Console.WriteLine();
            bb2.Print(); // 输出 BB2
            Console.WriteLine();

            A ab22 = new B22();
            ab22.Print(); // 输出 A
            B2 bb22 = new B22();
            bb22.Print(); // 输出 B2
            var b22 = new B22();
            b22.Print(); // 输出 B22

        }
        #endregion

    }

    #region 讲解值类型与引用类型-创建的相关类
    class User
    {
        public int Age { get; set; }
    }
    #endregion

    #region 讲解类型、方法与继承-创建的相关类
         
    public class A 
    {
        public virtual void Print() => Console.WriteLine("A");
    }

    public class B1 : A
    {
        public override void Print() => Console.WriteLine("B1");
    }

    public class BB1 : B1
    {
        public override void Print() => Console.WriteLine("BB1");
    }

    public class BB2 : B1
    {
        public new void Print() => Console.WriteLine("BB2");
    }

    public class B2 : A
    {
        public new void Print() => Console.WriteLine("B2");
    }

    public class B22 : B2
    {
        public new void Print() => Console.WriteLine("B22");
    }
    #endregion

}
