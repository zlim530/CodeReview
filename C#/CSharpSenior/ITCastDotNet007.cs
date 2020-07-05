using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
