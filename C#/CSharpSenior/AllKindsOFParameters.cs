using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    class AllKindsOFParameters {

        static void Main(string[] args) {
            Student stu = null;

            if (StudentFactory.Create("Tim",33,out stu)) {
                Console.WriteLine("Student {0} ,age is {1}",stu.Name,stu.Age);
            }
        }

    }

    class Student {
        public int Age { get; set; }
        public string Name { get; set; }
    }

    class StudentFactory {
        public static bool Create(string stuName,int stuAge,out Student stu) {
            stu = null;
            if (string.IsNullOrEmpty(stuName)) {
                return false;
            }
            if ( stuAge < 20 || stuAge > 80) {
                return false;
            }
            stu = new Student() { Name = stuName,Age = stuAge};
            return true;
        }
    }

}
