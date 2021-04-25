using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace AsynchronousInvokeOfDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Student stu1 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu2 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu3 = new Student() { ID = 3, PenColor = ConsoleColor.Red };

            //  第二种显式异步调用：是一种使用Task的高级显式异步调用
            Task task1 = new Task(new Action(stu1.DoHomework));
            Task task2 = new Task(new Action(stu2.DoHomework));
            Task task3 = new Task(new Action(stu3.DoHomework));

            task1.Start();
            task2.Start();
            task3.Start();

            // 模拟主线程在三个方法调用完毕后还要做一些事情
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main thread {0}.", i);
                Thread.Sleep(500);
            }
        }
    }

    class Student
    {
        public int ID { get; set; }
        public ConsoleColor PenColor { get; set; }

        public void DoHomework()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.ForegroundColor = this.PenColor;
                Console.WriteLine("Student {0} doing homework {1} hour(s).", this.ID, i);
                Thread.Sleep(500);
            }
        }
    }
}
