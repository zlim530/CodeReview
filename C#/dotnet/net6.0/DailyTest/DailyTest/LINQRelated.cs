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

        /// <summary>
        /// LINQ 常用扩展方法：Single、First、OrderBy、GroupBy
        /// </summary>
        /// <param name="args"></param>
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

            #region Single & First

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
            #endregion


            #region OrderBy & ThenBy

            Random rand = new Random();
            //var items = list.OrderByDescending(e => rand.Next());// 根据随机数排序，每次排序结果都是随机的
            //var items = list.OrderByDescending(e => Guid.NewGuid());
            //var items = list.OrderByDescending(e => e.Name[e.Name.Length - 1]);// 也可以自定义排序规则：根据名字的最后一个字母进行排序
            //var items = list.OrderBy(i => i.Age).ThenBy(s => s.Salary);
            // 先根据年龄进行排序（正序：从小到大），年龄相同的再根据工资进行排序（正序：从小到大）
            var items = list.OrderBy(a => a.Name).OrderBy(i => i.Age);
            // 联系两个 OrderBy 排序：先按照后面的进行排序，如果后面的排序一样再按照前面的排序规则进行排序，和 ThenBy 的逻辑顺序正好相反
            //foreach (var item in items)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion


            #region GroupBy 

            /*
            分组：
            GroupBy() 方法参数是分组条件表达式，返回值为 IGrouping<TKey, TSource> 类型的泛型 IEnumerable，也就是
            每一组以一个 IGrouping 对象的形式返回。IGrouping 是一个继承自 IEnumerable 的接口，IGrouping 中 Key 
            属性表示这一组的分组数据的值（即根据什么规则进行的分组）。
            */
            // SQL 语句 be like => select Age, max(Salary) from t group by Age
            /*IEnumerable<IGrouping<int, Employee>>*/
            var groups = list.GroupBy(e => e.Age);
            foreach (/*IGrouping<int, Employee>*/var g in groups)
            {
                // 根据年龄分组，获取每组人数、最大工资、平均工资
                Console.WriteLine(g.Key);
                Console.WriteLine("根据年龄进行分组，每一个年龄相同的组中，最大工资的是：" + g.Max(s => s.Salary));
                Console.WriteLine("根据年龄进行分组，每组人数是：" + g.Count());
                Console.WriteLine("根据年龄进行分组，每组平均工资是：" + g.Average(s => s.Salary));
                foreach (Employee e in g)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("************************");
            }
            #endregion

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
