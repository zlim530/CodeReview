using System;
using System.Collections.Generic;
using System.Text;

// LeetCode：20.有效的括号
// https://leetcode-cn.com/problems/valid-parentheses/
namespace Stack
{
    class Program
    {
        static void Main1(string[] args)
        {
            Console.WriteLine("Hello World!");
            Stack<int> stack = new Stack<int>();
            stack.Push(11);
            stack.Push(22);
            stack.Push(33);
            stack.Push(44);

            while ( !stack.IsEmpty() )
            {
                System.Console.WriteLine(stack.Pop());
            }

        }

        static void Main()
        {
            string s = "()";
            var restult = IsValid(s);
            System.Console.WriteLine(restult);
        }

        // 使用字典与栈这种数据结构来实现
        static bool IsValidVersion2(string s)
        {
            System.Collections.Generic.Stack<char> stack = new System.Collections.Generic.Stack<char>();
            Dictionary<char,char> dic = new Dictionary<char,char>{
                {')','('}, {'}','{'}, {']','['}
            };

            foreach(char i in s){
                if (!dic.ContainsKey(i))
                    stack.Push(i);
                else if (stack.Count == 0 || dic[i] != stack.Pop())
                    return false;
            }

            return stack.Count == 0;
        }

        static bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            int len = s.Length;
            for (int i = 0; i < len; i++)
            {
                if ( s[i] == '(' || s[i] == '{' || s[i] == '[')
                {
                    stack.Push(s[i]);
                }else // 右括号
                {
                    if ( stack.IsEmpty() )
                    {
                        return false;
                    }
                    char left = stack.Pop();
                    if ( left == '(' && s[i] != ')')
                    {
                        return false;
                    }
                    if ( left == '{' && s[i] != '}')
                    {
                        return false;
                    }
                    if ( left == '[' && s[i] != ']')
                    {
                        return false;
                    }
                }
            }
            // if ( stack.IsEmpty() )
            // {
            //     return true;
            // }else
            // {
            //     return false;
            // }
            return stack.IsEmpty();

        }


    }
}
