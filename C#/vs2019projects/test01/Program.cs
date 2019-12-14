using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test01
{
    class Program
    {
        /// <summary>
        /// 这是主方法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("fibonacii(20) = ");
            Console.WriteLine(fibonacii(20));
            //Console.WriteLine("fibonacii(40) = ",fibonacii(40));
       
            //Console.Write("fibonacii(20) = ", fibonacii(20));
            Console.ReadKey();
        }

        public static int fibonacii(int i)
        {
            if (i <= 0)
            {
                return 0;
            }
            else if (i > 0 && i <= 2)
            {
                return 1;
            }
            else
            {
                return fibonacii(i - 1) + fibonacii(i - 2);
            }
        }

    }
}
