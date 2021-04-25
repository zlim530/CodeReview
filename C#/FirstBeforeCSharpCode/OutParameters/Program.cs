using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutParameters
{
    class Program
    {
        static void Main()
        {
            Student stu = null;
            if ( StudentFactory.Create("Tim",23,out stu) )
            {
                Console.WriteLine("Student {0},age is {1}.",stu.Name,stu.Age);
            }
        }

        class Student
        {
            public int Age { get; set; }
            public string Name { get; set; }
        }

        class StudentFactory
        {
            public static bool Create(string stuName,int stuAge,out Student restult)
            {
                restult = null;
                if ( string.IsNullOrEmpty(stuName))
                {
                    return false;
                }

                if ( stuAge < 20 || stuAge > 80)
                {
                    return false;
                }

                restult = new Student() { Name = stuName,Age = stuAge};
                return true;

            }
        }

        static void Main2()
        {
            double x = 0;
            bool result = DoubleParser.TryParse("2",out x);
            if ( result)
            {
                Console.WriteLine(x);
            }
        }

        class DoubleParser
        {
            public static bool TryParse(string input, out double restult)
            {
                try
                {
                    restult = double.Parse(input);
                    return true;
                }
                catch   
                {
                    restult = 0;
                    return false;
                   
                }
            }
        }

        static void Main1(string[] args)
        {
            Console.WriteLine("Pls input first number:");
            var arg1 = Console.ReadLine();
            double x = 0;
            bool restult = double.TryParse(arg1, out x);
            if (restult == false)
            {
                Console.WriteLine("Input error.");
                return;
            }
            Console.WriteLine("Pls input second number:");
            var arg2 = Console.ReadLine();
            double y = 0;
            bool result_sed = double.TryParse(arg2,out y);
            if (result_sed == false)
            {
                Console.WriteLine("Input error!");
                return;
            }

            double z = x + y;
            Console.WriteLine(z);
        }
    }
}
