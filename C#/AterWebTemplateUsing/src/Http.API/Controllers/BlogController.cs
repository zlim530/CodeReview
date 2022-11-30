using Share.Models.BlogDtos;
namespace Http.API.Infrastructure;

[AllowAnonymous]
public class BlogController : RestControllerBase<IBlogManager>
{

    private readonly ISystemUserManager _userManager;

    public BlogController(
        IUserContext user,
        ILogger<BlogController> logger,
        IBlogManager manager,
        ISystemUserManager userManager
        ) : base(manager, user, logger)
    {
        this._userManager = userManager;
    }

    /// <summary>
    /// 筛选
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<ActionResult<PageList<BlogItemDto>>> FilterAsync(BlogFilterDto filter)
    {
        return await manager.FilterAsync(filter);
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Blog>> AddAsync(BlogAddDto form)
    {
        var user = await _userManager.GetCurrent(form.SystemUserId);
        if (user == null)
        {
            return NotFound("用户不存在！");
        }
        var entity = form.MapTo<BlogAddDto, Blog>();
        entity.SystemUser = user;
        return await manager.AddAsync(entity);
    }

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="id"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<Blog?>> UpdateAsync([FromRoute] Guid id, BlogUpdateDto form)
    {
        var current = await manager.GetCurrent(id);
        if (current == null) return NotFound();
        return await manager.UpdateAsync(current, form);
    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Blog?>> GetDetailAsync([FromRoute] Guid id)
    {
        var res = await manager.FindAsync(id);
        return (res == null) ? NotFound() : res;
    }

    /// <summary>
    /// ⚠删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<Blog?>> DeleteAsync([FromRoute] Guid id)
    {
        var entity = await manager.GetCurrent(id);
        if (entity == null) return NotFound();
        return await manager.DeleteAsync(entity);
    }
}