using System;

namespace LINQAndXML
{
    public class DailyTest
    {
        static void Main(string[] args)
        {
            string str = "a -b+c";
            string[] s = str.Split(new char[] { '-', '+' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(s[0]);// a
            Console.WriteLine(s[1]);// b
            Console.WriteLine(s[2]);// c
            Console.WriteLine(str[1]);
            Console.WriteLine(str[2]);// -

        }
    }
}
