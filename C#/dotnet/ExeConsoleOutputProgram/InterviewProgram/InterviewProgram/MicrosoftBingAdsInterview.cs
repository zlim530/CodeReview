using System;
using System.Linq;

/**
 * @author zlim
 * @create 2021/11/30 19:04:26
 */
namespace InterviewProgram
{
    public static class MicrosoftBingAdsInterview
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
            Console.WriteLine(CountOneNumbers(7));
        }

        /// <summary>
        /// 将字符串转换为整数：考察边界条件的处理
        /// 1. 当字符串中有非法字符时如何处理？
        /// 2. 当字符串超过 int 整型最大值如何处理？
        /// 3. 当字符串为负数如何处理？
        /// ...
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 使用 LINQ 将数组中的偶数取出来并从大到小进行排序
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 判断一个整数的二进制数中有几个1
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int CountOneNumbers(int a)
        {
            if (a < 0)
            {
                return -1;
            }

            var result = Convert.ToString(a,2);
            var count = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '1')
                {
                    count++;
                }
            }
            return count;


            /*
            *   1 => 001
            *   2 => 010
            *   3 => 011
            *   4 => 100 
            *   4 & 3 =   100
            *           & 011
            *           ------
            *             000
            *   3 & 2 =   011
            *           & 010
            *           ------
            *             010
            */
            //var count = 0;
            //while (a != 0)
            //{
            //    a &= (a - 1);
            //    count++;
            //}

            //return count;
        }
    }

    #region 单列模式的实现
    public class SingletonClass
    {
        private static readonly object _locker = new object();

        private static volatile SingletonClass instance;
        private SingletonClass()
        {

        }


        public static SingletonClass GetSingleInstance()
        {
            if (instance == null)
            {
                lock (_locker)
                {
                    if (instance == null)
                    {
                        instance = new SingletonClass();
                    }
                }
            }

            return instance;
        }

    }
    #endregion

}
