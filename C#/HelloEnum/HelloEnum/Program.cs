using System;

namespace HelloEnum
{
    class Program
    {
        static void Main2()
        {
            // 这里的student是一个结构体Student类型的位于Main函数也即主函数中的局部变量，在内存中存储在Main函数的栈内存中，并且这个局部变量的栈内存中直接存储着Student类型的实例
            Student student = new Student() { ID = 101,Name = "Timothy"};
            // 装箱：对student这个实例做了一个copy并存储在堆内存中，而后在栈内存的obj这个变量中引用着在堆内存中的student实例，所以引用着这个实例，也即存储着它的内存地址
            Object obj = student;
            // 拆箱
            Student student2 = (Student)obj;
            Console.WriteLine($"#{student2.ID} Name:{student2.Name}");
        }

        static void Main3()
        {
            Student stu1 = new Student() { ID = 101,Name = "Timothy"};
            // 值类型进行的值传递，stu2对象的改变与stu1对象没有任何关系
            Student stu2 = stu1;
            stu2.ID = 1001;
            stu2.Name = "Michael";
            Console.WriteLine($"#{stu1.ID} Name:{stu1.Name}");//#101 Name:Timothy
            Console.WriteLine($"#{stu2.ID} Name:{stu2.Name}");//#1001 Name:Michael
        }

        static void Main()
        {
            Student stu1 = new Student() { ID = 101,Name = "Timothy"};
            Student stu2 = stu1;
            stu2.ID = 1001;
            stu2.Name = "Michael";
            stu1.Speak();//I'm #101 student Timothy.
        }

        interface ISpeak
        {
            void Speak();
        }

        // 结构体的声明与类的声明很类似，结构体是值类型
        struct Student:ISpeak
        {
            public int ID { get; set; }

            public string Name { get; set; }

            public Student(int id ,string name)
            {
                ID = id;
                Name = name;
            }

            public void Speak()
            {
                Console.WriteLine($"I'm #{ID} student {Name}.");
            }
        }

        static void Main1(string[] args)
        {
            Person person = new Person();
            person.Level = Level.Employee;

            Person boss = new Person();
            boss.Level = Level.Boss;

            Console.WriteLine(boss.Level > person.Level);// True
            // 会依次打印枚举类型所对应的字符串，如果想看枚举类型所对应的的整数值数值需要进行显式类型转换
            Console.WriteLine((int)Level.Employee);//0
            Console.WriteLine((int)Level.Manager);//100
            Console.WriteLine((int)Level.Boss);//200
            Console.WriteLine((int)Level.BigBoss);//201

            person.Name = "Timothy";
            person.Skill = Skill.Drive | Skill.Cook | Skill.Program | Skill.Teach;
            Console.WriteLine(person.Skill);// 15
            // 过时用法不推荐
            Console.WriteLine( (person.Skill & Skill.Cook ) == Skill.Cook);//True
            // .NET Framework 4.0 后推荐的用法
            Console.WriteLine((person.Skill.HasFlag(Skill.Program)));// True
        }
    }

    enum Level
    { 
        Employee,
        Manager = 100,
        Boss = 200,
        BigBoss,
    }

    enum Skill
    { 
        Drive = 1,
        Cook = 2,
        Program = 4,
        Teach = 8,
    }

    class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Level Level { get; set; }

        public Skill Skill { get; set; }
    }
}
