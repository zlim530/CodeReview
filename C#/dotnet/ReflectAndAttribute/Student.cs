using System;
using System.Collections.Generic;
using System.Text;

/**
 * @author zlim
 * @create 2020/8/5 17:33:30
 */
namespace ReflectAndAttribute {

    interface IPerson {
        void Greet();
    }

    [MyOwn]
    [MyOwn("BeiJing",Fee = 5500)]// 相当于 new MyOwn("BeiJing"){ Fee = 5500 }
    public class Student : IPerson {

        public Role Roles { get; set; }

        public void AddRole(Role role) {
            Roles = Roles | role;
        }

        private int _age = 23;

        public Student() {
            Console.WriteLine("This is Class Student's Constructor without parameter.");
        }

        public Student(string Name) {
            Console.WriteLine($"This is Class Student's Constructor with parameter:{Name}.");
        }

        public void Greet() {
            Console.WriteLine("Hi there~");
        }
    }

    public class Teacher : IPerson {

        public Teacher() {

        }

        public Role Roles { get; set; }

        public void AddRole(Role role) {
            Roles = Roles | role;
        }

        public Teacher(string Name) {
            Console.WriteLine($"This is teacher {Name}.");
        }

        public void Greet() {
            Console.WriteLine("Hello,Students.");
        }

    }

}
