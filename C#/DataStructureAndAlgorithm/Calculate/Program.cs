using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


// leetcode:224.基本计算器
// https://leetcode-cn.com/problems/basic-calculator/
namespace Calculate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string s1 = " 2-1 + 2 ";
            string s = "2147483647";
            var result = calculate(s);
            System.Console.WriteLine(result);
        }

        static int calculate(String s) {

            Stack<int> stack = new Stack<int>();
            int operand = 0;
            int result = 0; // For the on-going result
            int sign = 1;  // 1 means positive, -1 means negative 1表示加法 -1表示减法
            string regularExpressions = "[0-9]";
            Regex rg = new Regex(regularExpressions);
            int len = s.Length;

            for (int i = 0; i < len; i++) {

                // char ch = s.charAt(i);
                // char ch = s.ToCharArray(i);
                if ( rg.IsMatch( s[i].ToString()) ) {

                    // Forming operand, since it could be more than one digit
                    // 操作数可以是多位的
                    int nums = Convert.ToInt32(s[i].ToString(),10);
                    operand = 10 * operand + nums;

                } else if (s[i] == '+') {

                    // Evaluate the expression to the left,
                    // with result, sign, operand
                    result += sign * operand;// result = restult + sign * operand;

                    // Save the recently encountered '+' sign
                    sign = 1;

                    // Reset operand
                    operand = 0;

                } else if (s[i] == '-') {

                    result += sign * operand;
                    sign = -1;
                    operand = 0;

                } else if (s[i] == '(') {

                    // Push the result and sign on to the stack, for later
                    // We push the result first, then sign
                    stack.Push(result);
                    stack.Push(sign);

                    // Reset operand and result, as if new evaluation begins for the new sub-expression
                    sign = 1;
                    result = 0;

                } else if (s[i] == ')') {

                    // Evaluate the expression to the left
                    // with result, sign and operand
                    result += sign * operand;

                    // ')' marks end of expression within a set of parenthesis
                    // Its result is multiplied with sign on top of stack
                    // as stack.pop() is the sign before the parenthesis
                    result *= stack.Pop();

                    // Then add to the next operand on the top.
                    // as stack.pop() is the result calculated before this parenthesis
                    // (operand on stack) + (sign on stack * (result from parenthesis))
                    result += stack.Pop();

                    // Reset the operand
                    operand = 0;
                }
            }
            return result + (sign * operand);
    }

        // static int Calculate(string s)
        // {
        //     Stack<int> data = new Stack<int>();
        //     Stack<char> op = new Stack<char>();
        //     string regularExpressions = "[0-9]";
        //     Regex rg = new Regex(regularExpressions);
        //     int len = s.Length;
        //     int result = 0;
        //     int operand = 0;
        //     for (int i = 0; i < len; i++)
        //     {
        //         if ( s[i] == '(')
        //         {
        //             op.Push(s[i]);
        //         }else if ( s[i] == '+' || s[i] == '-' || s[i] == ')')
        //         {
        //             while ( op.Count != 0 && ( op.Peek() == '+' || op.Peek() == '-' ) )
        //             {
        //                 int first = data.Pop();
        //                 int sed = data.Pop();
        //                 char ops = op.Pop();
        //                 if ( ops == '+')
        //                 {
        //                     result = sed + first;
        //                     data.Push(result);
        //                 } else
        //                 {
        //                     result = sed - first;
        //                     data.Push(result);
        //                 }
        //             }
        //             if ( s[i] == ')')
        //             {
        //                 if ( op.Count != 0 && op.Peek() == '(')
        //                 {
        //                     op.Pop();
        //                 }
        //             } else
        //             {
        //                 op.Push(s[i]);
        //             }
        //         } else if ( s[i] == ' ') 
        //         {
        //             continue;
        //         } else
        //         {
        //             data.Push(Convert.ToInt32(s[i].ToString(),10));
        //             // int nums = Convert.ToInt32(s[i].ToString(),10);
        //             // operand = 10 * operand + nums;
        //             // data.Push(operand);
        //         }
        //     }
        //     while ( op.Count != 0  )
        //     {
        //         int first = data.Pop();
        //         int sed = data.Pop();
        //         char ops = op.Pop();
        //         if ( ops == '+')
        //         {
        //             result = sed + first;
        //             data.Push(result);
        //         } else
        //         {
        //             result = sed - first;
        //             data.Push(result);
        //         }
        //     }
        //     return data.Pop();
        // }

        // static int CalculateX(string s)
        // {
        //     Stack<int> data = new Stack<int>();
        //     Stack<char> op = new Stack<char>();
        //     string regularExpressions = "[0-9]";
        //     Regex rg = new Regex(regularExpressions);
        //     int len = s.Length;
        //     foreach (var strings in s)
        //     {
        //         if ( rg.IsMatch(strings.ToString()) )
        //         {
        //             data.Push(Convert.ToInt32(strings.ToString(),10));
        //         } else if ( strings != ' ')
        //         {
        //             if ( op.Count == 0)
        //             {
        //                 op.Push(strings);
        //             } else
        //             {
        //                 if ( strings == '(')
        //                 {
        //                     op.Push(strings);
        //                 } else if ( strings != ')')
        //                 {
        //                     if ( data.Count != 0)
        //                     {
        //                         calculate(op,data);
        //                         op.Push(strings);
        //                     }
                            
        //                 } else
        //                 {
        //                     if ( data.Count != 0)
        //                     {
        //                         calculate(op,data);
        //                         op.Push(strings);
        //                     }
        //                 }
        //             }
        //         } else
        //         {
        //             continue;
        //         }
        //     }
        //     calculate(op,data);
        //     return data.Pop();

        // }

        // static void calculate(Stack<char> op,Stack<int> data)
        // {
        //     while ( op.Count != 0 && (op.Peek() == '+' || op.Peek() == '-') )
        //     {
        //         int first = data.Pop();
        //         int sed = data.Pop();
        //         int result;
        //         char s = op.Pop();
        //         if ( s == '+' )
        //         {
        //             result = sed + first;
        //             data.Push(result);
        //         } else
        //         {
        //             result = sed - first;
        //             data.Push(result);
        //         }
        //     }
        // }

    }

    // public class Solution {
    // private int i = -1; // 指针
    // private string s;
    // public int Calculate(string s) {
    //     this.s = s;
    //     return Parse();
    // }
    // public int Parse() {
    //     int ans = 0; // 计算结果
    //     bool plus = true; // true:加号, false:减号
    //     while (i < s.Length - 1) {
    //         i++;
    //         switch (s[i]) {
    //             case ' ': // 忽略空格
    //                 continue;
    //             case '+':
    //                 plus = true;
    //                 continue;
    //             case '-':
    //                 plus = false;
    //                 continue;
    //             case '(': // 遇到左括号表明有新的优先计算区域
    //                 if (plus) ans += Parse(); // 这里可以直接忽略括号，不过因为需要改右括号的处理，所以懒得优化
    //                 else ans -= Parse();
    //                 continue;
    //             case ')': // 遇到没有处理的右括号表明优先区域结束
    //                 return ans;
    //             default: // 遇到数字
    //                 if (plus) ans += ParseInt();
    //                 else ans -= ParseInt();
    //                 continue;
    //         }
    //     }
    //     return ans;
    // }
    // public int ParseInt() {
    //     int ans = 0;
    //     for (; i < s.Length && char.IsDigit(s[i]); i++) {
    //         ans *= 10;
    //         ans += s[i] - '0';
    //     }
    //     i--;
    //     return ans;
    // }
}
