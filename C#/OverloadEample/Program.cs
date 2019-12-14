using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverloadEample
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator ca = new Calculator();
            int res = ca.Add(4,3);
            Console.WriteLine(res);
            double r = ca.Add(3.0,4.0);
            Console.WriteLine(r);
        }
    }

    class Calculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }

        public double Add(double x, double y)
        {
            return x + y;
        }

        public int Add(int x, int y, int z)
        {
            return x + y + z;
        }
    }
}
