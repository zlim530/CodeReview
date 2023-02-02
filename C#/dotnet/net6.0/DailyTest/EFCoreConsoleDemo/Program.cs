using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace EFCoreConsoleDemo
{
    public class Program
    {
        /// <summary>
        /// EFCore 的迁移与基本使用
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main0(string[] args)
        {
            // db = 逻辑上的数据库
            using (var db = new MyDBContext())
            {
                //Book b = new Book();
                //b.Title = "Test";
                //b.AuthorName = "Test";
                //b.PubTime = DateTime.Now;
                //b.Price = 1;
                //db.Books.Add(b);// 把 book 对象加到 Books 这个逻辑上的数据表里

                //var b1 = new Book { AuthorName = "杨中科", Title = "零基础趣学C语言", Price = 59.8, PubTime = new DateTime(2019, 3, 1)};
                //var b2 = new Book { AuthorName = "Robert Sedgewick", Title = "算法（第4版）", Price = 99, PubTime = new DateTime(2012, 10, 1)};
                //var b3 = new Book { AuthorName = "吴军", Title = "数学之美", Price = 69, PubTime = new DateTime(2020, 5, 1)};
                //var b4 = new Book { AuthorName = "杨中科", Title = "程序员的SQL金典", Price = 52, PubTime = new DateTime(2008, 9, 1)};
                //var b5 = new Book { AuthorName = "吴军", Title = "文明之光", Price = 246, PubTime = new DateTime(2017, 3, 1)};

                //db.Books.Add(b1);
                //db.Books.Add(b2);
                //db.Books.Add(b3);
                //db.Books.Add(b4);
                //db.Books.Add(b5);

                var groups = db.Books.GroupBy(b => b.AuthorName)
                                    .Select(g => new { AuthorName = g.Key, BooksCount = g.Count(), MaxPrice = g.Max(b => b.Price) })
                                    ;

                foreach (var g in groups)
                {
                    Console.WriteLine($"作者名：{g.AuthorName}，著作数量：{g.BooksCount}，最贵的价格：{g.MaxPrice}");
                }

                var query = db.Books.Where(b => b.Id == 3);
                Console.WriteLine(query.ToQueryString());

                //var b = db.Books.Single(b => b.Id == 4);
                //b.AuthorName = "Jun Wu";

                //var b2 = db.Books.Single(b => b.Id == 1);
                //db.Remove(b2);

                //var books = db.Books.Where(b => b.Title.PadLeft(5) == "Hello");

                //await db.SaveChangesAsync();// 保存更改的操作：类似于 Update-Database 指令
                Console.WriteLine("done");
            }
        }

        /// <summary>
        /// EFCore 多关系配置
        /// </summary>
        /// <param name="args"></param>
        static async Task Main1(string[] args)
        {
            using (var dbContext = new MyDBContext())
            {
                #region 一对多关系的插入
                //var a = new Article() { Title = "The Most New PA Rules", Content = "According to google ... " };

                //var c1 = new Comment { Message = "Great!"};
                //var c2 = new Comment { Message = "Cannot understand" };

                //a.Comments.Add(c1);
                //a.Comments.Add(c2);

                //dbContext.Articles.Add(a);
                #endregion


                #region 一对多主从关系表的查询：包括使用 HasForeignKey 属性仅获取主表 Id

                var a = dbContext.Articles.Include(a => a.Comments).SingleOrDefault(a => a.Id == 2);
                //Console.WriteLine(a.Id);
                //Console.WriteLine(a.Title);
                //foreach (var c in a.Comments)
                //{
                //    Console.WriteLine($"{c.Id} + {c.Message}");
                //}

                //var c = dbContext.Comments.Include(c => c.Article).SingleOrDefault(c => c.Id == 1);
                //Console.WriteLine(c.Message);
                //Console.WriteLine($"{c.Article.Id} + {c.Article.Content}");

                //var c = dbContext.Comments.Select(c => new { Id = c.Id, AId = c.Article.Id }).Single(c => c.Id == 2);
                //Console.WriteLine($"cid:{c.Id}, aid:{c.AId}");

                var cmt = dbContext.Comments.Single(c => c.Id == 2);
                //Console.WriteLine(cmt.ArticleId);

                #endregion


                //var l = dbContext.Leaves.SingleOrDefault();
                //var user = new User { Name = "ZLim"};
                //var leave = new Leave { Remarks = "Annual Leave", Requester = user };
                //dbContext.Leaves.Add(leave);


                #region 自引用的组织结构树关系

                // 既可以甚至一个 ou 对象的 parent 节点也把一个节点加入到自己的 Children 节点
                var ouRoot = new OrgUnit { Name = "中科集团全球总部" };

                var ouAsia = new OrgUnit { Name = "中科集团亚太区总部" };
                var ouAmerica = new OrgUnit { Name = "中科集团美洲总部" };
                //ouAsia.Parent = ouRoot;
                //ouAmerica.Parent = ouRoot;
                ouRoot.Children.Add(ouAsia);
                ouRoot.Children.Add(ouAmerica);

                var ouUSA = new OrgUnit { Name = "中科美国" };
                var ouCan = new OrgUnit { Name = "中科加拿大" };
                //ouUSA.Parent = ouAmerica;
                //ouCan.Parent = ouAmerica;
                ouAmerica.Children.Add(ouUSA);
                ouAmerica.Children.Add(ouCan);

                var ouChina = new OrgUnit { Name = "中科集团（中国）" };
                var ouSg = new OrgUnit { Name = "中科集团（新加坡）" };
                //ouChina.Parent = ouAsia;
                //ouSg.Parent = ouAsia;
                ouAsia.Children.Add(ouChina);
                ouAsia.Children.Add(ouSg);

                dbContext.OrgUnits.Add(ouRoot);// 如果设置 Parent 节点，则需要将所有 OrgUnit 对象都加入到逻辑表中
                //dbContext.OrgUnits.Add(ouRoot);
                //dbContext.OrgUnits.Add(ouAsia);
                //dbContext.OrgUnits.Add(ouAmerica);
                //dbContext.OrgUnits.Add(ouUSA);
                //dbContext.OrgUnits.Add(ouCan);
                //dbContext.OrgUnits.Add(ouChina);
                //dbContext.OrgUnits.Add(ouSg);

                var root = dbContext.OrgUnits.SingleOrDefault(o => o.Parent == null);
                //Console.WriteLine(root.Name);
                //PrintChildren(1, dbContext, root);
                
                #endregion


                //await dbContext.SaveChangesAsync();

                Console.WriteLine("Done.");
            }
        }

        /// <summary>
        /// EFCore 一对一与多对多关系
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args)
        {
            using (var ctx = new MyDBContext())
            {
                var order = new Order { Name = "Book"};
                var delivery = new Delivery { CompanyName = "BBDelivery", Number = "BB001", Order = order};
                //ctx.Deliveries.Add(delivery);
                //ctx.Orders.Add(order);

                var stu1 = new Student { Name = "张三"};
                var stu2 = new Student { Name = "李四"};
                var stu3 = new Student { Name = "王五"};

                var tch1 = new Teacher { Name = "Tim"};
                var tch2 = new Teacher { Name = "NewJack"};
                var tch3 = new Teacher { Name = "Rose"};

                stu1.Teachers.Add(tch1);
                stu1.Teachers.Add(tch2);

                stu2.Teachers.Add(tch2);
                stu2.Teachers.Add(tch3);

                stu3.Teachers.Add(tch3);
                stu3.Teachers.Add(tch2);
                stu3.Teachers.Add(tch1);

                ctx.Students.Add(stu1);
                ctx.Students.Add(stu2);
                ctx.Students.Add(stu3);

                var teachers = ctx.Teachers.Include(t => t.Students);
                foreach (var t in teachers)
                {
                    Console.WriteLine(t.Name);
                    foreach (var s in t.Students)
                    {
                        Console.WriteLine("\t" + s.Name);
                    }
                }

                //await ctx.SaveChangesAsync(); 
            }
        }

        /// <summary>
        /// 缩进打印 parent 的所有子节点
        /// </summary>
        /// <param name="identLevel"></param>
        /// <param name="context"></param>
        /// <param name="parent"></param>
        static void PrintChildren(int identLevel, MyDBContext context, OrgUnit parent)
        {
            var children = context.OrgUnits.Where(o => o.Parent == parent).ToList();
            foreach (var child in children)
            {
                Console.WriteLine(new string('\t',identLevel) + child.Name);
                PrintChildren(identLevel + 1, context, child);// 递归打印以当前节点为父节点的子节点
            }
        }
    }
}