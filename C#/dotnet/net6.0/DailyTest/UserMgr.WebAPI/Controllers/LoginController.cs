using Microsoft.AspNetCore.Mvc;
using UserMgr.Domain;
using UserMgr.Domain.Enums;
using UserMgr.Infrastracture.DbContexts;
using UserMgr.WebAPI.Dtos;
using UserMgr.WebAPI.UnitOfWorks;

namespace UserMgr.WebAPI.Controllers;

/// <summary>
/// Ӧ�ò��Ƿǳ�����һ�㣬Ӧ�ò���Ҫ�������ݵ�У�顢�������ݵĻ�ȡ��������񷵻�ֵ��չʾ�ȴ�����û�и��ӵ�ҵ���߼�����Ϊ��Ҫ��ҵ���߼�������װ������㣨Domain��
/// </summary>
[ApiController]
[Route("[controller]/[action]")]
public class LoginController : ControllerBase
{
    private readonly UserDomainService userDomainService;

	public LoginController(UserDomainService userDomainService)
	{
		this.userDomainService = userDomainService;
	}

	[HttpPost]
	[UnitOfWork(typeof(UserDbContext))]// ��Ϊ CheckPassword ���п����޸����ݵĲ�������������ڴ˷����е��á�������Ԫ������
	public async Task<IActionResult> LoginByPhoneAndPasswordAsync(LoginByPhoneAndPwdRequest req)
	{
		if (req.Password.Length  <= 3)
		{
			return BadRequest("����ĳ��ȱ������3");
		}
		var result = await userDomainService.CheckLoginAsync(req.PhoneNumber, req.Password);
		switch (result)
		{
			case UserAccessResult.OK:
				return Ok("��¼�ɹ�");
			case UserAccessResult.PasswordError:
			case UserAccessResult.NoPassword:
			case UserAccessResult.PhoneNumberNotFound:
				return BadRequest("�ֻ��Ż����������");
            case UserAccessResult.Lockout:
				return BadRequest("�˻�������");
			default:
				throw new ApplicationException($"δֵ֪��{result}");
		}

	}

	[HttpPost]
	[UnitOfWork(typeof(UserDbContext))]
	public async Task<IActionResult> CheckCodeAsync(CheckLoginByPhoneAndCodeRequest req)
	{
		var result = await userDomainService.CheckCodeAsync(req.PhoneNumber, req.Code);
		switch (result)
		{
			case CheckCodeResult.OK:
				return Ok("��¼�ع�");
			case CheckCodeResult.PhoneNumberNotFound:
				return BadRequest("�������");
			case CheckCodeResult.Lockout:
				return BadRequest("�û������������Ժ�����");
			case CheckCodeResult.CodeError:
				return BadRequest("��֤�����");
			default:
				throw new NotImplementedException($"δֵ֪��{result}");
		}

	}

}