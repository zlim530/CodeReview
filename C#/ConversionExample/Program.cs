using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversionExample
{
    class Program
    {
        static void Main3()
        {
            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(i%10);
            //}
            var x = 3.0 + 2;
            Console.WriteLine(x.GetType().FullName);//System.Double
            Console.WriteLine(x);

            int y = 7;
            int z = y << 2;
            string strY = Convert.ToString(y, 2).PadLeft(32,'0');
            string strZ = Convert.ToString(z, 2).PadLeft(32, '0');
            Console.WriteLine(strY);
            Console.WriteLine(strZ);
            Console.WriteLine(z);//28
            // 在不溢出的情况下 二进制左移相当于乘2  右移相当于除2

        }

        static void Main()
        {
            char c1 = 'a';
            char c2 = 'A';
            var res = c1 < c2;
            Console.WriteLine(res.GetType().FullName);
            Console.WriteLine(res);
            ushort u1 = (ushort)c1;
            ushort u2 = (ushort)c2;
            Console.WriteLine(u1);
            Console.WriteLine(u2);

            string str1 = "abc";
            string str2 = "Abc";
            // 忽略大小写比较字符串 即将字符串均转为大写或小写再进行比较即可
            Console.WriteLine(str1.ToLower() == str2.ToLower());
        
        }

   
        static void Main1(string[] args)
        {
            Console.WriteLine(ushort.MaxValue);
            uint x = 65536;
            // 强制类型转换也即显式类型转换时可能会发生丢失精度的现象
            ushort y = (ushort)x;
            Console.WriteLine(y);
        }

        static void Main2()
        {
            Stone stone = new Stone();
            stone.Age = 5000;
            //Monkey m = (Monkey)stone;
            // 在stone类中没有添加任何操作前 直接将没有任何关联的两个类型变量进行强制转化会报错
            //Monkey m = (Monkey)stone;// 显式类型转换 对应explicit
            Monkey m = stone;   // 隐式类型转换 对应implicit
            Console.WriteLine(m.Age);
        }

        class Stone
        {
            public int Age;

            // 如果是隐式类型转换即将explicit 换为 implicit 
            // 这就是类型转换的本质
            //public static explicit operator Monkey(Stone stone)
            public static implicit operator Monkey(Stone stone)
            {
                Monkey m = new Monkey();
                m.Age = stone.Age / 500;
                return m;
            }

        }

        //class Stone
        //{
        //    public int Age;
        //}

        class Monkey
        {
            public int Age;
        }
    }
}
