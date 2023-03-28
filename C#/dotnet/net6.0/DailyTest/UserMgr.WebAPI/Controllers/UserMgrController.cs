using Microsoft.AspNetCore.Mvc;
using UserMgr.Domain.Entities;
using UserMgr.Domain.Interfaces;
using UserMgr.Domain.ValueObjects;
using UserMgr.Infrastracture.DbContexts;
using UserMgr.WebAPI.Dtos;
using UserMgr.WebAPI.UnitOfWorks;

namespace UserMgr.WebAPI.Controllers;

/// <summary>
/// Ӧ�ò���Ҫ���а�ȫ��֤��Ȩ��У�顢����У�顢������ơ�������Ԫ���ơ��������ĵ��õȡ���������������Ӧ�ò��в�Ӧ����ҵ��������ҵ���߼�
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
            return BadRequest("�ֻ����Ѿ�����");
        }
        var user = new User(phoneNumber);
        user.ChangePassword(req.Password);
        userDbContext.Users.Add(user);
        return Ok("�ɹ�");
    }
}