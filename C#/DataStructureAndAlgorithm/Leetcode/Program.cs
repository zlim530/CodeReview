using System;
using System.Collections.Generic;

namespace Leetcode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string S = "(()(()))";
            System.Console.WriteLine(ScoreOfParentheses(S));

        }

        public static int ScoreOfParentheses(string S)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(0);
            int len = S.Length;
            int value;
            int width;
            for (int i = 0; i < len; i++)
            {
                if ( S[i] == '(')
                {
                    stack.Push(0);
                }else
                {
                    value = stack.Pop();
                    width = stack.Pop();
                    stack.Push(width + Math.Max(value * 2,1));
                }
            }
            return stack.Pop();

        }
    }

    // public class Solution 
    // {
    // public int ScoreOfParentheses(string S) 
    // {
    //     if (string.IsNullOrEmpty(S))
    //         return 0;

    //     Stack<IndexOrValue> stack = new Stack<IndexOrValue>();

    //     for (int i = 0; i < S.Length; ++i)
    //     {
    //         if (S[i] == '(')
    //             stack.Push(new IndexOrValue(i, true));
    //         else
    //         {
    //             IndexOrValue iv = null;
    //             if (stack.Peek().isIndex)
    //             {
    //                 stack.Pop();
    //                 iv = new IndexOrValue(1, false);
    //             }
    //             else
    //             {
    //                 iv = new IndexOrValue(stack.Pop().val * 2, false);
    //                 stack.Pop();
    //             }

    //             if (stack.Any() && !stack.Peek().isIndex)
    //                 stack.Peek().val += iv.val;
    //             else
    //                 stack.Push(iv);
    //         }
    //     }

    //     if (stack.Count != 1)
    //         return -1;
    //     else
    //         return stack.Peek().val;
    //     }
    // }

    // public class IndexOrValue
    // {
    //     public IndexOrValue(int val, bool isIndex)
    //     {
    //         this.val = val;
    //         this.isIndex = isIndex;
    //     }

    //     public int val;
    //     public bool isIndex;
    // }
}
