using System;
using System.Linq.Expressions;

namespace LINQAndXML
{
    public class DailyTest
    {
        static void Main0(string[] args)
        {
            string str = "a -b+c";
            string[] s = str.Split(new char[] { '-', '+' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(s[0]);// a
            Console.WriteLine(s[1]);// b
            Console.WriteLine(s[2]);// c
            Console.WriteLine(str[1]);
            Console.WriteLine(str[2]);// -

        }

        static void Main(String[] args)
        {
            //int y = 10;
            //int x = y == 0 ? 1 : y;
            Func<int, int> del = x => x + 1;
            Expression<Func<int, int>> exp = x => x + 1;// 表达式目录树 exp 引用描述表达式 x => x + 1 的数据结构
            var del2 = exp.Compile();
            Console.WriteLine(del(1));
            Console.WriteLine(del2(1));
        }
    }
}
