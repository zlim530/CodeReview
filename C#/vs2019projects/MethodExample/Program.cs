using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            int a = c.Add(4, 5);
            Console.WriteLine(a);
            string b = c.GetToday();
            Console.WriteLine(b);
            c.PrintSum(10,6);
        }
    }

    class Calculator
    {
        //为了让类外面的实例也可以使用方法 使用public关键字声明方法
        public int Add(int x, int y)
        //有形参且有返回值的方法 返回值是什么类型就在public后跟上其返回的数据类型
        {
            int res = x + y;
            return res;
        }
        //在方法声明中的变量也是变量 称为参数变量 且为形式参数变量 简称形参
        
        public string GetToday()
        //无形参但有返回值的方法
        {
            int day = DateTime.Now.Day;
            return day.ToString();
        }

        public void PrintSum(int x ,int y)
        //有形参但没有返回值的方法 若方法中没有返回值 则需要使用void关键字声明
        {
            int res = x + y;
            Console.WriteLine(res);
        }
    }
}
