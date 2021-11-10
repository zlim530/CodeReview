using System;
using System.Collections.Generic;
using System.Linq;

namespace DailyLocalCode.SomeTryExample
{
    public static class LINQofFirst
    {
        #region 如何使用 Linq 获取每个分组中的第一个元素
        /* 
        Id          F1            F2             F3 
        ---------------------------------------------
        1           Nima          1990           10
        2           Nima          1990           11
        3           Nima          2000           12
        4           John          2001           1
        5           John          2002           2 
        6           Sara          2010           4
    
        我的需求是对 F1 字段进行分组然后按 Id 排序，最后从每个分组中提取出第一条记录项，类似下面的结果。

        Id          F1            F2             F3 
        ----------------------------------------------
        1           Nima          1990           10
        4           John          2001           1
        6           Sara          2010           4

        使用 Linq 来实现：
        */
        static void Main(string[] args)
        {
            var input = new List<F1Class>() { 
                new F1Class(){ Id = 1, F1 = "Nima", F2 = "1990", F3 = 10},
                new F1Class(){ Id = 2, F1 = "Nima", F2 = "1990", F3 = 11},
                new F1Class(){ Id = 3, F1 = "Nima", F2 = "2000", F3 = 12},
                new F1Class(){ Id = 4, F1 = "John", F2 = "2001", F3 = 1},
                new F1Class(){ Id = 5, F1 = "John", F2 = "2002", F3 = 2},
                new F1Class(){ Id = 6, F1 = "Sara", F2 = "2010", F3 = 4},
            };

            var result = input.GroupBy(i => i.F1).Select(i => i.First());

            var resul2t = input.GroupBy(i => new { i.F1, i.F2 }).Select(i => new { id = i.First().Id, f1 = i.First().F1 });

            var resul3t = from i in input
                            group i by i.F1
                            into groups
                            select groups.OrderBy(g => g.F2).First();

            // 可以先 OrderBy 再 GroupBy
            var resul4t = input.OrderBy(i => i.F2).GroupBy(i => i.F1).Select(i => i.First());

            Console.ReadLine();

        }

        #endregion
    }

    public class F1Class
    {
        public int Id { get; set; }
        public string F1 { get; set; }
        public string F2 { get; set; }
        public int F3 { get; set; }
    }
}
