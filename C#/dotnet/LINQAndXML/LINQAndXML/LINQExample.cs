using System;
using System.Collections.Generic;
using System.Linq;

/**
 * @author zlim
 * @create 2020/8/7 14:44:49
 */
namespace LINQAndXML {
    public class LINQExample {

        public static void Main0(string[] args) {
            var masters = new List<MartialArtsMaster>() { 
                new MartialArtsMaster(){ Id = 1,Name = "黄蓉",Age = 18,Menpai = "丐帮",Kungfu = "打狗棒法",Level = 9},
                new MartialArtsMaster(){ Id = 2,Name = "洪七公",Age = 70,Menpai = "丐帮",Kungfu = "打狗棒法",Level = 10},
                new MartialArtsMaster(){ Id = 3,Name = "郭靖",Age = 22,Menpai = "丐帮",Kungfu = "降龙十八掌",Level = 10},
                new MartialArtsMaster(){ Id = 4,Name = "任我行",Age = 50,Menpai = "明教",Kungfu = "吸星大法",Level = 1},
                new MartialArtsMaster(){ Id = 5,Name = "东方不败",Age = 35,Menpai = "明教",Kungfu = "葵花宝典",Level = 10},
                new MartialArtsMaster(){ Id = 6,Name = "林平之",Age = 23,Menpai = "华山",Kungfu = "葵花宝典",Level = 7},
                new MartialArtsMaster(){ Id = 7,Name = "岳不群",Age = 50,Menpai = "华山",Kungfu = "葵花宝典",Level = 8},
            };

            var kongfus = new List<Kongfu>() { 
                new Kongfu(){ KongfuId = 1,KongfuName = "打狗棒法",Lethality = 90},
                new Kongfu(){ KongfuId = 2,KongfuName = "降龙十八掌",Lethality = 95},
                new Kongfu(){ KongfuId = 3,KongfuName = "葵花宝典",Lethality = 100},
                new Kongfu(){ KongfuId = 4,KongfuName = "吸星大法",Lethality = 10},
            };


            // =====================================================================
            // LINQ query
            /*var GaiBangMaster = from m in masters
                                where m.Level > 8 && m.Menpai == "丐帮"
                                select m;*/

            // LINQ method
            /*var GaiBangMaster = masters.Where(m => m.Level > 8 && m.Menpai == "丐帮");

            Console.WriteLine("查询丐帮中功能高于8级的大侠：");
            foreach (var m in GaiBangMaster) {
                Console.WriteLine($"{m.Id} {m.Name} {m.Age} {m.Menpai} {m.Kungfu} {m.Level} ");
            }*/


            // =====================================================================
            // LINQ query
            /*var mastersKongfu = from m in masters
                                from k in kongfus
                                where k.Lethality > 90 && m.Kungfu == k.KongfuName
                                orderby m.Level
                                select m.Id + " " + m.Name + " " + m.Age + " " + m.Menpai + " " + m.Kungfu + " " + m.Level + " ";*/

            // LINQ method
            /*var mastersKongfu = masters.SelectMany(k => kongfus, (m,k) => new { mt = m, kf = k })
                                       .Where(x => x.kf.Lethality > 90 && x.mt.Kungfu == x.kf.KongfuName)
                                       .OrderBy(m => m.mt.Level)
                                       .Select(m => m.mt.Id + " " + m.mt.Name + " " + m.mt.Age + " " + m.mt.Menpai + " " + m.mt.Kungfu + " " + m.mt.Level + " ");

            Console.WriteLine("过滤所学武学杀伤力大于90的大侠：");
            foreach (var m in mastersKongfu) {
                Console.WriteLine(m.ToString());
            }*/

            // =====================================================================
            //int i = 0;

            // LINQ query
            /*var topMaster = from m in masters
                            from k in kongfus
                            where k.KongfuName == m.Kungfu
                            orderby m.Level * k.Lethality descending ,m.Age,m.Name
                            select (++i) + " " + m.Name + " " + m.Age + " " + m.Menpai + " " + m.Kungfu + " " + m.Level + " "+ m.Level * k.Lethality + " "  ;*/

            // LINQ method
            /*var topMaster = masters.SelectMany(k => kongfus, (m, k) => new { mt = m, kf = k })
                                   .Where(x => x.mt.Kungfu == x.kf.KongfuName)
                                   .OrderByDescending(m => m.mt.Level * m.kf.Lethality)
                                   .ThenBy(m => m.mt.Age)
                                   .ThenBy(m => m.mt.Name)
                                   .Select(m => (++i) + " " + m.mt.Name + " " + m.mt.Age + " " + m.mt.Menpai + " " + m.mt.Kungfu + " " + m.mt.Level + " ");


            Console.WriteLine("武林排行榜：");
            foreach (var top in topMaster) {
                Console.WriteLine(top.ToString());
            }*/


            // =====================================================================
            /*int i = 1;
            var masterTops = masters.Where(x => x.Level > 8)
                                    .OrderByDescending(x => x.Level)
                                    .Select(x => new { Id = x.Id,Name = x.Name,MasterKongfu = x.Kungfu,Level = x.Level,Top = (++i)});

            int j = 1;
            var kongfuTops = from k in kongfus
                             where k.Lethality > 90
                             orderby k.Lethality descending
                             select new { KId = k.KongfuId, KName = k.KongfuName, KLethality = k.Lethality, KTop = (j++) };

            var masterLethalityTops = from m in masterTops
                                      join k in kongfuTops on m.MasterKongfu equals k.KName
                                      orderby m.Level * k.KLethality descending
                                      select new { Id = m.Id, Name = m.Name, Kongfu = m.MasterKongfu, Level = m.Level, Kill = m.Level * k.KLethality };

            var masterLethalityTopMethod = masterTops.Join(kongfuTops, m => m.MasterKongfu, k => k.KName, (m, k) => new { Id = m.Id, Name = m.Name, Kongfu = m.MasterKongfu, Level = m.Level, Kill = m.Level * k.KLethality })
                                                     .OrderByDescending(m => m.Kill);

            Console.WriteLine("通过对象关联，返回新的对象的高手的杀伤力:");
            foreach (var item in masterLethalityTops) {
                Console.WriteLine(item);
            }
            Console.WriteLine("====================================");
            foreach (var item in masterTops) {
                Console.WriteLine(item);
            }
            Console.WriteLine("====================================");
            foreach (var item in kongfuTops) {
                Console.WriteLine(item);
            }
            Console.WriteLine("====================================");
            foreach (var item in masterLethalityTopMethod) {
                Console.WriteLine(item);
            }*/

            // =====================================================================

            //Console.WriteLine("使用 Join 和 GroupJoin 的区别：");
            /*Console.WriteLine("使用 Join：");
            var JoinExample = from k in kongfus
                              join m in masters on k.KongfuName equals m.Kungfu
                              orderby k.Lethality * m.Level descending
                              select new { KId = k.KongfuId, KName = k.KongfuName, KLethality = k.Lethality, MasterName = m.Name, Kill = k.Lethality * m.Level };*/

            /*Console.WriteLine("使用 GroupJoin：");
            var JoinExample = kongfus.GroupJoin(masters, k => k.KongfuName, m => m.Kungfu, (k, m) => new { k.KongfuId, k.KongfuName, k.Lethality, Count = m.Count()})
                                     .OrderByDescending(k => k.Lethality);

            foreach (var item in JoinExample) {
                Console.WriteLine(item.ToString());
            }*/

            // =====================================================================

            masters.Add(new MartialArtsMaster() { Id = 8,Name = "令狐冲",Age = 23,Menpai = "华山",Kungfu = "独孤九剑",Level = 10});
            masters.Add(new MartialArtsMaster() { Id = 9,Name = "梅超风",Age = 23,Menpai = "梅花岛",Kungfu = "九阴真经",Level = 8});
            masters.Add(new MartialArtsMaster() { Id = 10,Name = "黄药师",Age = 23,Menpai = "梅花岛",Kungfu = "弹指神通",Level = 10});
            masters.Add(new MartialArtsMaster() { Id = 11,Name = "风清扬",Age = 23,Menpai = "华山",Kungfu = "独孤九剑",Level = 10});


            kongfus.Add(new Kongfu() { KongfuId = 4,KongfuName = "独孤九剑",Lethality = 100});
            kongfus.Add(new Kongfu() { KongfuId = 5,KongfuName = "九阴真经",Lethality = 100});
            kongfus.Add(new Kongfu() { KongfuId = 6,KongfuName = "弹指神通",Lethality = 100});

            // =====================================================================

            /*var masterItems = from k in kongfus
                              join m in masters on k.KongfuName equals m.Kungfu
                              into groups
                              orderby groups.Count() descending
                              select new { KId = k.KongfuId, KName = k.KongfuName, KLethality = k.Lethality, Count = groups.Count() };*/

            /*var masterItems = kongfus.GroupJoin(masters, k => k.KongfuName, m => m.Kungfu, (k, m) => new { k.KongfuId, k.KongfuName, k.Lethality, Count = m.Count() })
                                     .OrderByDescending(k => k.Count);

            foreach (var item in masterItems) {
                Console.WriteLine(item.ToString());
            }*/

            // =====================================================================

            /*var groupItems = from m in masters
                             group m by m.Menpai into g
                             orderby g.Key
                             select new { MenPai = g.Key,Count = g.Count()};*/

            /*var groupItems = masters.GroupBy(m => m.Menpai, (k, m) => new { MenPai = k, Count = m.Count() });

            foreach (var item in groupItems) {
                Console.WriteLine(item.ToString());
            }*/

            // =====================================================================

            /*var AnyItems = from m in masters
                           where m.Kungfu == "葵花宝典"
                           select new { m.Name,m.Menpai,m.Kungfu};

            Console.WriteLine("练习葵花宝典的大侠们：");
            foreach (var item in AnyItems) {
                Console.WriteLine(item.ToString());
            }

            var all = AnyItems.All(m => m.Menpai == "明教");
            Console.WriteLine($"练习葵花宝典的大侠们是否全部属于明教：{all}");
            var any = AnyItems.Any(m => m.Menpai == "华山");
            Console.WriteLine($"练习葵花宝典的大侠们是否有属于华山的：{any}");*/


            /*var OYFMaster = new MartialArtsMaster() { Id = 13,Name = "欧阳锋",Age = 50,Menpai = "白驼山庄",Kungfu = "蛤蟆功",Level = 10};
            var HYSMaster = masters[9];

            var IsOYF = masters.Contains(OYFMaster);
            var ISHYS = masters.Contains(HYSMaster as MartialArtsMaster);
            Console.WriteLine($"大侠名单中是否有欧阳锋： {IsOYF}");
            Console.WriteLine($"大侠名单中是否有黄药师： {ISHYS}");*/

            // =====================================================================

            masters.Add(new MartialArtsMaster() { Id = 12, Name = "肖锋", Age = 33, Menpai = "丐帮", Kungfu = "降龙十八掌", Level = 9 });
            masters.Add(new MartialArtsMaster() { Id = 13, Name = "段誉", Age = 23, Menpai = "天空寺", Kungfu = "六脉神剑", Level = 7 });
            masters.Add(new MartialArtsMaster() { Id = 14, Name = "虚竹", Age = 33, Menpai = "逍遥派", Kungfu = "北冥神功", Level = 9 });
            masters.Add(new MartialArtsMaster() { Id = 15, Name = "方证大师", Age = 33, Menpai = "少林寺", Kungfu = "七十二绝技", Level = 9 });
            masters.Add(new MartialArtsMaster() { Id = 16, Name = "杨过", Age = 33, Menpai = "古墓派", Kungfu = "玉女心经", Level = 9 });
            masters.Add(new MartialArtsMaster() { Id = 17, Name = "小龙女", Age = 33, Menpai = "古墓派", Kungfu = "玉女心经", Level = 10 });

            kongfus.Add(new Kongfu() { KongfuId = 7, KongfuName = "六脉神剑", Lethality = 100 });
            kongfus.Add(new Kongfu() { KongfuId = 8, KongfuName = "北冥神功", Lethality = 100 });
            kongfus.Add(new Kongfu() { KongfuId = 9, KongfuName = "七十二绝技", Lethality = 100 }); 
            kongfus.Add(new Kongfu() { KongfuId = 10, KongfuName = "玉女心经", Lethality = 95});

            /*int pageSize = 5;
            // double Math.Ceiling(double a) : 返回大于或等于指定的双精度浮点数的最小整数值
            int pageNumer = (int)Math.Ceiling(masters.Count() / (double)pageSize);
            Console.WriteLine("使用分区操作符分页：");

            Console.WriteLine($"大侠总数：{masters.Count} 总页数：{pageNumer} 每页人数：{pageSize}");
            for (int i = 0; i < pageNumer; i++) {
                var pageMaster = (from m in masters
                                  join k in kongfus on m.Kungfu equals k.KongfuName
                                  select new { m.Name, m.Menpai, m.Kungfu, k.Lethality, m.Level, kill = m.Level * k.Lethality })
                                // Skip : 跳过序列中指定数量的元素，然后返回剩余的元素 Take : 从序列的开头返回指定数量的相邻元素
                               .Skip(i * pageSize).Take(pageSize);
                Console.WriteLine("姓名       门派      杀伤力     修炼等级        总武力");

                foreach (var item in pageMaster) {
                    Console.WriteLine(item.ToString());
                }
            }*/

            // =====================================================================
            // 集合操作符：Union：并集且没有重复元素；Concat：返回两个并集；Except：差集，返回只出现在一个序列中的元素
            /*var items = from m in masters
                        where m.Menpai == "华山" || m.Menpai == "明教"
                        select m;
                                // Intersect：通过使用默认的相等比较器对值进行比较，生成两个序列的交集
            var IntersectItem = items.Intersect(from m in masters where m.Kungfu == "葵花宝典" select m);

            Console.WriteLine("华山派和明教中都使用葵花宝典的大侠:");
            foreach (var item in IntersectItem) {
                Console.WriteLine("{0} {1} {2}",item.Name,item.Menpai,item.Kungfu);
            }*/

            // Count：元素数量 LongCount：元素总量 Sum：元素总和 Max：最大值 Min：最小值
            int[] numbers = { 1,2,3,4};
            // Aggregate：将序列应用累加器函数
            int x = numbers.Aggregate((prod,n) => prod + n);
            int y = numbers.Aggregate(0, (prod, n) => prod + n);
            int z = numbers.Aggregate(0,(prod,n) => prod + n,r => r*2);
            Console.WriteLine("x = " + x + " y = " + y + " z = " + z);

        }

    }

    /// <summary>
    /// 武林高手
    /// </summary>
    public class MartialArtsMaster {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string Menpai { get; set; }

        public string Kungfu { get; set; }

        public int Level { get; set; }
    }

    /// <summary>
    /// 武学
    /// </summary>
    public class Kongfu {
        public int KongfuId { get; set; }
        public string KongfuName { get; set; }

        public int Lethality { get; set; }
    }
}
