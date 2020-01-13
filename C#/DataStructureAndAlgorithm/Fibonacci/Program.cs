using System;

namespace Fibonacci
{
    class Program
    {
        // n        : 1 2 3 4 5 6 7 8  ...
        // Fibonacci: 0 1 1 2 3 5 8 13 ... 
        static void Main(string[] args)
        {
            // Console.WriteLine(Fibonacci1(24));
            // Console.WriteLine(Fibonacci2(64));
            Console.WriteLine(Fibonacci3(3));
            
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

        static int Fibonacci3(int n)
        {
            if (n <= 1)
            {
                return n;
            }
            int first = 0;
            int second = 1;
            while ( n-- > 1)
            {
                second += first;
                first = second - first;
            }
            return second;
            
        }

    }
}
