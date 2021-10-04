using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterviewProgram {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            Console.WriteLine(Reverse("I love China"));
            var array = SplitIntegeArray(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8};
            Console.WriteLine(BinarySearch(arr, 4));
        }

        #region 请写出一个方法，传入一个string， 返回倒叙输出的句子，其中的每个单词本身并不会翻转， 比如 "I love China" ->“China love I”
        /*
        请写出一个方法，传入一个string， 返回倒叙输出的句子，其中的每个单词本身并不会翻转， 比如 "I love China" ->“China love I”
        */
        public static string Reverse(string str) {
            if (string.IsNullOrEmpty(str)) {
                return "";
            }
            
            var arr = str.Split(" ");
            var length = arr.Length;
            var temp = new string[length];
            var sb = new StringBuilder();
            var mid = length / 2;
            for (int i = 0; i < length; i++) {
                if (i == mid) {
                    temp[i] = arr[i];
                } else {
                    temp[i] = arr[arr.Length - 1 - i];
                }
                sb.Append(temp[i]);

                if (i != length - 1) {
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
        public static int[][]  SplitIntegeArray(int[] arr) {
            if (arr == null || arr.Length <= 0) {
                throw new Exception("Input Array is null!");
            }
            List<List<int>> list = new List<List<int>>();
            List<int> temp = new List<int>();
            var length = arr.Length;

            for (int i = 0; i < length; i++) {
                if (i % 3 == 0) {
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
        public static int BinarySearch(int[] arr,int v) {
            if (arr == null || arr.Length == 0) {
                return -1;
            }
            var begin = 0;
            var end = arr.Length;
            while (begin < end) {
                int mid = (begin + end) >> 1;
                if (v < arr[mid]) {
                    end = mid;
                } else if (v > arr[mid]) {
                    begin = mid + 1;
                } else {
                    return mid;
                }
            }
         
            return -1;
        }
        #endregion

    }
}
