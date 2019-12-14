using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversionExamplePro
{
    class Program
    {
        static void Main1(string[] args)
        {
            Teacher t = new Teacher();
            // 判断变量是否为某一类型：is
            var res = t is Teacher;
            Console.WriteLine(res.GetType().FullName);
            Console.WriteLine(res);
        }

        static void Main2()
        {
            object o = new Teacher();
            // o这个对象像Teacher一样 对不对呀 如果o真的像Teacher一样 就将o这个变量的地址交给t这个变量
            // 否则的话 就把一个null值交给t
            Teacher t = o as Teacher;
            if (t != null)
            {
                t.Teach();
            }
        }

        static void Main3()
        {
            int x = 3;
            int y = 3;
            int a = 3;
            if ( x >y && a++>3)
            {
                Console.WriteLine("Hello");// 不会打印Hello
            }
            Console.WriteLine(a);//3 &&逻辑与的短路效应

            int xx = 5;
            if (xx > y && a++>3)// 后置加加与其他操作符进行组合运算时 先将变量的值拿出来参与运算 再进行自增 所以a=3>3为假 故不会打印hello hello
            {
                Console.WriteLine("hello hello");
            }
            Console.WriteLine(a); // 结束运算后 a进行自增 故a = 4

            a = 3;
            if ( xx> y || a++>3)
            {
                Console.WriteLine("hello || "); // 会打印hello ||
            }
            Console.WriteLine(a);// a = 3  ||逻辑或的短路效应

            if (x >y || ++a>3)
            {
                Console.WriteLine("Hello ||");
            }
            Console.WriteLine(a);// a = 4:前置加加会先进行自增操作再参与运算
        }

        static void Main4()
        {
            Nullable<int> x = null; //Nullable<> 可空的
            int? y = null;  // Nullable<>简写
            y = 99;
            Console.WriteLine(y);
            Console.WriteLine(y.Value);
            int? z = null;
            int yy = z ?? 80; // ?? null 合并操作符 即z 如果为 null，就拿 80 来代替
            Console.WriteLine(yy);
            //x = 100;
            //Console.WriteLine(x);
            Console.WriteLine(x.HasValue);
        }

        static void Main()
        {
            int x = 80;
            // ?: 条件操作符 唯一一个三元操作符，本质上就是 if else 的简写
            // 使用 () 将条件括起来，提高可读性。
            string str = (x >= 10) ? "Pass" : "Failed";
            Console.WriteLine(str);
        }




    }

    class Animal
    {
        public void Eat()
        {
            Console.WriteLine("Eating...");
        }
    }

    class Human : Animal
    {
        public void Think()
        {
            Console.WriteLine("Who am i?");
        }
    
    }

    class Teacher : Human
    {
        public void Teach()
        {
            Console.WriteLine("I'm teaching programming.");
        }
    }
}
