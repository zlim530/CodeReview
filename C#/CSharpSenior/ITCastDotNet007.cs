using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

/**
 * @author zlim
 * @create 2020/6/20 23:24:42
 */
namespace CSharpSenior {
    public class ITCastDotNet007 {

        public static void Main0(string[] args) {
            // 2.使用这个委托：
            // 声明了一个委托变量 md，并且 new 了一个委托对象，而后把方法 M1 作为参数传递进去
            MyDelegate md = new MyDelegate(M1);

            // 3.调用 md 委托的时就相当于调用了 M1 方法
            md();

        }

        static void M1() {
            Console.WriteLine("This is a void Method.");
        }

    }
    /*
    1.定义委托类型：
        定义一个委托类型，用来保存无参数，无返回值的方法
        其中 MyDelegate 为委托的名字，void 表示只能存储没有返回值的方法 MyDelegate() 表示存储没有参数的方法
    */
    public delegate void MyDelegate();

    public class Program {
        public static void Main1(string[] args) {
            //TestClass tc = new TestClass();
            //WriteTimeDelegate writeTime = new WriteTimeDelegate(WritrTimeToFile);
            //WriteTimeDelegate writeTime = new WriteTimeDelegate(PrintTimeToConsole);
            //tc.DoSomething(writeTime);
            //Console.WriteLine("OK");

            string[] names = new string[] { "Tim", "Tom" };
            GetStringDelegate changeString = new GetStringDelegate(ChangeStrings);
            Program.ChangeStrings(names, changeString);
            for (int i = 0; i < names.Length; i++) {
                Console.WriteLine(names[i]);
            }
        }

        public static string ChangeStrings(string name) {
            return name.ToUpper();
        }

        public static void ChangeStrings(string[] strs) {
            for (int i = 0; i < strs.Length; i++) {
                strs[i] = "☆" + strs[i] + "☆";
            }
        }

        public static void ChangeStrings(string[] strs,GetStringDelegate changeString) {
            for (int i = 0; i < strs.Length; i++) {
                strs[i] = changeString(strs[i]);
            }
        }

        public static void WritrTimeToFile() {
            File.WriteAllText("time.txt", System.DateTime.Now.ToString());
        }

        public static void PrintTimeToConsole() {
            Console.WriteLine(System.DateTime.Now.ToString());
        }
    }

    public delegate string GetStringDelegate(string name);

    public class TestClass {
        public void DoSomething(WriteTimeDelegate writeTime) {
            Console.WriteLine("===============");
            Console.WriteLine("===============");
            //Console.WriteLine(System.DateTime.Now.ToString());
            if (writeTime != null) {
                writeTime();
            }
            Console.WriteLine("===============");
            Console.WriteLine("===============");
        }
    }

    public delegate void WriteTimeDelegate();
}

namespace 委托复习 {
    public class Program {
        public static void Main0(string[] args) {
            /*
            1.委托是一种数据类型，与类同级，一般是直接在命令空间中定义；
            2.定义委托时，需要指明返回值类型、委托名与参数列表，这样就可以确定该类型的委托可以存储什么样的方法；
            3.使用委托：
                3.1 声明委托变量
                3.2 委托是引用类型，因此当声明委托变量后，如果不能赋值则该委托变量默认值为 null，因此在使用委托变量之前最好是
                做非空检验：MyDelegate != null
                3.3 委托类型的变量只能赋值一个委托类型的对象，但是同一个委托变量可以委托多个方法
            */
            //MyDelegate md = new MyDelegate(M1);
            //md.Invoke();
            MyDelegate md = M1;
            md();
        }

        public static void Main1(string[] args) {
            while (true) {
                Console.WriteLine("请输入邮箱：");
                string email = Console.ReadLine();
                email = Regex.Replace(email, @"(\w+)(@\w+\.\w)", ReplaceMethod, RegexOptions.ECMAScript);
                Console.WriteLine(email);
            }
        }

        static string ReplaceMethod(Match match) {
            string uid = match.Groups[1].Value;
            string others = match.Groups[2].Value;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < uid.Length; i++) {
                sb.Append("*");
            }
            sb.Append(others);
            return sb.ToString();
        }

        static void M1() {
            Console.WriteLine("M1 function.");
        }
    }

    public delegate void MyDelegate();
}

namespace 匿名方法与Lambda表达式 {
    public class Program {
        public static void Main0(string[] args) {
            // 匿名方法：匿名方法不能直接在类中定义，而是在给委托变量赋值时，需要赋值一个方法，此时可以“现做现卖”，定义一个匿名方法给该委托
            //无参数、无返回值的匿名方法
            MyDelegate md = delegate () {
                Console.WriteLine("Hello,World!");
            };
            //无参数、无返回值的 lambda(λ) 表达式
            md += () => {
                Console.WriteLine("lambda!");
            };
            md();

            //有参数、无返回值的匿名方法
            MyDelegate1 md1 = delegate (string msg) {
                Console.WriteLine(msg);
            };
            md1("Hello,World!2");
            //有参数、无返回值的 lambda 表达式：lambda 表达式不需要表明数据类型，因为委托已经限定了参数的数据类型
            MyDelegate1 md2 = msg => {
                Console.WriteLine(msg);
            };
            md2("hello,Lambda!");

            //有参数、有返回值的匿名方法
            AddDelegate add = delegate (int n1, int n2, int n3) {
                return n1 + n2 + n3;
            };
            Console.WriteLine(add(10, 20, 30));
            //有参数、有返回值的 lambda 表达式
            AddDelegate add1 = (n1,n2,n3) => {
                return n1 + n2 + n3;
            };
            Console.WriteLine(add1(1,2,3) + "lambda!");

            AddDelegate1 add2 = (arr) => {
                for (int i = 0; i < arr.Length; i++) {
                    Console.WriteLine(arr[i]);
                }
                return arr.Sum();
            };
            Console.WriteLine(add2(3,4,5)+" lambda!!");
        }
    }

    public delegate int AddDelegate1(params int[] arr);
    public delegate int AddDelegate(int n1, int n2, int n3);
    public delegate void MyDelegate1(string msg);
    public delegate void MyDelegate();
}
