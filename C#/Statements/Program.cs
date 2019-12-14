using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Statements
{
    class Program
    {
        static void Main()
        {
            Greeting("Mr.C#");
        }

        static void Greeting(string name)
        {
            // 提前return原则
            // 通过提前 return 可以让代码阅读者立刻就鉴别出来程序将在什么情况下 return，同时减少 if else 嵌套，写出更优雅的代码。
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            Console.WriteLine("Hello ,{0}",name);
        }

        static void Main4()
        { 
            int[] intArray = new int[] { 1,2,3,4,5,6,7,8};

            List<int> intList = new List<int> { 1, 2, 3, 4, 5, 6 };
            // 遍历集合的简易方法：foreach语句
            foreach (var current in intList)
            {
                Console.WriteLine(current);
            }
        }

        static void Main3()
        {
            int[] intArray = new int[] { 1,2,3,4,5,6,7,8};
            IEnumerator enumerator = intArray.GetEnumerator();// 指月
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

            // 当迭代器迭代完集合后 会在集合的最后一个元素处 通过Reset方法可以将迭代器重新指向集合的第一个元素 从而又开始一遍新的迭代
            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
        }
        static void Main2()
        {
            // print 9x9 multiplication table
            for (int a = 1; a <= 9; a++)
            {
                for (int b = 1; b <= a; b++)
                {
                    Console.Write("{0}x{1}={2}\t",a,b,a*b);
                }
                Console.WriteLine();
            }


        }
        static void Main1(string[] args)
        {
            int score = 0;
            bool canContinue = true;
            while (canContinue)
            {
                Console.WriteLine("Pls input first number");
                string str1 = Console.ReadLine();
                if (str1 == "q" || str1 == "quit")
                {
                    break;
                }
                int x ;
                int y ;
                try
                {
                     x = int.Parse(str1);
                }
                catch (FormatException f)
                {
                    Console.WriteLine("The first number format is wrong.");
                    continue;
                    throw;
                }
                catch (OverflowException o)
                {
                    Console.WriteLine("The first number is over value.");
                    continue;
                    throw;
                }
                catch (ArgumentNullException a)
                {
                    Console.WriteLine("The first number is CAN'T be null.");
                    continue;
                    throw;
                }

                Console.WriteLine("Pls input second number");
                string str2 = Console.ReadLine();
                if (str2 == "q" || str1 == "quit")
                {
                    break;
                }

                try
                {
                    y = int.Parse(str2);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("The second number format is wrong.");
                    continue;
                    throw;
                }
                catch (OverflowException o)
                {
                    Console.WriteLine("The second number is over value.");
                    continue;
                    throw;
                }
                catch (ArgumentNullException a)
                {
                    Console.WriteLine("The second number is CAN'T be null.");
                    continue;
                    throw;
                }

                int sum = x + y;
                if (sum == 100)
                {
                    score++;
                    Console.WriteLine("Correct! {0} + {1} = {2}", x, y, sum);
                }
                else
                {
                    Console.WriteLine("Error! {0} + {1} = {2}",x,y,sum);
                    canContinue = false;
                }
            }
            Console.WriteLine("Your score is {0}",score);
            Console.WriteLine("GAME OVER.");
        }
    }
}
