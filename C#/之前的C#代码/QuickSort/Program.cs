using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 5,1,9,3,7,4,8,6,2};
            Console.WriteLine("初始状态：");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
            //var pivot = Program.Partition(arr,0,arr.Length-1);
            //Program.QuickSort(arr,0,arr.Length-1);
            Program.QuickSortPro(arr,0,arr.Length-1);
            Console.WriteLine("=========================人工分界线===============");
            //Console.WriteLine(pivot);
        }

        public static void QuickSort(int[] arr,int low ,int hight)
        {
            int pivotKey;
            if ( low < hight)
            {
                //pivotKey = Program.Partition(arr, low, hight);
                pivotKey = Program.PartitionPro(arr,low,hight);
                Console.WriteLine("算出枢（shu：门的轴）值{0},与对应的下标{1}",arr[pivotKey],pivotKey);
                Program.QuickSort(arr, low, pivotKey - 1);
                Program.QuickSort(arr, pivotKey+1, hight);
            }
        }

        public static void QuickSortPro(int[] arr, int low, int hight)
        {
            int pivotKey;
            while (low < hight)
            {
                pivotKey = Program.PartitionPro(arr,low,hight);
                Console.WriteLine("算出枢（shu：门的轴）值{0},与对应的下标{1}", arr[pivotKey], pivotKey);
                QuickSortPro(arr,low,pivotKey-1);
                low = pivotKey + 1;
            }
        }


        public static int Partition(int[] arr, int low, int hight)
        {
            int pivotKey = arr[low];
            while (low < hight)
            {
                while (low<hight && arr[hight] >= pivotKey)
                {
                    hight--;
                }
                if (low < hight && arr[hight] < pivotKey)
                {
                    swap(arr, low, hight);
                }
                while (low < hight && arr[low] <= pivotKey)
                {
                    low++;
                }
                if (low < hight && arr[low] > pivotKey)
                {
                    swap(arr,low,hight);
                }

            }
            // 将基准值的下标返回
            return low;
        }

        public static int PartitionPro(int[] arr, int low, int hight)
        {
            var pivot = arr[low];
            var pivotKey = low;
            while (low < hight)
            {
                while ( low < hight && arr[hight] >= pivot)
                {
                    hight--;
                }
                while (low < hight && arr[low] <= pivot)
                {
                    low++;
                }
                if ( low < hight && arr[low] > pivot)
                {
                    swap(arr,low,hight);
                }

            }
            swap(arr, pivotKey, low);
            return low;
        }

        private static void swap(int[] arr, int low, int hight)
        {
            if ( low == hight)
            {
                return;
            }
            int temp = arr[low];
            arr[low] = arr[hight];
            arr[hight] = temp;
            Console.WriteLine("发生交换，交换后：");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }

        }
    }
}
