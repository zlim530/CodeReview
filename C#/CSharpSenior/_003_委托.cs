using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpSenior {
    /*
    认识委托：
        1.什么是委托？
            1.1 委托是一个类(class)
            1.2 委托包含的是具有相同方法签名，相同返回值类型的多个方法（也可以只有一个）
        2.为什么要使用委托？
            2.1 省去了大量的 if...else 或 switch 的判断
            2.2 使程序更加面向对象，具有更好的扩展性
        3.委托的应用场景
            3.1 winform 和 webform 的按钮 click 事件
            2.3 Linq运算符中的 Where 和 Select 中的 Func 和 Action 委托
            2.3 Tracy.Proxy 接口代理组件
    */
    class _003_委托 {

        public delegate void MyDelegate();

        static void Main1(string[] args) {
            MyDelegate myDelegate = new MyDelegate(() => {
                Console.WriteLine("Hello,World!");
            });
        }

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
