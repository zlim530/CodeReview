using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new int[] { 4,3,2,1,5,6,7};
            Console.WriteLine("初始状态：");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
            Program.BubbleSort(a);
            Console.WriteLine("============");
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }

        public static void BubbleSort(int[] a)
        {
            int len = a.Length - 1;
            int lastPostion = 0;
            for (int i = 0; i < a.Length-1; i++)
            {
                var flag = true;
                for (int j = 0; j < len; j++)
                {
                    if ( a[j] > a[j+1])
                    {
                        int temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;
                        flag = false;
                        lastPostion = j;
                        Console.WriteLine("发生交换：");
                        Console.WriteLine("==========in inner for=======");
                        foreach (var item in a)
                        {
                            Console.WriteLine(item);
                        }
                        
                    }
                    Console.WriteLine("你在浪费计算吗？");
                }
                len = lastPostion;
                if (flag) break;
                Console.WriteLine("第{0}次遍历：",i+1);
                Console.WriteLine("=========in outter for===========");
                foreach (var item in a)
                {
                    Console.WriteLine(item);
                }
                
            }
        }
    }
}
