using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFunc
{
    class Program
    {
        static void Main(string[] args)
        {
            //Caculator ca = new Caculator();
            //double res = Caculator.GetCirleArea(10);
            //Console.WriteLine(res);
            //double x = 3.0;
            //double y = 4.0;
            //double res_sed = Caculator.GetCyV(x,y);
            //double res_thd = Caculator.GetCV(3.0,4.0);
            //Console.WriteLine(res_sed);
            //Console.WriteLine(res_thd);
            double result = Caculator.GetCV(100,100);
        }
    }

    class Caculator
    {
        public static double GetCirleArea(double r)
        {
            return Math.PI * r * r;
        }

        public static double GetCyV(double r, double h)
        {
            double a = GetCirleArea(r);
            return a * h;
        }

        public static double GetCV(double r, double h)
        {
            double cv = GetCyV(r,h);
            return cv / 3;
        }
    
    }


}
