using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParametersExample
{
    class Program
    {
        static void Main()
        {
            int y = 1;
            IWantSideEffect(ref y);
            Console.WriteLine(y);
        }

        // 数据类型为值类型的引用形参
        static void IWantSideEffect(ref int x)
        {
            x = x + 100;
        }

        static void Main2()
        {
            Student stu = new Student { Name = "Tim"};
            UpdateObject(stu);
            Console.WriteLine("HashCode{0},Name={1}.", stu.GetHashCode(), stu.Name);
        }

        // 数据类型为引用类型的传值参数：只操作对象，不创建新对象：在实际工作中不常见
        static void UpdateObject(Student stu)
        {
            stu.Name = "Tom";// 副作用，side-effect
            Console.WriteLine("HashCode{0},Name={1}.",stu.GetHashCode(),stu.Name);
        }


        static void Main1(string[] args)
        {
            var stu = new Student { Name = "Tim"};
            SomeMethod(stu);
            Console.WriteLine(stu.Name);
            Console.WriteLine("{0} HashCode is {1}.", stu.Name, stu.GetHashCode());

            int y = 100;
            stu.AddOne(y);
            Console.WriteLine(y);
        }

        // 数据类型为引用类型的传值参数：并且创建新对象
        static void SomeMethod(Student stu)
        {
            stu = new Student { Name="Tim"};
            Console.WriteLine(stu.Name);
            Console.WriteLine("{0} HashCode is {1}.",stu.Name,stu.GetHashCode());
        }
    }

    class Student
    {
        public string Name { get; set; }

        // 数据类型为值类型的传值参数
        public void AddOne(int x)
        {
            x = x + 1;
            Console.WriteLine(x);
        }
    }
}
