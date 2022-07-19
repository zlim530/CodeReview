using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTest
{
    public class LINQRelated
    {
        /// <summary>
        /// 学习 LINQ：让数据处理变得简单：
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args)
        {
            /*
            统计一个字符串中每个字母出现的频率（忽略大小写），然后按照从高到低的顺序输出出现频率高于2次的单词和其出现的频率（次数）
            */
            var s = "hello LINQ!";
            var items = s.Where(c => char.IsLetter(c))  // 过滤非字母
                            .Select(c => char.ToLower(c))   // 大写字母转换为小写
                            .GroupBy(c => c)// 根据字母进行分组
                            .Where(g => g.Count() > 2)// 过滤掉出现次数 <=2
                            .OrderByDescending(g => g.Count())// 按照次数排序
                            .Select(g => new { Char = g.Key, Count = g.Count()});
            foreach (var item in items)
            {
                Console.WriteLine("item.Char:" + item.Char + " item.Count:" + item.Count);
            }
        }

        /// <summary>
        /// Where 的简单使用
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args)
        {
            int[] nums = { 1, 2, 3, 4, 5, 6, 35, 111};
            //Where 方法会遍历集合中每个元素，对于每个元素
            //都调用 a => a>10 这个表达式判断一下是否为 true
            //如果为 true,则把这个放到返回的集合中，原集合不变
            //IEnumerable<int> items = nums.Where(a => a > 10);
            //IEnumerable<int> items = MyWhere(nums, a => a > 10);
            IEnumerable<int> items = MyWhereYield(nums, a => a > 10);
            foreach (var i in items)
            {
                Console.WriteLine(i);
            }
        }

        static void Main(string[] args)
        {
            List<Employee> list = new List<Employee>();
            list.Add(new Employee { Id = 1, Name = "jerry", Age = 28, Gender = false, Salary = 5000});
            list.Add(new Employee { Id = 2, Name = "jim", Age = 33, Gender = false, Salary = 3000});
            list.Add(new Employee { Id = 3, Name = "lily", Age = 35, Gender = true, Salary = 9000});
            list.Add(new Employee { Id = 4, Name = "lucy", Age = 16, Gender = true, Salary = 2000});
            list.Add(new Employee { Id = 5, Name = "kimi", Age = 25, Gender = false, Salary = 1000});
            list.Add(new Employee { Id = 6, Name = "nancy", Age = 35, Gender = true, Salary = 8000});
            list.Add(new Employee { Id = 7, Name = "zack", Age = 35, Gender = false, Salary = 8500});
            list.Add(new Employee { Id = 8, Name = "jack", Age = 35, Gender = false, Salary = 8000});

            // Any() 方法：是否至少有一条数据：有可能比 Count() 实现效率高：因为只要有一条满足则返回不需要遍历全部结果获得满足的 Count 值
            bool b1 = list.Any(e => e.Salary > 8000);
            bool b2 = list.Where(e => e.Salary > 8000).Any();

            // 获取一条数据（是否带参数的两种写法）
            // Single：有且只有一条满足要求的数据：大于一条会报错，没有也会报错
            //list.Single();//System.InvalidOperationException:“Sequence contains more than one element”
            list.Single(s => s.Id == 1);
            // SingleOrDefault：最多只有一条满足要求的数据：大于一条会报错，没有则返回默认值
            //list.SingleOrDefault();//System.InvalidOperationException:“Sequence contains more than one element”
            list.SingleOrDefault(s => s.Name == "tom");

            //First：至少有一条，返回第一条，大于一条不报错，小于一条报错
            list.First();
            //list.First(s => s.Name == "tom");// System.InvalidOperationException:“Sequence contains no matching element”
            // FirstOrDefalut：返回第一条数据，如果没有数据则返回默认值
            list.FirstOrDefault(f => f.Id == 90);
            // 选择合适的方法，“防御性编程”：不要害怕报错、抛出异常，因为这样可以让我们清楚知道程序在哪一步发生了问题，越早期发现问题越好，这样可以避免错误的数据在后续的程序中继续运行

        }

        /// <summary>
        /// Where 的简单实现
        /// </summary>
        /// <param name="items"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        static IEnumerable<int> MyWhere(IEnumerable<int> items, Func<int, bool> f)
        {
            List<int> result = new List<int>();
            foreach (var item in items)
            {
                if (f(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        /// <summary>
        /// 使用 yield 简单实现 Where
        /// </summary>
        /// <param name="items"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        static IEnumerable<int> MyWhereYield(IEnumerable<int> items, Func<int, bool> f)
        {
            foreach (var item in items)
            {
                if (f(item))
                {
                    Console.WriteLine($"In MyWhereYield:{item}");
                    yield return item;
                }
            }
        }

    }
}
