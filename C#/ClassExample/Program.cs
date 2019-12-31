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
            Student stu = new Student(1, "Timothy");
            Console.WriteLine(stu.ID);
            Console.WriteLine(stu.Name);
            stu.Report();
        }
    }

    class Student 
    {
        // 实例构造器
        public Student(int id ,string name)
        {
            this.ID = id;
            this.Name = name;
        }

        ~Student()
        {
            Console.WriteLine("Bye bye! Relaese the system resources...");
        }
        public int ID { get; set; }

        public string Name { get; set; }

        public void Report()
        {
            Console.WriteLine($"I'm #{ID} student , my name is {Name}.");
        }
    }
}
