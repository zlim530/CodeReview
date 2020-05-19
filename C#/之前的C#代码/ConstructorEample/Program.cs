using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructorEample
{
    class Program
    {
        static void Main(string[] args)
        {
            // 引用变量stu 也是局部变量 内存分配在栈区 
            Student stu = new Student();
            // new Student() 是实例 实例在内存中分配在堆区
            Student stu2 = new Student(2, "Mr. C#");
            // 因为string也是类类型 Name是string类型的引用变量 而这里的"Mr. C#"就是string类型的实例
            Console.WriteLine(stu.ID);
            Console.WriteLine(stu.Name==null);
            Console.WriteLine(stu2.ID);
            Console.WriteLine(stu2.Name);
            Console.WriteLine();
        }
    }

    class Student
    {
        // 带参数的构造器 使用ctor+tab+tab可快速调出构造器的定义语句块
        public Student(int initID,string initName)
        {
            this.ID = initID;
            this.Name = initName;
        }

        // 不带参数的构造器
        public Student()
        {
            ID = 1;
            Name = "Wang";
        }

        public int ID;
        // 整形是结构体类型 在内存中占4个字节
        public string Name;
        // string是类类型 而类类型是引用类型 故Name是引用类型的变量 
        // 而引用类型变量在内存中也是占4个字节 它里面存储的是实例的内存地址
    }
}
