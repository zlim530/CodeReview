using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualBasic;
using System.Data.Common;

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
        static async Task Main2(string[] args)
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
        /// EFCore 基于关系的复杂查询
        /// </summary>
        /// <param name="args"></param>
        static async Task Main3(string[] args)
        {
            using (var ctx = new MyDBContext())
            {
                var item = new Article { Title = "Microsoft Thought for the day!" , Content = "Looking forward to ... " };
                var item2 = new Article { Title = "Microsoft Update!", Content = "After recent windows update  ... " };

                var comment1 = new Comment { Message = "Yes!"};
                var comment2 = new Comment { Message = "Really?!"};
                var comment3 = new Comment { Message = "Good to know!"};
                var comment4 = new Comment { Message = "Me too!"};

                item.Comments.Add(comment1);
                item.Comments.Add(comment3);
                item2.Comments.Add(comment4);
                item2.Comments.Add(comment2);

                //ctx.Articles.Add(item);
                //ctx.Articles.Add(item2);

                var items = ctx.Articles.Where(a => a.Comments.Any(c => c.Message.Contains("Microsoft")));
                foreach (var i in items)
                {
                    Console.WriteLine(i.Title);
                }

                // 用 Include 就会把关系数据自动添加上，如果不需要关联数据的全部数据就不需要 Include，仅作为筛选条件也不需要 Include
                var items2 = ctx.Comments.Where(c => c.Message.Contains("?!")).Select(c => c.Article).Distinct();
                foreach (var i2 in items2)
                {
                    Console.WriteLine(i2.Title);
                }

                var order = ctx.Orders.Where(o => o.Delivery.Number == "BB001").SingleOrDefault();
                Console.WriteLine(order.Name);

                await ctx.SaveChangesAsync();
            }
        }

        /// <summary>
        /// IQueryable And IEnumerabl
        /// </summary>
        /// <param name="args"></param>
        static void Main4(string[] args)
        {
            using (var ctx = new MyDBContext())
            {
                #region IQueryable 与 IEnumerable 的区别
                //：都采用延迟加载机制，区别是 IEnumerable 的数据处理过滤过程在程序项目所在的客户端上，而不是在数据库端
                IQueryable<Article> query = ctx.Articles;// 延迟加载：将查询操作翻译成 SQL 语句在数据库服务器中进行评估操作：也即服务器端评估
                /*
                IQueryable 数据源生成的 SQL 语句：
                SELECT [t].[Id], [t].[Content], [t].[Title]
                FROM [T_Articles] AS [t]
                WHERE [t].[Title] LIKE N'%Microsoft%' 
                */
                //IEnumerable<Article> query = ctx.Articles;// 在内存中获取 Article 表的所有数据，并在程序内部进行过滤操作：也即客户端评估
                /*
                IEnumerable 数据源生成的 SQL 语句：
                SELECT [t].[Id], [t].[Content], [t].[Title]
                FROM [T_Articles] AS [t] 
                */
                // 通常来说一般都是使用服务器端评估效率更高
                //IEnumerable<Article> list = query.Where(a => a.Title.Contains("Microsoft"));

                //IEnumerable<Article> list = query.Where(a => IsOk(a.Title));
                //The LINQ expression 'DbSet<Article>().Where(a => Program.IsOk(a.Title))' could not be translated. => 当遇到比较复杂的操作逻辑导致 LINQ 无法翻译为 SQL 语句时，就只能用 IEnumerable 来执行

                #endregion

                //foreach (var a in list)
                //{
                //    Console.WriteLine(a.Title);
                //}

                #region 关于延迟查询

                IEnumerable<Article> list = ctx.Articles;
                //list = list.Where(a => a.Title.Contains("Microsoft"));// IEnumerable 也是延迟加载的，在调用终结方法前不会生成执行 SQL 语句，不会将所有的数据加载到内存中
                //Console.WriteLine(list.Count());// 此时才生成了 SQL 语句：SELECT [t].[Id], [t].[Content], [t].[Title] FROM[T_Articles] AS[t]

                // IQueryable 最重要的作用是拼接表达式树，所谓的非终结方法其实是这棵树上的一个个分支，等终结方法执行时，会将拼接好的 SQL 语句真正执行
                query = query.Where(a => a.Title.Contains("Microsoft"));// IQueryable 构建了一个可以被执行的查询，它是一个待查询的逻辑，因此 IQueryable 是可以被重复使用（复用）的

                // 每一次终结操作都会生成一条新的 SQL 语句
                /*SELECT[t].[Id], [t].[Content], [t].[Title]
                FROM[T_Articles] AS[t]
                WHERE[t].[Title] LIKE N'%Microsoft%'*/
                //query.ToArray();

                /*SELECT COUNT(*)
                FROM[T_Articles] AS[t]
                WHERE[t].[Title] LIKE N'%Microsoft%'*/
                //Console.WriteLine(query.Count());

                /*SELECT MAX([t].[Id])
                FROM[T_Articles] AS[t]
                WHERE[t].[Title] LIKE N'%Microsoft%'*/
                //Console.WriteLine(query.Max(a => a.Id));

                #endregion

                //QueryArticles("Microsoft",false,false,2);
                //PrintPage(1,2);

                #region IQueryable 的“缺点”
                /*
                1、DataReader：分批从数据库服务器读取数据。内存占用小、 DB连接占用时间长；
                2、DataTable：把所有数据都一次性从数据库服务器都加载到客户端内存中。内存占用大，节省DB连接。
                验证 IQueryable 底层是使用 DataReader 的方式：
                1、用insert into select多插入一些数据，然后加上Delay/Sleep的遍历IQueryable。在遍历执行的过程中，停止SQLServer服务器。
                IQueryable内部就是在调用DataReader。
                2、优点：节省客户端内存。（因为过滤处理过程在数据库端）
                缺点：如果处理的慢，会长时间占用连接
                */

                // 1. 当 dbContext 对象被消除后就不可以在 IQueryable 对象上调用终结方法
                //var queryable = Test();
                //// System.ObjectDisposedException: Cannot access a disposed context instance.
                //foreach (var item in queryable)
                //{
                //    Console.WriteLine(item.Title);
                //}

                // 2. 当多个 IQueryable 对象遍历嵌套时，很多数据库都不支持多个 DataReader 同时执行
                // System.InvalidOperationException: There is already an open DataReader associated with this Connection which must be closed first.
                // 解决方法1：对于 SQLServer，可以在数据库连接字符串上加上"MultipleActiveResultSets=True;"，但其他数据库不支持；解决方法2：使用 IEnumerable 将所有数据加载到客户端内存中处理
                //foreach (var a in query.ToArray()) 
                //{
                //      Console.WriteLine(a.Title);
                //    foreach (var c in ctx.Comments)
                //    {
                //        Console.WriteLine(c.Message);
                //    }
                //}

                #endregion

            }
        }

        /// <summary>
        /// 异步方法是对于那些耗时(如IO等)操作，能够避免长时间占用线程导致线程阻塞
        /// 注意：非终结方法是不消耗 IO 
        /// </summary>
        /// <param name="args"></param>
        static async Task Main5(string[] args)
        {
            // 异步遍历 IQueryable : 一般没必要这么做
            using (var ctx = new MyDBContext())
            {
                // 1.将终结方法异步化
                foreach (var a in await ctx.Articles.ToListAsync())
                {
                    Console.WriteLine(a.Title);
                }

                // 2.使用 IAsyncEnumerable 对象
                await foreach (var a in ctx.Articles.Where(a => a.Title.Contains("a")).AsAsyncEnumerable())
                {
                    Console.WriteLine(a.Title);
                }

                // 3.使用 ForEachAsync 方法
                ctx.Articles.Where(a => a.Title.Contains("a")).ForEachAsync(a => Console.WriteLine(a.Title));
            }
        }

        /// <summary>
        /// EF Core 执行原生 SQL 语句
        /// 一般Linq操作就够了，尽量不用写原生SQL；
        /// 1、非查询SQL用ExecuteSqlInterpolated() ；
        /// 2、针对实体的SQL查询用FromSqlInterpolated()。
        /// 3、复杂SQL查询用ADO.NET的方式或者Dapper等。
        /// </summary>
        /// <param name="args"></param>
        static async Task Main6(string[] args)
        {
            using (var ctx = new MyDBContext())
            {
                #region 非查询的原生 SQL 语句
                string name = "Tim NiuBee!!!";
                int id = 18;
                name = ";delete from T_Articles;";

                //string sql = @$"insert into T_Articles
                //    (Title,Content)
                //    select Title,{name} 
                //    from T_Articles
                //    where id <= {id}";
                //Console.WriteLine(sql);

                FormattableString sql = @$"insert into T_Articles
                    (Title,Content)
                    select Title,{name} 
                    from T_Articles
                    where id <= {id}";
                Console.WriteLine($"Format: {sql.Format}");
                Console.WriteLine($"parameters: {string.Join(",",sql.GetArguments())}");

                // 使用 ExecuteSqlInterpolated 方法可以防止 SQL 注入，因为 ExecuteSqlInterpolated 方法会将传入的字符串转为 FormattableString 对象，将原始字符串参数化
                //await ctx.Database.ExecuteSqlInterpolatedAsync(@$"insert into T_Articles
                //    (Title,Content)
                //    select Title,{name} 
                //    from T_Articles
                //    where id <= {id}
                //    ");

                #endregion


                #region 实体相关的查询原生 SQL 语句
                var pattern = "%the%";

                // FromSqlInterpolated 方法返回的是 IQueryable 对象，因此这个方法是非终结方法，也是延迟执行的
                // 但是 FromSqlInterpolated 只能单表查询，不能使用Join语句进行关联查询。但是可以在查询后面使用Include()来进行关联数据的获取。
                var queryable = ctx.Articles.FromSqlInterpolated($"select * from T_Articles where Title like {pattern} ");// 除非另外还指定了 TOP、OFFSET 或 FOR XML，否则，ORDER BY 子句在视图、内联函数、派生表、子查询和公用表表达式中无效：由于 EF Core 自动生成的 SQL 语句中使用了子句查询语法，而在查询子句中不可以使用 order by 关键字
                                                                                                                          //foreach (var item in queryable.Include(a => a.Comments).OrderBy(g => Guid.NewGuid()).Skip(1).Take(3))
                                                                                                                          //{
                                                                                                                          //    Console.WriteLine(item.Id + item.Title);
                                                                                                                          //    foreach (var c in item.Comments)
                                                                                                                          //    {
                                                                                                                          //        Console.WriteLine(c.Message);
                                                                                                                          //    }
                                                                                                                          //}
                #endregion


                #region 任意原生 SQL 查询语句

                //1. 使用 ADO .NET 的方式实现
                //DbConnection conn = ctx.Database.GetDbConnection();// 拿到 context 对象对应的底层的 Connection 数据库连接对象
                //if (conn.State != System.Data.ConnectionState.Open)
                //{
                //    await conn.OpenAsync();
                //}

                //using (var cmd = conn.CreateCommand())
                //{
                //    cmd.CommandText = "select Id, Count(*) from T_Articles group by Id";
                //    using (var reader = await cmd.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            long i = reader.GetInt64(0);
                //            int count = reader.GetInt32(1);
                //            Console.WriteLine($"id:{i}, count:{count}");
                //        }
                //    }
                //}

                // 2.使用 Dapper 来实现
                var g = ctx.Database.GetDbConnection().Query<GroupArticlesId>("select Id, Count(*) Count from T_Articles group by Id");
                //foreach (var item in g)
                //{
                //    Console.WriteLine(item.Id + ":" +  item.Count);
                //}

                #endregion
            }
        }

        /// <summary>
        /// EF Core 实体跟踪相关：保存快照 -> 标记对象的状态 -> 根据实体状态的不同生成 SQL 语句而将实体的变更生成到数据库中
        /// </summary>
        /// <param name="args"></param>
        static async Task Main(string[] args)
        {
            using (var ctx = new MyDBContext())
            {
                // 只要一个实体对象和 DBContext 对象发生任何关系（包括但不限于查询、Add、以及与 DbContext 有关系的其他对象产生关系）都会默认被 DbContext 跟踪
                //var a = await ctx.Articles.FirstOrDefaultAsync();
                //a.Content = "dasjkldj";
                //a.Title = "Breaking News!!!" + a.Title;

                var items = ctx.Articles.Skip(6).Take(3).ToArray();
                var a1 = items[0];
                var a2 = items[1];
                var a3 = items[2];

                var a4 = new Article { Title = "AddEntryStatusTest", Content = "EntityEntryState"};
                var a5 = new Article { Title = "NoTachTest", Content = "NoStateTest" };

                a1.Title = "UpdateTest";
                ctx.Remove(a2);
                ctx.Articles.Add(a4);

                EntityEntry e1 = ctx.Entry(a1);
                EntityEntry e2 = ctx.Entry(a2);
                EntityEntry e3 = ctx.Entry(a3);
                EntityEntry e4 = ctx.Entry(a4);
                EntityEntry e5 = ctx.Entry(a5);

                /*
                DbContext会根据跟踪的实体的状态，在SaveChanges()的时候，根据实体状态的不同，生成Update、Delete、Insert等SQL语句，来把内存中实体的变化更新到数据库中。
                */
                Console.WriteLine(e1.State);// Modified
                Console.WriteLine(e2.State);// Deleted
                Console.WriteLine(e3.State);// Unchanged
                Console.WriteLine(e4.State);// Added
                Console.WriteLine(e5.State);// Detached

                /*
                如果通过DbContext查询出来的对象只是用来展示，不会发生状态改变，则可以使用AsNoTracking()来 “禁用跟踪”。
                如果查询出来的对象不会被修改、删除等，那么查询时可以AsNoTracking()，就能降低内存占用。
                */
                var items2 = ctx.Articles.AsNoTracking().Take(3).ToArray();
                foreach (var item in items2)
                {
                    Console.WriteLine(item.Title + ":" + item.Id);
                    Console.WriteLine(ctx.Entry(item).State);// Detached:未跟踪的
                }

                var a = new Article { Id = 18, Title = "UpdateTest"};
                var entrya = ctx.Entry(a);
                //entrya.Property("Title").IsModified = true;// 使用这种写法可以让 update 操作只执行一条 SQL 语句，而不是像往常那样先查询再进行修改或者删除操作：但是这种写法不推荐，知道即可
                Console.WriteLine(entrya.DebugView.LongView);
                /*
                    Article {Id: 18} Modified
                    Id: 18 PK
                    Content: <null>
                    Title: 'UpdateTest' Modified
                    Comments: [] 
                */
                entrya.State = EntityState.Deleted;

                await ctx.SaveChangesAsync();
            }
        }

        class GroupArticlesId
        {
            public long Id { get; set; }

            public int Count { get; set; }
        }

        /// <summary>
        /// 如果方法需要返回查询结果，并且在方法里销毁DbContext的话，是不能返回IQueryable的。必须一次性加载返回。
        /// </summary>
        /// <returns></returns>
        static IQueryable<Article> Test()
        {
            using (var ctx = new MyDBContext())
            {
                return ctx.Articles.Where(a => a.Title.Contains("Microsoft"));
            }
        }

        /// <summary>
        /// 打印文章表的某一页数据与总页数
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        static void PrintPage(int pageIndex, int pageSize)
        {
            using (var ctx = new MyDBContext())
            {
                var query = ctx.Articles.Where(a => a.Title.Contains("Microsoft"));
                query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                foreach (var item in query)
                {
                    Console.WriteLine(item.Title);
                }

                long count = query.LongCount();
                var pageCount = (long)Math.Ceiling(count * 1.0 / pageSize);
                Console.WriteLine($"Total Page:{pageCount}");
            }
        }

        /// <summary>
        /// IQueryable 对象动态拼接 SQL 
        /// </summary>
        /// <param name="searchWord"></param>
        /// <param name="searchAll"></param>
        /// <param name="orderPrice"></param>
        /// <param name="maxId"></param>
        static void QueryArticles(string searchWord, bool searchAll, bool orderPrice, int maxId)
        {
            using (MyDBContext ctx = new MyDBContext())
            {
                var querys = ctx.Articles.Where(a => a.Id <= maxId);
                if (searchAll)
                {
                    querys = querys.Where(a => a.Title.Contains(searchWord) || a.Content.Contains(searchWord));
                }
                else
                {
                    querys = querys.Where(a => a.Title.Contains(searchWord));
                }
                if (orderPrice)
                {
                    querys = querys.OrderBy(a => a.Id);
                }

                foreach (var q in querys)
                {
                    Console.WriteLine(q.Title);
                }
            }
        }

        static bool IsOk(string s)
        {
            if (s.StartsWith("M"))
            {
                return s.Length > 5;
            }
            else
            {
                return s.Length < 3;
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