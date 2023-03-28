using Microsoft.AspNetCore.Mvc;
using UserMgr.Domain.Entities;
using UserMgr.Domain.Interfaces;
using UserMgr.Domain.ValueObjects;
using UserMgr.Infrastracture.DbContexts;
using UserMgr.WebAPI.Dtos;
using UserMgr.WebAPI.UnitOfWorks;

namespace UserMgr.WebAPI.Controllers;

/// <summary>
/// 应用层主要进行安全认证、权限校验、数据校验、事务控制、工作单元控制、领域服务的调用等。从理论上来讲，应用层中不应该有业务规则或者业务逻辑
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class UserMgrController : ControllerBase
{
    private readonly IUserDomainRepository userDomainRepository;
    private readonly UserDbContext userDbContext;

    public UserMgrController(IUserDomainRepository userDomainRepository,
                            UserDbContext userDbContext)
    {
        this.userDomainRepository = userDomainRepository;
        this.userDbContext = userDbContext;
    }

    [HttpPost]
    [UnitOfWork(typeof(UserDbContext))]
    public async Task<IActionResult> AddNewUserAsync(AdduserRequest req)
    {
        PhoneNumber phoneNumber = req.PhoneNumber;
        if (await userDomainRepository.FindOneAsync(phoneNumber) != null)
        {
            return BadRequest("手机号已经存在");
        }
        var user = new User(phoneNumber);
        user.ChangePassword(req.Password);
        userDbContext.Users.Add(user);
        return Ok("成功");
    }
}