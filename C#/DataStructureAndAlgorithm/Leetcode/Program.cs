using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Leetcode
{
    class Program
    {
        static void Main()
        {
            MyStack obj = new MyStack();
            obj.Push(4);
            obj.Push(3);
            int param_1 = obj.Pop();
            int param_2 = obj.Top();
            bool param_3 = obj.Empty();
            System.Console.WriteLine(param_1);
            System.Console.WriteLine(param_2);
            System.Console.WriteLine(param_3);
        }


        static void Main3()
        {
            System.Console.WriteLine("hello world!");
            string[] tokens = {"10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+"};
            var restult = EvalRPN(tokens);
            System.Console.WriteLine(restult);
            
        }

        // LeetCode：150.逆波兰表达式求值
        // https://leetcode-cn.com/problems/evaluate-reverse-polish-notation/
        static int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();
            string regularExpression = "[0-9]";
            Regex rg = new Regex(regularExpression);
            int restult;
            int len = tokens.Length;
            for (int i = 0; i < len; i++)
            {
                if ( rg.IsMatch(tokens[i].ToString()) )
                {
                    stack.Push(Convert.ToInt32(tokens[i].ToString(),10));
                }else
                {
                    int first = stack.Pop();
                    int sed = stack.Pop();
                    if ( tokens[i] == "+")
                    {
                        restult = sed + first;
                        stack.Push(restult);
                    } else if ( tokens[i] == "-")
                    {
                        restult = sed - first;
                        stack.Push(restult);
                    } else if (tokens[i] == "*")
                    {
                        restult = sed * first;
                        stack.Push(restult);
                    } else
                    {
                        restult = sed / first;
                        stack.Push(restult);
                    }
                }
            }
            return stack.Pop();
        }
//     public class Solution {
//     public int EvalRPN(string[] tokens) {
//         Stack<int> stack = new Stack<int>();
        
//         int result = 0;
//         foreach (string s in tokens) {
//             if (s == "+" || s == "-" || s == "*" || s == "/") {
//                 int second = stack.Pop();
//                 int first = stack.Pop();
                
//                 if (s == "+") {
//                     result = first + second;
//                 }
//                 else if (s == "-") {
//                     result = first - second;
//                 }
//                 else if (s == "*") {
//                     result = first * second;
//                 }
//                 else {
//                     result = first / second;
//                 }
                
//                 stack.Push(result);
//             }
//             else {
//                 stack.Push(int.Parse(s));
//                 result = stack.Peek();
//             }
//         }
        
//         return result;
//     }
// }

        static void Main2()
        {
            MyQueue queue = new MyQueue();
            queue.Push(1);
            queue.Push(2);
            System.Console.WriteLine(queue.Peek());
            System.Console.WriteLine(queue.Pop());
            // queue.Peek();
            // queue.Pop();
            // queue.Empty();
            System.Console.WriteLine(queue.Empty());
        }


        static void Main1(string[] args)
        {
            Console.WriteLine("Hello World!");
            string S = "(()(()))";
            System.Console.WriteLine(ScoreOfParentheses(S));

        }

        // LeetCode：856.括号的分数
        // https://leetcode-cn.com/problems/score-of-parentheses/
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
