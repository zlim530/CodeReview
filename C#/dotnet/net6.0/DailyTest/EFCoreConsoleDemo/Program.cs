using Microsoft.EntityFrameworkCore;

namespace EFCoreConsoleDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
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
    }
}