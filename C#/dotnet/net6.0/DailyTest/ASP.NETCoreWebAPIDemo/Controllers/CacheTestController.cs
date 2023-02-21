using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Zack.ASPNETCore;

namespace ASP.NETCoreWebAPIDemo;

[Route("api/[controller]/[action]")]
[ApiController]
public class CacheTestController : ControllerBase
{
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<CacheTestController> logger;
    private readonly IMemoryCacheHelper memoryCacheHelper;
    private readonly IDistributedCache distributedCache;
    private readonly IDistributedCacheHelper distributedCacheHelper;

    public CacheTestController(IMemoryCache memoryCache,
        ILogger<CacheTestController> logger,
        IMemoryCacheHelper memoryCacheHelper,
        IDistributedCache distributedCache,
        IDistributedCacheHelper distributedCacheHelper)
    {
        this.memoryCache = memoryCache;
        this.logger = logger;
        this.memoryCacheHelper = memoryCacheHelper;
        this.distributedCache = distributedCache;
        this.distributedCacheHelper = distributedCacheHelper;
    }

    /// <summary>
    /// 使用封装了 GetOrCreateAsync 方法的分布式缓存操作帮助类
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Book?>> GetBookByDistributedCacheHelper(long id)
    {
        var book = await distributedCacheHelper.GetOrCreateAsync("Book" + id, async (e) => {
            e.SlidingExpiration = TimeSpan.FromSeconds(5);// 设置滑动过期时间
            var book = await MyDbContext.GetBookByIdAsync(id);
            return book;
        }, 20);// 20:设置绝对过期时间

        if (book == null)
        {
            return NotFound($"找不到Id={id}的书");
        }
        else
        {
            return Ok(book);
        }
    }

    /// <summary>
    /// 使用 Redis 分布式缓存
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Book?>> GetBookByDistributedCache(long id)
    {
        Book? book;
        string? s = await distributedCache.GetStringAsync("Book" + id);
        if (s == null)
        {
            book = await MyDbContext.GetBookByIdAsync(id);
            await distributedCache.SetStringAsync("Book" + id, JsonSerializer.Serialize(book));
        }
        else
        {
            book = JsonSerializer.Deserialize<Book?>(s);
        }

        if (book == null)
        {
            return NotFound($"找不到Id={id}的书");
        }
        else
        {
            return Ok(book);
        }
    }

    /// <summary>
    /// 限制了 IQueryable 和 IEnumerable 延迟加载类型作为缓存的值
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Book?>> GetBookByIdWithoutDelayLoadingAsync(long id)
    {
        var b = await memoryCacheHelper.GetOrCreateAsync<Book?>("Book" + id, async (b) => {
            return await MyDbContext.GetBookByIdAsync(id);
        });

        if (b == null)
        {
            return NotFound($"找不到Id={id}的书");
        }
        else
        {
            return Ok(b);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Book?>> GetBookByIdAsync(long id)
    {
        // GetOrCreateAsync 方法二合一：1）从缓存中取数据 2）如果缓存中没有数据就执行回调方法：从数据源取数据，并且返回给调用者并且保存到缓存
        logger.LogInformation($"开始查询缓存id={id}的书");
        Book? b = await memoryCache.GetOrCreateAsync("Book" + id, async (b) =>
        {
            logger.LogInformation($"缓存里没有扎到，去数据库查询id={id}的书");
            //b.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);// 设置缓存绝对过期时间为10秒
            //b.SlidingExpiration = TimeSpan.FromSeconds(5); // 设置滑动过期时间：只要在设定时间内访问即可重置过期时间
            b.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.NextDouble(10,15));// 让过期时间随机
            var book = await MyDbContext.GetBookByIdAsync(id);
            logger.LogInformation($"从数据库中查询的结果是{book??null}"); // GetOrCreateAsync 方法会把 null 也做为一个合法的缓存值，存入对应的程序内存缓存中
            return book;
        });
        logger.LogInformation($"GetOrCreateAsync 执行结果：{b}");
        if (b == null)
        {
            return NotFound($"找不到Id={id}的书");
        }
        else
        {
            return b;
        }
    }

    [HttpGet]
    [ResponseCache(Duration=20)]
    public DateTime GetNow() => DateTime.Now;
}