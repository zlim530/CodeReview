using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ThisParameters
{
    class Program
    {
        static void Main()
        {
            var myList = new List<int>() { 12,34,56,78};
            bool result = myList.All(i => i>10);
            Console.WriteLine(result);
            
        }

        static bool AllGreaterThanTen(List<int> intList)
        {
            foreach (var item in intList)
            {
                if ( item <= 10)
                {
                    return false;
                }
            }

            return true;
        }


        static void Main1(string[] args)
        {
            double x = 3.14159;
            // 因为double类型本身没有Round方法，故只能使用Math.Round
            double y = Math.Round(x,4);
            Console.WriteLine(y);

            double z = x.Round(4);
            Console.WriteLine(z);
        }


    }
    static class DoubleExtension
    {
        public static double Round(this double input, int digits)
        {
            return Math.Round(input, digits);
        }
    }
}
