using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * @author zlim
 * @create 2021/11/30 19:04:26
 */
namespace InterviewProgram
{
    public class MicrosoftBingAdsInterview
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(ConvertToIntByString("123"));
            var array = SortedByEvenNumbers(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            for (int i = 0; i < array.Length - 1; i++)
            {
                Console.WriteLine(array[i]);
            }
            //array.OrderBy(d => d.Score).Skip(10).Take(10);
        }


        public static int ConvertToIntByString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return -1;
            }

            var array = str.ToCharArray();
            int result = 0;


            for (int i = 0; i < array.Length; i++)
            {
                // 123
                result *= 10;
                result += (int)((array[i]) - 48);
            }

            return result;
        }


        public static int[] SortedByEvenNumbers(int[] array)
        {
            if (array.Length == 0)
            {
                return null;
            }

            var count = array.Length;

            var result = array.Where(a => a % 2 == 0).Select(a => a).OrderBy(a => a).ToArray();

            return result;
        }


        public static int CountOneNumbers(int a)
        {
            if (a < 0)
            {
                return -1;
            }

            /*
                 1 1 1 
                |0 0 0
                -------
                 1 1 1 
            */
            var result = a | 0;

            return -1;
        }
    }


    public class SingleClass
    {
        private static readonly object _lock = new object();

        private static SingleClass instance;
        private SingleClass()
        {

        }

        public static SingleClass GetSingleInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    instance = new SingleClass();
                }
            }

            return instance;
        }
    }
}
