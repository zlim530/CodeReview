using EntityFrameworkCoreModel;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace ASP.NETCoreWebAPIDemo.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FilterTestController : ControllerBase
{
    private readonly MyMultiDBContext _myDbContext;
    private readonly MyMovieDBContext _movieDbContext;

    public FilterTestController(MyMultiDBContext myMultiDBContext,
        MyMovieDBContext myMovieDBContext)
    {
        _myDbContext = myMultiDBContext;
        _movieDbContext = myMovieDBContext;
    }


    [HttpGet]
    public async Task<string> TranscationFilterTest()
    {
        _myDbContext.Books.Add(new EntityFrameworkCoreModel.Book { Title = "数学之美", AuthorName = "Jun Wu", Price = 69, PubDate = DateTime.Now });
        await _myDbContext.SaveChangesAsync();
        _movieDbContext.Movies.Add(new Movie { Title = "Maria Larssons eviga ögonblick", ReleaseDate = DateTime.Parse("2008-09-24"), Genre = "剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记", Price = 8.4M });
        await _movieDbContext.SaveChangesAsync();
        return "done";
    }

    [HttpGet]
    [NotTranscationAttibute]
    public async Task<string> TranscationAsyncTest()
    {
        using (TransactionScope tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            _myDbContext.Books.Add(new EntityFrameworkCoreModel.Book { Title = "数学之美", AuthorName = "Jun Wu", Price = 69, PubDate = DateTime.Now });
            await _myDbContext.SaveChangesAsync();
            _movieDbContext.Movies.Add(new Movie { Title = "Maria Larssons eviga ögonblick", ReleaseDate = DateTime.Parse("2008-09-24"), Genre = "剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记", Price = 8.4M });
            await _movieDbContext.SaveChangesAsync();
            tx.Complete();
        }

        return "done";
    }

    [HttpGet]
    [NotTranscationAttibute]
    public string TranscationTest()
    {
        #region 不使用 TranscationScope：不同事务间无法嵌套与自动回滚
        //_myDbContext.Books.Add(new EntityFrameworkCoreModel.Book { Title = "程序员的SQL金典", AuthorName = "杨中科", Price = 52, PubDate = DateTime.Now });
        //_myDbContext.SaveChanges();// 一个事务
        //_movieDbContext.Movies.Add(new Movie { Title = "Maria Larssons eviga ögonblick",  ReleaseDate = DateTime.Parse("2008-09-24"), Genre = "剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记", Price = 8.4M });
        //_movieDbContext.SaveChanges();// 一个事务
        #endregion

        using (TransactionScope tx = new TransactionScope())// 只有有一个失败了，就会自动回滚
        {
            _myDbContext.Books.Add(new EntityFrameworkCoreModel.Book { Title = "数学之美", AuthorName = "Jun Wu", Price = 69, PubDate = DateTime.Now });
            _myDbContext.SaveChanges();// 一个事务
            _movieDbContext.Movies.Add(new Movie { Title = "Maria Larssons eviga ögonblick", ReleaseDate = DateTime.Parse("2008-09-24"), Genre = "剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记 剧情 / 传记", Price = 8.4M });
            _movieDbContext.SaveChanges();// 一个事务
            tx.Complete();
        }

        return "done";
    }

    [HttpGet]
    public string ExceptionFilterTestDemo()
    { 
        var s = System.IO.File.ReadAllText("d:/1.txt");
        return s;
    }

    [HttpGet]
    public string ActionFilterTestDemo() 
    {
        Console.WriteLine("运行 Action 方法中");
        return "OK";
    }

}