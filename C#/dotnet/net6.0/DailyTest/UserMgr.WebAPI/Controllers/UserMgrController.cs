using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserMgr.Domain;
using UserMgr.Domain.Entities;
using UserMgr.Domain.Interfaces;
using UserMgr.Domain.ValueObjects;
using UserMgr.Infrastracture.DbContexts;
using UserMgr.WebAPI.Dtos;
using UserMgr.WebAPI.UnitOfWorks;

namespace UserMgr.WebAPI.Controllers;

/// <summary>
///Ӧ�ò� ��WebAPI������Ҫ���а�ȫ��֤��Ȩ��У�顢����У�顢������ơ�������Ԫ���ơ��������ĵ��õȡ���������������Ӧ�ò��в�Ӧ����ҵ��������ҵ���߼�
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class UserMgrController : ControllerBase
{
    private readonly IUserDomainRepository userDomainRepository;
    private readonly UserDomainService userDomainService;
    private readonly UserDbContext userDbContext;

    public UserMgrController(IUserDomainRepository userDomainRepository,
                            UserDbContext userDbContext,
                            UserDomainService userDomainService)
    {
        this.userDomainRepository = userDomainRepository;
        this.userDbContext = userDbContext;
        this.userDomainService = userDomainService;
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


    [HttpPut]
    [UnitOfWork(typeof(UserDbContext))]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest req)
    {
        var user = await userDomainRepository.FindOneAsync(req.Id);
        if (user == null)
        {
            return NotFound();
        }
        user.ChangePassword(req.Password);
        return Ok("�ɹ�");
    }

    [HttpPut]
    [Route("{id}")]
    [UnitOfWork(typeof(UserDbContext))]
    public async Task<IActionResult> UnlockAsync(Guid id)
    {
        var user = await userDomainRepository.FindOneAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        userDomainService.ResetAccessFail(user);
        return Ok("�ɹ�");
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await userDbContext.Users.ToListAsync();
        return Ok(users);
    }

}