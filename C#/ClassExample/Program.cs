using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //类可以用于声明类（类型）变量、创建实例等
            Student stu = new Student(1, "Timothy");
            //类是实例的模板
            Console.WriteLine(stu.ID);
            Console.WriteLine(stu.Name);
            stu.Report();
            var stu2 = new Student {
                ID = 2,
                Name = "Tom"
            };
            Console.WriteLine(Student.Amount);
            Console.ReadKey();

            // 类的高级应用
            //Type t = typeof(Student);
            //object o = Activator.CreateInstance(t,1,"Timothy");
            //Student stu = o as Student;
            //Console.WriteLine(o is Student);// True
            //Console.WriteLine(stu.Name); // Timothy
        }
    }
    // 类是一种数据结构 具体到每一个类 都是自定义的引用类型
    class Student 
    {

        public Student() {

        }

        // 实例构造器
        public Student(int id ,string name)
        {
            this.ID = id;
            this.Name = name;
            Amount = 100;
            Amount++;
        }
        public static int Amount { get; set; }

        //static Student()
        //{
        //    Amount = 100;    
        //}

        ~Student()
        {
            Console.WriteLine("Bye bye! Relaese the system resources...");
            Amount--;
        }

        // 从现实世界学生群体中抽象出来的属性：学生的ID与学生的姓名
        public int ID { get; set; }

        public string Name { get; set; }

        // 从现实世界学生群体汇总抽象出来的行为
        public void Report()
        {
            Console.WriteLine($"I'm #{ID} student , my name is {Name}.");
        }
    }

    class Car {
        private string description;

        private uint nWheels;

        public Car(string description ,uint nWheels) {
            this.description = description;
            this.nWheels = nWheels;
        }

    }
}
