using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CSharpSenior {
    class _002_Lambda运算符 {

        static void Main(string[] args) {
            // 使用带有方法语法的LINQ功能来演示Lambda表达式的用法：
            string[] words = { "bot","apple","apricot"};
            int minmalLength = words.Where(w => w.StartsWith("a")).Min(w => w.Length);
            Console.WriteLine(minmalLength);// 5

            int[] numbers = { 1,4,7,10};
            // lambda 表达式的输入参数在编译时是强类型。 当编译器可以推断输入参数的类型时，可以省略类型声明
            // 如果需要指定输入参数的类型，则必须对每个参数执行类型声明
            // int product = numbers.Aggregate(1, (int interim, int next) => interim * next);
            int product = numbers.Aggregate(1,(interim,next) => interim * next);
            Console.WriteLine(product);// 280

            // 在没有输入参数的情况下定义lambda表达式：
            Func<string> greet = () => "Hello,World!";
            Console.WriteLine(greet());// Hello,World!

        }

        public override string ToString() {
            return "  Hello,World!   ".Trim();
        }
    }
}
