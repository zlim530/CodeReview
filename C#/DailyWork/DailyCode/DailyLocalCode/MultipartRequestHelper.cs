using System;
using System.Net.Http.Headers;

namespace DailyLocalCode
{
    public static class MultipartRequestHelper
    {
        //public static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
        //{
        //    var boundary = Microsoft.Net.Http.Headers.HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;
        //}
    }

    public class MyConsole
    {
        protected char[] CoreNewLine = new char[2] {'\r','\n' };

        public void Write(char value)
        { 
            
        }

        public void WriteLine()
        {
            Console.WriteLine(CoreNewLine);
        }

        public void WriteLine(string value)
        {
            if (value == null)
            {
                WriteLine();
                return;
            }
            int length = value.Length;
            int num = CoreNewLine.Length;
            char[] array = new char[length + num];
            value.CopyTo(0, array, 0, length);
            switch (num)
            {
                case 2:
                    array[length] = CoreNewLine[0];
                    array[length + 1] = CoreNewLine[1];
                    break;
                case 1:
                    array[length] = CoreNewLine[0];
                    break;
                default:
                    Buffer.BlockCopy(CoreNewLine, 0, array, length * 2, num * 2);
                    break;
            }

        }

    }

    delegate void Del(string str);

    public static class DelTest
    {
        static void Main0(string[] args)
        {
            Del del = name => { Console.WriteLine($"Notification received for:{name}"); };
            del("zlim530");
        }
    }

}
