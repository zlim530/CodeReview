using System;
using System.Threading.Tasks;
using System.Threading;

namespace SynchronousCallOfDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Student stu1 = new Student() { ID = 1, PenColor = ConsoleColor.Yellow };
            Student stu2 = new Student() { ID = 2, PenColor = ConsoleColor.Green };
            Student stu3 = new Student() { ID = 3, PenColor = ConsoleColor.Red };

            // 第一种同步调用：直接同步调用
            //stu1.DoHomework();
            //stu2.DoHomework();
            //stu3.DoHomework();

            // 一个委托类只封装了一个方法：单播委托
            Action action1 = new Action(stu1.DoHomework);
            Action action2 = new Action(stu2.DoHomework);
            Action action3 = new Action(stu3.DoHomework);

            // 单播委托
            // 第二种同步调用：同步调用委托间接方法调用
            //action1.Invoke();
            //action2.Invoke();
            //action3.Invoke();

            // 多播委托
            // 第三种同步调用：同步调用多播委托间接方法调用
            action1 += action2;
            action1 += action3;
            action1.Invoke();

            // 模拟主线程在三个方法调用完毕后还要做一些事情
            for (int i = 0; i < 10; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Main thread {0}.",i);
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
