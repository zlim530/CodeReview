using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleApp
{
    class Rectangel
    {
        double length;
        double width;

        public void Acceptdetails()
        {
            length = 4.5;
            width = 3.4;
        }

        public double GetArea()
        {
            return length * width;
        }

        public void Display()
        {
            Console.WriteLine("length:{0}",length);
            Console.WriteLine("width:{0}",width);
            Console.WriteLine("Area:{0}",GetArea());
        }
    
    }

    class Program
    {
        static void Main(string[] args)
        {
            Rectangel r = new Rectangel();
            r.Acceptdetails();
            r.GetArea();
            r.Display();

        }
    }
}
