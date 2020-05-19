using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Field_Property_Index_Const
{
    class Program
    {
        static void Main()
        {
            try
            {
                Stu stu1 = new Stu();
                stu1.SetAge(20);

                Stu stu2 = new Stu();
                stu2.SetAge(20);

                Stu stu3 = new Stu();
                // 赋予字段非法值 导致字段被污染 可以在类中定义get和set字段的方法来保护字段
                stu3.SetAge(200);

                var avgAge = (stu1.GetAge() + stu2.GetAge() + stu3.GetAge()) / 3;
                Console.WriteLine(avgAge);
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        class Stu
        {
            private int age;

            public int GetAge()
            {
                return age;
            }

            public void SetAge(int value)
            {
                if (value >= 0 && value <= 120)
                {
                    age = value;
                }
                else
                {
                    throw new Exception("Age value has error.");
                }
            }
        }

        static void Main2()
        {
            //Student stu1 = new Student(1);
            //Console.WriteLine(stu1.ID);
            //stu1.ID = 2;
            Console.WriteLine(Brush.DefaultColor.Red);
            Console.WriteLine(Brush.DefaultColor.Green);
            Console.WriteLine(Brush.DefaultColor.Blue);

        }

        struct Color
        {
            public int Red;
            public int Green;
            public int Blue;
        }

        class Brush
        {
            // 只读字段只能进行一次初始化 要么在声明只读字段时进行初始化 要么在静态构造器中对只读字段进行初始化
            public static readonly Color DefaultColor/* = new Color() { Red = 0, Green = 0,Blue = 0 }*/;

            static Brush()
            {
                Brush.DefaultColor = new Color() { Red = 0,Green = 0,Blue = 0 };
            }

        }


        static void Main1(string[] args)
        {
            List<Student> stuList = new List<Student>();
            Student.ReportAmount(); // 200 因为在student类中使用了静态字段的构造器对静态字段Amount进行初始化
            for (int i = 0; i < 100; i++)
            {
                Student stu = new Student();
                stu.Age = 24;
                stu.Score = i;
                stuList.Add(stu);

            }

            int totalAge = 0;
            int totalScore = 0;
            foreach (var stu in stuList)
            {
                totalAge += stu.Age;
                totalScore += stu.Score;
            }

            Student.AverageAge = totalAge / Student.Amount;
            Student.AverageScore = totalScore / Student.Amount;
                
            Student.ReportAmount(); // 300
            Student.ReportAverageAge();
            Student.ReportAverageScore();
        }
    }

    class Student
    {
        public readonly int ID;

        public Student(int id)
        {
            this.ID = id;
        }

        // 实例字段
        public int Age;
        public int Score;
           
        // 静态字段 
        public static int AverageAge;
        public static int AverageScore;
        public static int Amount;

        // 实例字段构造器 也即初始化器 也即构造函数
        // 将实例字段与静态字段联系起来了 当每个Student实例创建时 让Amount加1
        public Student()
        {
            Student.Amount++;
        }

        // 静态构造器 也即静态字段的初始化器 也即静态字段的构造函数
        static Student()
        {
            Student.Amount = 200;
        }

        public static void ReportAmount()
        {
            Console.WriteLine(Student.Amount);
        }

        public static void ReportAverageAge()
        {
            Console.WriteLine(Student.AverageAge);
        }

        public static void ReportAverageScore()
        {
            Console.WriteLine(Student.AverageScore);
        }
    }
}
