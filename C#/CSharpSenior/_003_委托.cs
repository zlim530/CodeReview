using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    class _003_委托 {
        static void Main0(string[] args) {
            Calculator calculator = new Calculator();
            // Action只能委托用于无形参无返回值的方法
            Action action = new Action(calculator.Report);
            calculator.Report();
            // 委托类型间接调用方法的两种格式 
            // 第二种是仿照C语言中的函数指针的简略写法
            action.Invoke();
            action();

            // Func可以委托有形参有返回值的方法 
            //第一个参数是被委托方法的第一个形参
            //第二参数是其第二个形参 而最后一个参数是被委托方法的返回值类型
            Func<int, int, int> func1 =
            new Func<int, int, int>(calculator.Add);
            Func<int, int, int> func2 =
            new Func<int, int, int>(calculator.Sub);

            int x = 100;
            int y = 200;
            int z = 0;

            z = func1(x, y);
            Console.WriteLine(z);
            z = func2(x, y);
            Console.WriteLine(z);

        }

        static void Main(string[] args) {
            var calculator = new Calculator();
            var calc1 = new Calc(calculator.Mul);
            Console.WriteLine(calc1(5,6));
        }
    }

    public delegate double Calc(double x,double y);

    class Calculator {
        public void Report() {
            Console.WriteLine("I have 3 methods.");
        }

        public int Add(int a, int b) {
            return a + b;
        }

        public int Sub(int a, int b) {
            return a - b;
        }

        public double Mul(double x,double y) {
            return x * y;
        }

        public double Div(double x,double y) {
            return x / y;
        }
    }
}
