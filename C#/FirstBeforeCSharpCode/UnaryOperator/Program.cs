using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnaryOperator
{
    class Program
    {
        static void Main()
        {
            int x = 12345678;
            Console.WriteLine(x);
            int y = ~x;
            // ~ 求反操作符 按位取反
            Console.WriteLine(y);
            int z = y + 1;
            Console.WriteLine(z);

            var xStr = Convert.ToString(x, 2).PadLeft(32, '0');
            var yStr = Convert.ToString(y, 2).PadLeft(32, '0');
            Console.WriteLine(xStr);
            Console.WriteLine(yStr);

            Student stu = new Student(null);
            Console.WriteLine(stu.Name);

        }

        class Student
        {
            public string Name;
            public Student(string initName)
            {
                if (!string.IsNullOrEmpty(initName))
                {
                    this.Name = initName;
                }
                else
                {
                    throw new ArgumentException("initName cannot be null or empty");
                }

            }
        
        }

        static void Main1(string[] args)
        {
            var x = int.MaxValue; // -2147483647
            int y = -x;
            // 计算机求相反数：按位取反再加一
            //01111111111111111111111111111111 --> x
            //10000000000000000000000000000001 --> y
            Console.WriteLine(y);

            string xStr = Convert.ToString(x, 2).PadLeft(32,'0');
            Console.WriteLine(xStr);

            string yStr = Convert.ToString(y,2).PadLeft(32,'0');
            Console.WriteLine(yStr);
        }
    }
}
