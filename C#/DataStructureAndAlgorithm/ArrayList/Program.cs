using System;

namespace DynamicArray
{
    class Program
    {
        static void Main()
        {
            ArrayList<Person> persons = new ArrayList<Person>();
            persons.Add(new Person(21,"Zoe"));
            persons.Add(new Person(35,"Timothy"));
            persons.Add(new Person(18,"Michael"));

            persons.Add(1,new Person(20,"Lily"));
            Console.WriteLine(persons.Get(0));
            Console.WriteLine(persons.Set(3, new Person(17, "Timmy")));
            Console.WriteLine(persons.GetSize());
            Console.WriteLine(persons.Remove(3));
            Console.WriteLine(persons.GetSize());
            persons.Report();
            System.Console.WriteLine("hello world!");
        }


        static void Main1(string[] args)
        {
            ArrayList<int> array = new ArrayList<int>(10);
            array.Add(10);
            array.Add(20);
            array.Add(30);
            array.Add(40);

            array.Add(1,11);
            Console.WriteLine(array.IsContains(50));
            Console.WriteLine(array.Get(0));
            Console.WriteLine(array.Set(4,44));
            Console.WriteLine(array.GetSize());
            Console.WriteLine(array.GetIndexOf(44));
            //array.Clear();
            Console.WriteLine(array.Remove(0));
            Console.WriteLine(array.GetSize());
            array.Report();
            
        }
    }

    class Person
    {


        public int Age { get; set; }

        public string Name { get; set; }

        public Person(int age, string name)
        {
            this.Age = age;
            this.Name = name;
        }
    }
}
