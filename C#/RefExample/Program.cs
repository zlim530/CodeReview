using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Student stu = new Student { Name= "Tim"};
            Console.WriteLine("HashCode{0},Name={1}.", stu.GetHashCode(), stu.Name);
            Console.WriteLine("----------------------------");
            IWantSideEffect(ref stu);
            Console.WriteLine("HashCode{0},Name={1}.", stu.GetHashCode(), stu.Name);

            Student outterStu = new Student { Name = "Tim"};
            Console.WriteLine("HashCode{0},Name={1}.", outterStu.GetHashCode(), outterStu.Name);
            Console.WriteLine("----------------------");
            IWantSideEffectPro(ref outterStu);
            Console.WriteLine("HashCode{0},Name={1}.", outterStu.GetHashCode(), outterStu.Name);
        }

        // 数据类型为引用类型的引用参数：不创建新对象值改变对象的值
        static void IWantSideEffectPro(ref Student stu)
        {
            stu.Name = "Tom";
            Console.WriteLine("HashCode{0},Name={1}.", stu.GetHashCode(), stu.Name);
        }


        // 数据类型为引用类型的引用参数：创建新对象
        static void IWantSideEffect(ref Student stu)
        {
            stu = new Student { Name = "Tom"};
            Console.WriteLine("HashCode{0},Name={1}.",stu.GetHashCode(),stu.Name);
        }
    }

    class Student
    {
        public string Name { get; set; }
    }
}
