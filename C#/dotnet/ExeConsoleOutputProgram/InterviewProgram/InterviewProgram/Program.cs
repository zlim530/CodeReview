using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace InterviewProgram
{
    class Program
    {
        static void Main0(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Reverse("I love China"));
            var array = SplitIntegeArray(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Console.WriteLine(BinarySearch(arr, 4));
        }

        #region 请写出一个方法，传入一个string， 返回倒叙输出的句子，其中的每个单词本身并不会翻转， 比如 "I love China" ->“China love I”
        /*
        请写出一个方法，传入一个string， 返回倒叙输出的句子，其中的每个单词本身并不会翻转， 比如 "I love China" ->“China love I”
        */
        public static string Reverse(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            var arr = str.Split(" ");
            var length = arr.Length;
            var temp = new string[length];
            var sb = new StringBuilder();
            var mid = length / 2;
            for (int i = 0; i < length; i++)
            {
                if (i == mid)
                {
                    temp[i] = arr[i];
                }
                else
                {
                    temp[i] = arr[arr.Length - 1 - i];
                }
                sb.Append(temp[i]);

                if (i != length - 1)
                {
                    sb.Append(" ");
                }
            }
            var result = sb.ToString();
            return result;
        }
        #endregion


        #region 将一维数组按照三个一组分为二维数组
        /*
        请写出一个方法，将一个数组中的元素，每三个为一组，输出二维数组 
        {1,2,3,4,5,6,7,8} ->{{1,2,3}, {4,5,6},{7,8}}
        输入一个数组:
        {1,2,3,4,5,6,7}
        输出二维数组：
        {{1,2,3}, {4,5,6},{7}}
        */
        public static int[][] SplitIntegeArray(int[] arr)
        {
            if (arr == null || arr.Length <= 0)
            {
                throw new Exception("Input Array is null!");
            }
            List<List<int>> list = new List<List<int>>();
            List<int> temp = new List<int>();
            var length = arr.Length;

            for (int i = 0; i < length; i++)
            {
                if (i % 3 == 0)
                {
                    list.Add(new List<int>());
                }
                list[i / 3].Add(arr[i]);
            }

            return list.Select(x => x.ToArray()).ToArray();
        }
        #endregion


        #region 数组的二分查找
        /*
        请写一个方法，实现二分查找(递归，循环二选一)，
        传入的数组是非重复的升序数组
        返回下标
        */
        public static int BinarySearch(int[] arr, int v)
        {
            if (arr == null || arr.Length == 0)
            {
                return -1;
            }
            var begin = 0;
            var end = arr.Length;
            while (begin < end)
            {
                int mid = (begin + end) >> 1;
                if (v < arr[mid])
                {
                    end = mid;
                }
                else if (v > arr[mid])
                {
                    begin = mid + 1;
                }
                else
                {
                    return mid;
                }
            }

            return -1;
        }
        #endregion

    }


    public class ArrayListExample
    {
        public static void Main1(string[] args)
        {
            byte[] arr_byte = new byte[100];
            byte[] arr_byte_large = new byte[9999999];

            FillArrayWithRand(arr_byte);
            FillArrayWithRand(arr_byte_large);

            //PrintArray(arr_byte);
            //PrintArray(arr_byte_large);
            //return;

            Stopwatch sw = new Stopwatch();
            Console.Write("读取100位Array从0 - 99位的时间放大一百万倍： ");
            sw.Start();

            ReadArrayMultipleTimes(arr_byte);

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds + " ms.");
            Console.Write("读取999，999位Array从851,863 - 851,962位的时间一百万倍：");
            sw.Start();
            sw.Restart();

            ReadArrayMultipleTimes(arr_byte_large, 851_863);

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + " ms.");

        }

        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private static void FillArrayWithRand(byte[] array) => rngCsp.GetBytes(array);
        private static void PrintArray(byte[] array, int RowCount = 12)
        {
            for (int i = 0; i < array.Length; i++)
            {
                string HexString = Convert.ToString(array[i], 16);
                HexString = "0x" + HexString.PadLeft(2, '0').ToUpper();
                Console.Write(HexString);
                if (i % (RowCount + 1) != 0)
                {
                    Console.Write("\t");
                }
                else
                {
                    Console.Write("\r\n");
                }
            }
        }

        private static void ReadArrayMultipleTimes(byte[] array, int start = 0, int length = 99, int repeat = 1_000_000)
        {
            int end = start + length;
            for (int i = 0; i < repeat; i++)
            {
                for (int j = start; j < end; j++)
                {
                    _ = array[j];
                }
            }
        }

    }


    public class MyDelagate
    {
        static void Main2(string[] args)
        {
            void GoStation(Action do_sth)
            {
                WriteLine("去火车站");
                WriteLine("找到站长");
                do_sth();
                WriteLine("离开火车站");
            }

            GoStation(() => WriteLine("打他一顿"));

            #region StringBuilder

            var strList = new List<string>() { "A1","A2","A3"};
            var str3 = "";
            foreach (var str in strList)
            {
                str3 += str;
            }
            WriteLine(str3);

            var str4 = new StringBuilder();
            foreach (var str in strList)
            {
                str4.Append(str);
                //str4.AppendLine(str);
            }
            WriteLine(str4);

            var str5 = strList.Aggregate((a, b) => a + b);
            WriteLine(str5);
            #endregion

        }
    }


    public class 类型转换
    {
        public static void Main3(string[] args)
        {
            #region 基础类型
            
            string str = "44";

            // 方法一：
            int int1 = Convert.ToInt32(str);
            int int2 = int.Parse(str);

            // 方法二：
            try
            {
                int int3 = Convert.ToInt32(str);
                int int4 = int.Parse(str);
            }
            catch (Exception)
            {
                // 转换失败处理
            }

            // 方法三：推荐使用，代码简洁且可读性高
            if (int.TryParse(str, out _))// if(int.TryParse(str, out int int6))
            {

            }
            else
            {
                // 转换失败处理
            }

            #endregion

            #region 类类型

            Animal animal = new Dog();

            // 方法一：强制类型转换
            var dog1 = (Dog)animal;

            // 方法二：在外面套一个 try-catch
            try
            {
                var dog2 = (Dog)animal;
                // 做自己要做的
            }
            catch (Exception)
            {
                // 转换失败处理
            }

            // 方法三：使用 as 
            var dog3 = animal as Dog;
            if (dog3 != null)
            {
                // 做自己要做的
            }
            else
            {
                // 转换失败处理
            }

            // 方法四：使用 is：推荐使用
            if (animal is Dog) // if(animal is Dog dog4)
            {
                // 做自己要做的
            }
            else
            {
                // 转换失败处理
            }

            #endregion
        }
        public class Animal { public string Name { get; set; }}

        public class Dog : Animal { }

        public class Cat : Animal { }
    }


    public class 充分利用
    {
        public static void Main(string[] args)
        {
            var up = new UPMaster() { Date = new Date() { Like = 2 } };

            // 方法一：
            var like1 = up.Date.Like;

            // 方法二：
            var like2 = 0;
            if (up != null && up.Date != null && up.Date.Like.HasValue)
            {
                like2 = up.Date.Like.Value;
            }

            // 方法三：
            var like3 = up?.Date?.Like ?? 0;

            // 注意：
            // ?? 运算符的优先级高于 ?：
            int? coin1 = 1;
            int? coin2 = 2;
            var like4 = coin2 > coin1 ? coin2 : coin1 ?? 0;

            var like5 = (coin2 > coin1 ? coin2 : coin1) ?? 0;
            WriteLine(like4);
            WriteLine(like5);
        }


        public class UPMaster
        {
            public Date Date { get; set; }
        }

        public class Date
        {
            public int? Like { get; set; }
        }
    }


    public class const和readonly
    {

    }
}
