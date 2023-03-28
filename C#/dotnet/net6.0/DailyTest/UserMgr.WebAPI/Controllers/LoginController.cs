using Microsoft.AspNetCore.Mvc;
using UserMgr.Domain;
using UserMgr.Domain.Enums;
using UserMgr.Infrastracture.DbContexts;
using UserMgr.WebAPI.Dtos;
using UserMgr.WebAPI.UnitOfWorks;

namespace UserMgr.WebAPI.Controllers;

/// <summary>
/// 应用层是非常薄的一层，应用层主要进行数据的校验、请求数据的获取、领域服务返回值的展示等处理，并没有复杂的业务逻辑，因为主要的业务逻辑都被封装在领域层（Domain）
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
	[UnitOfWork(typeof(UserDbContext))]// 因为 CheckPassword 中有可能修改数据的操作，因此我们在此方法中调用“工作单元”特性
	public async Task<IActionResult> LoginByPhoneAndPasswordAsync(LoginByPhoneAndPwdRequest req)
	{
		if (req.Password.Length  <= 3)
		{
			return BadRequest("密码的长度必须大于3");
		}
		var result = await userDomainService.CheckLoginAsync(req.PhoneNumber, req.Password);
		switch (result)
		{
			case UserAccessResult.OK:
				return Ok("登录成功");
			case UserAccessResult.PasswordError:
			case UserAccessResult.NoPassword:
			case UserAccessResult.PhoneNumberNotFound:
				return BadRequest("手机号或者密码错误");
            case UserAccessResult.Lockout:
				return BadRequest("账户被锁定");
			default:
				throw new ApplicationException($"未知值：{result}");
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
				return Ok("登录重构");
			case CheckCodeResult.PhoneNumberNotFound:
				return BadRequest("请求错误");
			case CheckCodeResult.Lockout:
				return BadRequest("用户被锁定，请稍后再试");
			case CheckCodeResult.CodeError:
				return BadRequest("验证码错误");
			default:
				throw new NotImplementedException($"未知值：{result}");
		}

	}

}