using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelctionSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 5,1,9,3,7,4,8,6,2};
            Console.WriteLine("初始状态：在Main函数中");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
            Program.SelectionSort(arr);
            Console.WriteLine("After sorting:");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }

        public static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length-1; i++)
            {
                // 先选择第一个元素为最小值
                int min = i;
                for (int j = i+1; j < arr.Length; j++)
                {
                    if ( arr[min] > arr[j] )
                    {
                        // 每一次遍历数组都会找出最小值
                        min = j;
                    }
                }
                // 每次从i到j遍历完数组后都要让i与min交换，保证每轮遍历出来的最小值在最左
                swap(arr,i,min);
            }
        }

        private static void swap(int[] arr, int i,int j)
        {
            if ( i == j)
            {
                return;
            }
            var temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
            Console.WriteLine("下标{0}和{1}已交换.",i,j);
            Console.WriteLine("交换后结果：在swap函数中：");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }
    }
}
