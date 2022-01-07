using System;
using System.Collections.Generic;

namespace CountNumberPairs
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = { 20, 260, 20, 260, 500, 500};
            var count = Count(nums);
            Console.WriteLine(count);
            var countbyArray = CountByArray(nums, 520, 520);
            Console.WriteLine(countbyArray);
        }

        // 更通用且light的数组实现版本：因为dictionary数据类型太heavy了
        static int CountByArray(int[] nums, int key, int sum)
        {
            if (nums == null) return 0;
            var counter = 0;
            // 将数组的索引当作从1到key的数值：为了索引与1到key一一对应，所以这里数组的大小为key+1
            // 考点：数据的索引是从0开始的
            // 数组此处索引的值当作此数字出现的次数
            var dict = new int[key+1];
            foreach (var n in nums)
            {
                var diff = sum - n;
                if (dict[diff] > 0)
                {
                    counter++;
                    dict[diff]--;
                }
                else
                {
                    dict[n]++;
                }
            }
            
            return counter;
        }   

        static int Count(int[] nums)
        {
            if (nums == null) return 0;
            var counter = 0;
            // 使用字典，key 存储数组中的值，value 存储此值出现的次数
            // 注：数组中的元素是可重复
            var dict = new Dictionary<int, int>();
            foreach (var n in nums)
            {
                var diff = 520 - n;
                if (dict.ContainsKey(diff))
                {
                    counter++;
                    dict[diff]--;
                    if (dict[diff] == 0)
                        dict.Remove(diff);
                }
                else
                {
                    if (!dict.ContainsKey(n))
                        dict[n] = 0;
                    dict[n]++;
                }
            }

            return counter;
        }
        
        
    }
}