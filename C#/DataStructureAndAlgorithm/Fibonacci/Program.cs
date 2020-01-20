using System;

// LeetCode：509.斐波那契数
// https://leetcode-cn.com/problems/fibonacci-number/
namespace Fibonacci
{
    class Program
    {
        // n        : 0 1 2 3 4 5 6 7  8  ...
        // Fibonacci: 0 1 1 2 3 5 8 13 21... 
        static void Main(string[] args)
        {
            // Console.WriteLine(Fibonacci1(24));
            //Console.WriteLine(Fibonacci2(1));
            //Console.WriteLine(Fibonacci3(1));
            Solution s = new Solution();
            Console.WriteLine(s.Fib(3));
            
        }

        // O(2^n)
        static int Fibonacci1(int n)
        {
            if ( n <= 1)
            {
                return n;
            }
            return Fibonacci1(n - 1) + Fibonacci1(n - 2);
        }

        // O(n)
        static int Fibonacci2(int n)
        {
            if ( n <= 1)
            {
                return n;
            }
            int first = 0;
            int second = 1;
            int sum = 0;
            for (int i = 0; i < n - 1; i++)
            {
                sum = first + second;
                first = second;
                second = sum;
            }
            return sum;
        }

        // n        : 1 2 3 4 5 6 7 8  ...
        // Fibonacci: 0 1 1 2 3 5 8 13 ... 
        static int Fibonacci3(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            int first = 0;
            int second = 1;
            // n-- > 1 ：表达式n--的值为n 而n的值为 n -1 
            // n-- > 0：循环n次，而计算第个n数仅需要循环n-1次，故为n-- > 1
            while ( n-- > 1)
            {
                // 非递归算法实现斐波那契数列的实质就是计算前两个数，并将计算结果赋值给新的第二个数，而第一个数为前两个数中的第二个数
			    // 也即交换两个数：second = second + first ; first = second - first; 
                second += first;
                first = second - first;
            }
            return second;
 
        }

    }

    public class Solution
    {
        int firstNum = 0;
        int secondNum = 1;
        int fibResult = 0;
        public int Fib(int N)
        {
            if (N == 0)
            {
                return 0;
            }
            else if (N == 1)
            {
                return 1;
            }
            fibResult = firstNum + secondNum;
            firstNum = secondNum;
            secondNum = fibResult;
            Fib(N - 1);
            return fibResult;
        }
    }
}
