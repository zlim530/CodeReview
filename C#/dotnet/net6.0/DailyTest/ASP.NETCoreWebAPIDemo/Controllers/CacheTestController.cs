using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ASP.NETCoreWebAPIDemo;

[Route("api/[controller]/[action]")]
[ApiController]
public class CacheTestController : ControllerBase
{
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<CacheTestController> logger;

    public CacheTestController(IMemoryCache memoryCache, ILogger<CacheTestController> logger)
    {
        this.memoryCache = memoryCache;
        this.logger = logger;
    }


    [HttpGet]
    public async Task<ActionResult<Book?>> GetBookByIdAsync(long id)
    {
        // GetOrCreateAsync 方法二合一：1）从缓存中取数据 2）如果缓存中没有数据就执行回调方法：从数据源取数据，并且返回给调用者并且保存到缓存
        logger.LogInformation($"开始查询缓存id={id}的书");
        Book? b = await memoryCache.GetOrCreateAsync("Book" + id, async (b) =>
        {
            logger.LogInformation($"缓存里没有扎到，去数据库查询id={id}的书");
            return await MyDbContext.GetBookByIdAsync(id);
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