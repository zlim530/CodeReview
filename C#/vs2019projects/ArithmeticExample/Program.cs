using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Arithmetic a = new Arithmetic();
            //a.PrintXTo1(10);
            //a.PrintXTo1Pro(10);

            Sum s = new Sum();
            int sum = s.Sum1ToX(100);
            Console.WriteLine(sum);
            s.MathMethod(100);
        }
    }

    class Sum
    {
        //public int Sum1ToX(int x)
        //{
        //    int sum = 0;
        //    for (int i = 1; i <= x; i++)
        //    {
        //        sum += i;
        //    }
        //    return sum;
        //}

        public int Sum1ToX(int x)
        {
            if (x == 1)
            {
                return 1;
            }
            else
            {
                return x + Sum1ToX(x - 1);
            }
        }

        public void MathMethod(int x)
        {
            //(1 + x) * x / 2;
            Console.WriteLine(((1+x) * x/2));
        }
        
    }

    class Arithmetic
    {
        public void PrintXTo1(int x)//循环实现从x打印到1
        {
            for (int i = x; i > 0; i--)
            {
                Console.WriteLine(i);
            }
        }

        public void PrintXTo1Pro(int x)//递归实现从x打印到1  递归：即在方法（函数）中调用本身
        {
            if (x == 1)
            {
                Console.WriteLine(x);
            }
            else
            {
                Console.WriteLine(x);
                PrintXTo1Pro(x - 1);
            }
        }
    }
}
