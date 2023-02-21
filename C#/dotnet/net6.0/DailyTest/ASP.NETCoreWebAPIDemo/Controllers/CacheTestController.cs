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
    /// ʹ�÷�װ�� GetOrCreateAsync �����ķֲ�ʽ�������������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<Book?>> GetBookByDistributedCacheHelper(long id)
    {
        var book = await distributedCacheHelper.GetOrCreateAsync("Book" + id, async (e) => {
            e.SlidingExpiration = TimeSpan.FromSeconds(5);// ���û�������ʱ��
            var book = await MyDbContext.GetBookByIdAsync(id);
            return book;
        }, 20);// 20:���þ��Թ���ʱ��

        if (book == null)
        {
            return NotFound($"�Ҳ���Id={id}����");
        }
        else
        {
            return Ok(book);
        }
    }

    /// <summary>
    /// ʹ�� Redis �ֲ�ʽ����
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
            return NotFound($"�Ҳ���Id={id}����");
        }
        else
        {
            return Ok(book);
        }
    }

    /// <summary>
    /// ������ IQueryable �� IEnumerable �ӳټ���������Ϊ�����ֵ
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
            return NotFound($"�Ҳ���Id={id}����");
        }
        else
        {
            return Ok(b);
        }
    }

    [HttpGet]
    public async Task<ActionResult<Book?>> GetBookByIdAsync(long id)
    {
        // GetOrCreateAsync ��������һ��1���ӻ�����ȡ���� 2�����������û�����ݾ�ִ�лص�������������Դȡ���ݣ����ҷ��ظ������߲��ұ��浽����
        logger.LogInformation($"��ʼ��ѯ����id={id}����");
        Book? b = await memoryCache.GetOrCreateAsync("Book" + id, async (b) =>
        {
            logger.LogInformation($"������û��������ȥ���ݿ��ѯid={id}����");
            //b.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);// ���û�����Թ���ʱ��Ϊ10��
            //b.SlidingExpiration = TimeSpan.FromSeconds(5); // ���û�������ʱ�䣺ֻҪ���趨ʱ���ڷ��ʼ������ù���ʱ��
            b.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Random.Shared.NextDouble(10,15));// �ù���ʱ�����
            var book = await MyDbContext.GetBookByIdAsync(id);
            logger.LogInformation($"�����ݿ��в�ѯ�Ľ����{book??null}"); // GetOrCreateAsync ������� null Ҳ��Ϊһ���Ϸ��Ļ���ֵ�������Ӧ�ĳ����ڴ滺����
            return book;
        });
        logger.LogInformation($"GetOrCreateAsync ִ�н����{b}");
        if (b == null)
        {
            return NotFound($"�Ҳ���Id={id}����");
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