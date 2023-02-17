using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Zack.ASPNETCore;

namespace ASP.NETCoreWebAPIDemo;

[Route("api/[controller]/[action]")]
[ApiController]
public class CacheTestController : ControllerBase
{
    private readonly IMemoryCache memoryCache;
    private readonly ILogger<CacheTestController> logger;
    private readonly IMemoryCacheHelper memoryCacheHelper;

    public CacheTestController(IMemoryCache memoryCache, ILogger<CacheTestController> logger, IMemoryCacheHelper memoryCacheHelper)
    {
        this.memoryCache = memoryCache;
        this.logger = logger;
        this.memoryCacheHelper = memoryCacheHelper;
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