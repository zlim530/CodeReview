using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person();
            Person p2 = new Person();
            p1.Name = "Dear";
            p2.Name = "Dear's wife";
            //List<Person> nation = Person.GetMany(p1, p2);
            List<Person> nation = p1 + p2;
            // 说明操作符的本质就是方法(即函数也即算法)的“简记法”
            foreach (var p in nation)
            {
                Console.WriteLine(p.Name);
            }

        }
    }

    class Person
    {
        public string Name;
        //public static List<Person> GetMany(Person p1,Person p2)
        public static List<Person> operator + (Person p1, Person p2)
        {
            List<Person> people = new List<Person>();
            people.Add(p1);
            people.Add(p2);
            for (int i = 0; i < 11; i++)
            {
                Person child = new Person();
                child.Name = p1.Name + "&" + p2.Name + "s child";
                people.Add(child);

            }

            return people;
        }
    }
}
