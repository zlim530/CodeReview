using System;

namespace Reflection
{
    public class ReflectionTest
    {
        public ReflectionTest()
        {
            Console.WriteLine("无参构造函数~");
        }

        public ReflectionTest(string name)
        {
            Console.WriteLine($"有参构造函数：{name}");
        }

    }
}
