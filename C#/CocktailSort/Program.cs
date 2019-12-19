using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailSort
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[] { 5,1,9,3,7,4,8,6,2};
            Console.WriteLine("初始状态：在Main函数中：");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
            //Program.CocktailSort(arr);
            Program.CocktailSortPro(arr);
            Console.WriteLine("After sorting:");
            foreach (var item in arr)
            {
                Console.WriteLine(item);
            }
        }

        public static void CocktailSortPro(int[] arr)
        {
            int low, hight;
            for (int i = 0; i < arr.Length/2; i++)
            {
                low = i;
                hight = arr.Length - i - 1;
                for (int j = i; j < arr.Length-i-1; j++)
                {
                    if ( arr[low] < arr[j+1])
                    {
                        low = j + 1;
                    }
                }
                swap(arr,low,hight);
                Console.WriteLine("从左到右发生交换：在CocktailSort的第二个上面内层for循环中：");
                foreach (var item in arr)
                {
                    Console.WriteLine(item);
                }
                for (int j = arr.Length-i-1; j > i; j--)
                {
                    if ( arr[j-1] < arr[hight])
                    {
                        hight = j - 1;
                    }
                }
                swap(arr,i,hight);
                Console.WriteLine("从右到左发生交换：在CocktailSort的第二个下面内层for循环中");
                foreach (var item in arr)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public static void CocktailSort(int[] arr)
        {
            for (int i = 0; i < arr.Length / 2; i++)
            {
                for (int j = i; j < arr.Length-i-1; j++)
                {
                    if ( arr[j] > arr[j+1])
                    {
                        swap(arr,j,j+1);
                        Console.WriteLine("从左到右发生交换：在CocktailSort的第二个上面内层for循环中：");
                        foreach (var item in arr)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                for (int j = arr.Length-i-2; j > i; j--)
                {
                    if ( arr[j-1] > arr[j])
                    {
                        swap(arr,j-1,j);
                        Console.WriteLine("从右到左发生交换：在CocktailSort的第二个下面内层for循环中");
                        foreach (var item in arr)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
            }
        }

        private static void swap(int[] arr, int j, int v)
        {
            if ( j == v)
            {
                return;
            }
            var temp = arr[j];
            arr[j] = arr[v];
            arr[v] = temp;
        }
    }
}
