using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CSharpSenior {
    class AllKindsOFParameters {

        static void Main(string[] args) {
            //Student stu = null;

            //if (StudentFactory.Create("Tim",33,out stu)) {
            //    Console.WriteLine("Student {0} ,age is {1}",stu.Name,stu.Age);
            //}

            var outterStu = new Student() { Age = 24,Name = "Tim"};
            Console.WriteLine("HashCode = {0},Name = {1}", outterStu.GetHashCode(), outterStu.Name);
            Console.WriteLine("=========================================");
            IWantSideEffect(ref outterStu);
            Console.WriteLine("HashCode = {0},Name = {1}", outterStu.GetHashCode(), outterStu.Name);
            Console.WriteLine("=========================================");
            string outterStuAddr = getMemory(outterStu);
            Console.WriteLine(outterStuAddr);
        }

        static void IWantSideEffect(ref Student stu) {
            stu = new Student() { Age = 23, Name = "Tom"};
            Console.WriteLine("HashCode = {0},Name = {1}",stu.GetHashCode(),stu.Name);
            //string stus = Convert.ToString(stu,2).PadLeft(32,'0');
        }

        public static string getMemory(object o) // 获取引用类型的内存地址方法  
        {
            GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

            IntPtr addr = GCHandle.ToIntPtr(h);

            return "0x" + addr.ToString("X");
        } 


    }

    [StructLayout(LayoutKind.Sequential)]
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
