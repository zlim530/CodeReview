using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParamsParameters
{
    class Program
    {
        static void Main()
        {
            PrintInfo("Tim",34);

            PrintInfo(age:24,name:"Tom");
        }

        static void PrintInfo(string name, int age)
        {
            Console.WriteLine("Hello {0},you are {1}.",name,age);
        
        }

        static void Main1(string[] args)
        {
            //var myIntArray = new int[] { 1,2,3};
            //int result = CalculateSum(myIntArray);
            int result = CalculateSum(1,2,3);
            Console.WriteLine(result);

            int x = 100;
            int y = 200;
            int z = x + y;
            Console.WriteLine("{0}+{1}={2}.",x,y,z);

            string str = "Tim:Tom.Amy;Lisa";
            // string[] string.Split(params char[] separator);
            // 字符串分割方法，可以设置多个分隔符
            string[] restult = str.Split(':','.',';');
            foreach (var name in restult)
            {
                Console.WriteLine(name);
            }
        }

        // 使用params数组参数后，不再需要单独声明数组 
        // 其实我们早就接触过params参数了 Console.WriteLine()方法其中有一个重载就用到了params参数 即参照指定格式输出参数时
        // void Console.WriteLine(string format,params object[] arg)
        static int CalculateSum(params int[] intArray)
        {
            int sum = 0;
            foreach (var item in intArray)
            {
                sum += item;
            }
            return sum;
        }

        // 在使用params关键字前：每次调用CalculateSum()方法都需要提前声明一个数组
        //static int CalculateSum(int[] intArray)
        //{
        //    int sum = 0;
        //    foreach (var item in intArray)
        //    {
        //        sum += item;
        //    }

        //    return sum;
        //}
    }
}
