using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var stu1 = new Student();
                stu1.Age = 20;

                var stu2 = new Student();
                stu2.Age = 20;

                var stu3 = new Student();
                stu3.Age = 120;

                var avgAge = (stu1.Age + stu2.Age +stu3.Age) / 3;

                Console.WriteLine(avgAge);

                //var stu = new Student();
                Student.Amount = -10;
                Console.WriteLine(Student.Amount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    class Student
    {
        private int age;

        // 实例属性
        public int Age
        {
            get
            { 
                return age; 
            }
            set 
            {
                if (value >= 0 && value <= 120)
                {
                    this.age = value;
                }
                else
                {
                    throw new Exception("Age value has error.");
                }
            }
        }

        private static int amount;

        // 静态属性
        public static int Amount
        {
            get { return amount; }
            set {
                if (value >= 0)
                {
                    Student.amount = value;
                }
                else
                {
                    throw new Exception("Amount must greater than 0.");
                }
            }
        }




    }
}
