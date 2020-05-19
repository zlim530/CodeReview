using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            //成员访问操作符：.
            //作用：
            //    访问外层名称空间中的子名称空间
            //    访问名称空间中的类型
            //    访问类型的静态成员
            //    访问对象中的实例成员

            //System.IO.File.Create("c:\\test.txt");

            //Form myform = new Form();
            //myform.Text = "hello world!";
            //myform.ShowDialog();

            //方法调用操作符：f(x) f 即 function 表示函数 也即方法 x表示
            //    表示参数(可以没有)：
            //    C# 里方法调用都要用到()。
            //    Action 是委托，委托在创建时只需要知道方法的名称，
            //    不调用方法，所以只会用到方法名（不加()）。
            //    当然最终myAction(); 也用到了方法调用操作符()。

            Calculator c = new Calculator();
            double x = c.Add(3.4,5.0);
            Console.WriteLine(x);

            Action myAction = new Action(c.PrintHello);
            // 委托Action只能委托那些没有返回值也没有参数的方法
            myAction();

        }
    }

    class Calculator
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public void PrintHello()
        {
            Console.WriteLine("Hello");
        }
    }
}
