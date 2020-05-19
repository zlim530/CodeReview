using System;
using System.Collections;

namespace InterfaceOfContract
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums1 = new int[] { 1,2,3,4,5};
            ArrayList nums2 = new ArrayList { 1,2,3,4,5};


            // 此时不管是int[] 还是 ArrayList均可以成功调用Sum与Avg函数：这就是接口的作用
            Console.WriteLine(Sum(nums1));
            Console.WriteLine(Avg(nums1));
            Console.WriteLine(Sum(nums2));
            Console.WriteLine(Avg(nums2));

        }

        // 分析可知：nums1 与 nums2为供方 sum与avg为需方 需方只需要实现了可迭代的供方
        // 而供方不管是int[]型的数组还是Arraylist型的object 两者均实现了IEnumerable
        // 故对代码进行重构：使用接口进行供需双方的解耦合

        static int Sum(IEnumerable nums)
        {
            int sum = 0;
            foreach (var n in nums)
            {
                sum += (int)n;
            }
            return sum;
        }

        static double Avg(IEnumerable nums)
        {
            int sum = 0;
            double count = 0;
            foreach (var n in nums)
            {
                sum += (int)n;
                count++;
            }
            return sum / count;
        }

        // 此时Sum与Avg仅支持整数型的数组作为参数 如果想要计算ArrayList类型的总值与平均值需要另外再书写两个函数
        //    static int Sum(int[] nums)
        //    {
        //        int sum = 0;
        //        foreach (var n in nums)
        //        {
        //            sum += n;
        //        }
        //        return sum;
        //    }

        //    static double Avg(int[] nums)
        //    {
        //        int sum = 0;
        //        double count = 0;
        //        foreach (var n in nums)
        //        {
        //            sum += n;
        //            count++;
        //        }
        //        return sum / count;
        //    }

        //    static int Sum(ArrayList nums)
        //    {
        //        int sum = 0;
        //        foreach (var n in nums)
        //        {
        //            sum += (int)n;
        //        }
        //        return sum;
        //    }

        //    static double Avg(ArrayList nums)
        //    {
        //        int sum = 0;
        //        double count = 0;
        //        foreach (var n in nums)
        //        {
        //            // 此时n为object类型 需要进行类型强转
        //            sum += (int)n;
        //            count++;
        //        }
        //        return sum / count;
        //    }
    }
}
