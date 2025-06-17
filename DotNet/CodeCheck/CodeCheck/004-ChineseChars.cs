using System.Text;
using static ChineseChars.Helpers;

namespace CodeCheck
{
    internal class ChineseChars
    {
        static void Main(string[] args)
        {
            // CharSet(字符集，字符用什么数值表示，比如UTF-8,UTF-32字符集，其中UTF-8是可变长度字符集，也即不同字符的字节长度是不固定的，而UTF-32则是不管什么字符均采用4个字节来存储，缺点浪费空间，优点解码迅速）
            // Encoding（编码，字符要怎么存，存成几字节）
            // char（字符） byte（字节）
            Console.WriteLine("a");
            PrintAsBinary("a".ToCharArray()); //1 char
            PrintAsBinary(Encoding.UTF8.GetBytes("a")); // 1 byte
            // 01100001,（开头为0表示单字节字符）
            PrintAsBinary(Encoding.UTF32.GetBytes("a")); // 1 byte

            Console.WriteLine("我");
            PrintAsBinary("我".ToCharArray()); // 1 char
            PrintAsBinary(Encoding.UTF8.GetBytes("我")); // 3 bytes
            //11100110,10001000,10010001,（开头为1表示多字节字符，1后面有几个就表示后面还有几个字节）
            PrintAsBinary(Encoding.UTF32.GetBytes("我")); // 1 byte

            Console.WriteLine("😄");
            PrintAsBinary("😄".ToCharArray()); // 2 char
            PrintAsBinary(Encoding.UTF8.GetBytes("😄")); // 4 bytes
            //11110000,10011111,10011000,10000100,
            PrintAsBinary(Encoding.UTF32.GetBytes("😄")); // 1 byte
            // Test
        }
    }
}

namespace ChineseChars
{
    public class Helpers
    {
        public static void PrintAsBinary(char[] charArray)
        {
            foreach (var ch in charArray)
            {
                Console.Write(Convert.ToString(ch,2).PadLeft(8,'0'));
                Console.Write(",");
            }
            Console.WriteLine();
        }

        public static void PrintAsBinary(byte[] byteArray)
        {
            foreach (var b in byteArray)
            {
                Console.Write(Convert.ToString(b, 2).PadLeft(8, '0'));
                Console.Write(",");
            }
            Console.WriteLine();
        }

        public static void PrintasHex(byte[] byteArray) 
        {
            foreach (var b in byteArray)
            {
                Console.Write(Convert.ToString(b, 16).PadLeft(8, '0'));
                Console.Write(",");
            }
            Console.WriteLine();
        }
    }
}
