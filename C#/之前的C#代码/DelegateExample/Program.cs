using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateExample
{
    class Program
    {
        // 通常类也类之间的定义都是平级的，但C#中允许嵌套类声明类（即一个类中可以声明另一个类）
        public delegate double Calc(double x,double y);
        static void Main()
        {
            Type t = typeof(Action);
            //  委托是类
            Console.WriteLine(t.IsClass);

            CalculatorPro calculatorpro = new CalculatorPro();
            Calc calc1 = new Calc(calculatorpro.Mul);

            Console.WriteLine(calc1(5,6));
        }

        class CalculatorPro
        {
            public double Mul(double x, double y)
            {
                return x * y;
            }

            public double Div(double x, double y)
            {
                return x / y;
            }
        }

        static void Main1(string[] args)
        {
            Calculator calculator = new Calculator();
            // Action委托了一个无返回值也无形参的方法
            Action action = new Action(calculator.Report);
            calculator.Report();
            // 委托类型间接调用方法的两种格式 第二种是仿照C语言中的函数指针的简略写法
            action.Invoke();
            action();

            // Func可以委托有形参有返回值的方法 第一个参数是被委托方法的第一个形参，第二参数是其第二个形参 而最后一个参数是被委托方法的返回值类型
            Func<int, int, int> func1 = new Func<int, int, int> (calculator.Add);
            Func<int, int, int> func2 = new Func<int, int, int>(calculator.Sub);

            int x = 100;
            int y = 200;
            int z = 0;

            z = func1(x,y);
            Console.WriteLine(z);
            z = func2(x,y);
            Console.WriteLine(z);

        }
    }

    class Calculator
    {
        public void Report()
        {
            Console.WriteLine("I have 3 methods.");
        }

        public int Add(int a,int b)
        {
            return a + b;
        }

        public int Sub(int a, int b)
        {
            return a - b;
        }
    }
}
