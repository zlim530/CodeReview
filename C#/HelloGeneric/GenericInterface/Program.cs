using System;
using System.Collections.Generic;

namespace GenericInterface
{
    class Program
    {
        static void Main()
        {
            IDictionary<int, string> dict = new Dictionary<int,string>();
            dict[1] = "Timothy";
            dict[2] = "Michael";
            Console.WriteLine($"Student #1 is  {dict[1]}");
            Console.WriteLine($"Student #2 is  {dict[2]}");

        }

        static void Main2()
        {
            // List实现了IList接口也即List继承了IList接口 故可以用IList<int>类型的变量引用List<int>类型的实例
            IList<int> list = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }


        static void Main1(string[] args)
        {
            Student stu = new Student();
            stu.ID = 1000000000000000001;
            stu.Name = "Timothy";
            Console.WriteLine(stu.ID);
            Console.WriteLine(stu.Name);
        }
    }

    interface IUnique<TId>
    {
        TId ID { get; set; }
    }

    class Student: IUnique<ulong>
    {
        public ulong ID { get ; set ; }

        public string Name { get; set; }
    }
}
