using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCheck
{
    internal class _002_AboutMainFunction
    {
        /// <summary>
        /// Main 方法是 C# 应用程序的入口点。 （库和服务不要求使用 Main 方法作为入口点）。Main 方法是应用程序启动后调用的第一个方法。
        /// 自 C# 9 起，可以省略 Main 方法，并像在 Main 方法中一样编写 C# 语句：所谓顶级语句
        /// Main 在类或结构中声明。 Main 必须是 static，它不需要是 public。 （在前面的示例中，它获得的是private成员的默认访问权限）。封闭类或结构不一定要是静态的。
        /// Main 的返回类型可以是 void、int、Task 或 Task<int>。
        /// 当且仅当 Main 返回 Task 或 Task<int> 时，Main 的声明可包括 async 修饰符。 这明确排除了 async void Main 方法。
        /// </summary>
        /// <param name="args"></param>
        static void Main01(string[] args)
        {
            // args 数组不能为 null。 因此，无需进行 null 检查即可放心地访问 Length 属性。
            // Display the number of command line arguments.
            Console.WriteLine(args.Length);
            // 数组中的第一个元素包含执行程序的文件名。 如果文件名不可用，则第一个元素等于 String.Empty。 其余元素包含命令行上输入的任何其他令牌。
            // C:\code\CodeReview\DotNet\CodeCheck\CodeCheck\bin\Debug\net6.0\CodeCheck.dll
            Console.WriteLine((Environment.GetCommandLineArgs())[0]);
        }


        static int Main(string[] args)
        {
            // Test if input arguments were supplied.
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter a numeric argument.");
                Console.WriteLine("Usage: Factorial <num>");
                return 1;
            }

            // Try to convert the input arguments to numbers. This will throw
            // an exception if the argument is not a number.
            // num = int.Parse(args[0]);
            int num;
            bool test = int.TryParse(args[0], out num);
            if (!test)
            {
                Console.WriteLine("Please enter a numeric argument.");
                Console.WriteLine("Usage: Factorial <num>");
                return 1;
            }

            // Calculate factorial.
            long result = Functions.Factorial(num);

            // Print result.
            if (result == -1)
                Console.WriteLine("Input must be >= 0 and <= 20.");
            else
                Console.WriteLine($"The Factorial of {num} is {result}.");

            return 0;
        }
    }

    public class Functions
    {
        public static long Factorial(int n)
        {
            // Test for invalid input.
            if ((n < 0) || (n > 20))
            {
                return -1;
            }

            // Calculate the factorial iteratively rather than recursively.
            long tempResult = 1;
            for (int i = 1; i <= n; i++)
            {
                tempResult *= i;
            }
            return tempResult;
        }
    }
}
