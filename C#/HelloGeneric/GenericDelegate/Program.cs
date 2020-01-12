using System;

namespace GenericDelegate
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<string> a1 = Say;
            a1("Timothy");
            Action<int> a2 = Mul;
            a2.Invoke(1);
            // 声明两个double类型的局部变量a和b，并()中声明的局部变量带入=>后面{}的表达式逻辑中：即lambda表达式
            Func<double, double, double> func1 = (double a, double b) => { return a + b; };
            var restult = func1(100.1,200.2);
            Console.WriteLine(restult);
            // 因为前面的泛型委托Func<>中已经表明了所委托方法两个形参的数据数据类型与返回值类型，在这里均为double类型，故lambda表达式中的()可以不用声明局部变量的数据类型，而一定是double类型
            Func<int, int, int> func2 = (a, b) => { return a + b; };
            var restult2 = func2(100,200);
            Console.WriteLine(restult2);
        }

        static int Add(int a, int b)
        {
            return a + b;
        }

        static double Add(double a,double b)
        {
            return a + b;
        }

        static void Say(string str)
        {
            Console.WriteLine($"Hello,{str}!");
        }

        static void Mul(int x)
        {
            Console.WriteLine(x * 100);
        }
    }
}
