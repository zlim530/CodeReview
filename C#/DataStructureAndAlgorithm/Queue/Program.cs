using System;
using System.Collections.Generic;

namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Queue<int> queue = new Queue<int>();
            queue.EnQueue(11);
            queue.EnQueue(22);
            queue.EnQueue(33);
            queue.EnQueue(44);
            /* 44 33 22 11*/

            for (int i = 0; i < queue.GetSize(); i++)
            {
                Console.WriteLine(queue.GetFront());
            }
        }
    }
}
