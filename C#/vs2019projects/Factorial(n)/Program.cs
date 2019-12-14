using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorial_n_
{
    class Program
    {
        static void Main(string[] args)
        {
            Factorial f = new Factorial();
            //int res = f.GetFactorial(3);
            //Console.WriteLine(res);
            int res;
            for (; ; )
            {
                Console.WriteLine("pls input a num:");
                int n = Convert.ToInt32(Console.ReadLine());
                res = f.GetFactorialPro(n,1);
                Console.WriteLine(res);
                //if (Console.ReadLine() == "quit")
                //{
                //    break;
                //}
            }
        }
    }

    class Factorial
    {
        public int GetFactorial(int n)  //传统的递归方法来求n的阶乘
        {
            if (n == 1)
            {
                return 1;
            }
            else
            {
                return n * GetFactorial(n - 1);
            }

        }

        public int GetFactorialPro(int n, int res)//使用“尾递归”的方法求n的阶乘
        //当递归调用是函数体中最后执行的语句并且它的返回值不属于表达式一部分即它的返回值不参与构成一个表达式时， 这个递归就是尾递归
        /*
         现代的编译器就会发现这个特点， 生成优化的代码， 复用栈帧。 
         第一个算法中因为有个n * factorial(n-1) ,  虽然也是递归，但是递归的结果处于一个表达式中
         还要做计算， 所以就没法复用栈帧了，只能一层一层的调用下去。
         */
        {
            if (n == 1)
            {
                return res;
            }
            else
            {
                return GetFactorialPro(n-1,n*res);
            }

        }

    }
}
