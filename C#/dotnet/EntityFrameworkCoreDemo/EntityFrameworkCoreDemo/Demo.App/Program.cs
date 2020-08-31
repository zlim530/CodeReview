using Demo.Date;
using Demo.Domian;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.App {
    class Program {
        /// <summary>
        /// 添加数据：单行与多行
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args) {
            using var context = new DemoContext();

            var serieA = new League { Country = "Italy",Name = "Serie A"};

            var serieB = new League { Country = "Italy",Name = "Serie B"};

            var serieC = new League { Country = "Italy",Name = "Serie C"};

            var milan = new Club { 
                Name = "AC Milan",
                City = "Milan",
                DateOfEstableishment = new DateTime(1899,12,16),
                League = serieA
            };
            // 在同一张表中添加单条数据
            //context.Leagues.Add(serieA);

            // 在不同表中同时添加多笔数据：直接使用 context，不使用 DbSet，context 可以识别出来每个数据是哪个类型（表）的
            context.AddRange(serieB,serieC,milan);

            // 在一张表中同时添加多笔数据
            //context.Leagues.AddRange(serieB,serieC);
            //context.Leagues.AddRange(new List<League> { serieB,serieC});

            var count = context.SaveChanges();

            Console.WriteLine(count);
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="args"></param>
        static void Main1(string[] args) {
            using var context = new DemoContext();

            //var italy = "Italy";

            //var leagues = context.Leagues
            //    .Where(x => x.Country == italy)// 如果这里的使用字面值，那么生成的 SQL 语句里也是写死的常量
            //    .ToList();

            //var league1s = context.Leagues
            //    .Where(x => 
            //        EF.Functions.Like(x.Country,"%e%"))// 如果这里的使用字面值，那么生成的 SQL 语句里也是写死的常量
            //    .ToList();

            //foreach (var league in league1s) {
            //    Console.WriteLine(league.Name);
            //}

            //var leagu2e = (from lg in context.Leagues 
            //               where lg.Country == "Italy"
            //               select lg).ToList();

            var first = context.Leagues.SingleOrDefault(x => x.Id == 2);

            var one = context.Leagues.Find(2);

            Console.WriteLine(first?.Name);
            Console.WriteLine(one?.Name);
            /*
            info: Microsoft.EntityFrameworkCore.Database.Command[20101]
                  Executed DbCommand (29ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
                  SELECT TOP(2) [l].[Id], [l].[Country], [l].[Name]
                  FROM [Leagues] AS [l]
                  WHERE [l].[Id] = 2
            Serie B
            Serie B
            输出控制台中只有一条 SQL 语句，即 SingleOrDefault 查询命令的结果会缓存在 context 对象中
            当我们使用 Find 继续查询时会先去 context 对象的缓存中查看是否有结果数据，如果没有再去数据库中查询
            但是如果反过来，即先执行 Find 命令再执行 SingleOrDefault 命令则会输出两条 SQL 语句
            */

            var last = context.Leagues
                        .OrderByDescending(x => x.Id)// 使用 LastOrDefault 方法之前需要进行排序，否则会报错
                        .LastOrDefault(x => x.Name.Contains("a"));
            Console.WriteLine(last?.Name);
        }


        /// <summary>
        /// 删除、修改
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args) {
            using var context = new DemoContext();

            #region Delete
            // delete
            // 只能删除被 context 对象追踪的数据，所以在删除之前需要先进行查询
            // 让 context 对象追踪查询到的数据
            //var milan = context.Clubs.Single(x => x.Name == "AC Milan");

            // 调用删除方法
            //context.Clubs.Remove(milan);
            //context.Remove(milan);

            //context.Clubs.RemoveRange(milan);
            //context.RemoveRange(milan);

            // 执行对数据库的事务动作
            //var count = context.SaveChanges();
            //Console.WriteLine(count);
            #endregion

            #region Update
            //var leagues = context.Leagues.Skip(1).Take(3).ToList();

            //foreach (var league in leagues) {
            //    league.Name += "~~";
            //}

            //var count = context.SaveChanges();

            //Console.WriteLine(count);

            // 模拟在数据库中根据前端传入的数据进行修改，此时我们就不能使用查询了
            // AsNoTracking 即表示 context 对象不会追踪数据的变化，就相当于前端传过来的数据，没有变化追踪
            // 如果想在全局中使用 AsNoTracking 可以在 DemoContext 类中的构造函数中进行配置
            var league = context.Leagues.AsNoTracking().First();

            league.Name += "++";

            // 对应 context 没有追踪或者说没有变化的数据，要使用 Update 方法进行更新
            // 但是 Update 方法会将一条数据中的所有字段标记为修改状态，也即将所有字段都修改了一遍（如果我们没有在代码中修改值那么就保持原值不变，但是仍让会在 SQL 语句中执行原列名=原值），除了主键以外
            context.Leagues.Update(league);

            var count = context.SaveChanges();

            Console.WriteLine(count);

            #endregion
        }


        /// <summary>
        /// 添加关系数据
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {

        }
    }
}
