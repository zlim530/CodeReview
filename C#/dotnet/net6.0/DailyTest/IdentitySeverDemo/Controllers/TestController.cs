using IdentitySeverDemo.DTO;
using IdentitySeverDemo.Helper;
using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IdentitySeverDemo.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize]// ����Ҫ��¼���ܷ��ʵĿ��������� Action ��������Ӵ�����
public class TestController : ControllerBase
{
    private readonly UserManager<MyUser> userManager;
    private readonly RoleManager<MyRole> roleManager;

    public TestController(UserManager<MyUser> userManager, RoleManager<MyRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    [HttpGet]
    [Authorize(Roles ="admin")]// ��ʾֻ�� admin ��ɫ���û��ſ��Է��ʴ˽ӿ�
    public ActionResult<string> Hello()
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var userName = this.User.FindFirst(ClaimTypes.Name)!.Value;
        var roles = this.User.FindAll(ClaimTypes.Role);
        var roleName = string.Join(",", roles.Select(r => r.Value));
        return Ok($"Id = {userId}, UserName = {userName}, roleNames = {roleName}");
    }

    [HttpGet]
    [AllowAnonymous]// ��ʾ����¼Ҳ���Է��ʴ˽ӿڷ���
    public async Task<ActionResult<string>> InitTest()
    {
        bool isExists = await roleManager.RoleExistsAsync("admin");
        if (isExists == false)
        {
            MyRole admin = new MyRole { Name = "admin"};
            var result = await roleManager.CreateAsync(admin);
            if (!result.Succeeded)
            {
                return BadRequest("roleManager.CreateAsync failed");
            }
        }
        MyUser user = await userManager.FindByNameAsync("admin");
        if (user == null) 
        {
            user = new MyUser { UserName = "yzk"};
            var result = await userManager.CreateAsync(user,"123456");
            if (!result.Succeeded)
            {
                return BadRequest("userManager.CreateAsync failed");
            }
        }
        if (!await userManager.IsInRoleAsync(user, "admin"))
        {
            var result = await userManager.AddToRoleAsync(user, "admin");
            if (!result.Succeeded)
            {
                return BadRequest("�û������ɫ������");
            }
        }
        return Ok("OK");
    }


    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CheckPwd(CheckPwdRequest request)
    {
        var userName = request.UserName;
        var password = request.Password;
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return BadRequest("�û������������");
        }
        if (await userManager.IsLockedOutAsync(user))
        {
            return BadRequest($"�û��ѱ���������������ʱ��Ϊ��{user.LockoutEnd}");
        }
        if (await userManager.CheckPasswordAsync(user, password))
        {
            await userManager.ResetAccessFailedCountAsync(user);
            return Ok("��¼�ɹ�");
        }
        else
        {
            await userManager.AccessFailedAsync(user);
            return BadRequest("�û������������");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<string>> SendResetPasswordToken(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return BadRequest("�û�������");
        }
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        Console.WriteLine($"Reset Password Token is {token}");
        return Ok(token);
    }


    [HttpPut]
    [AllowAnonymous]
    public async Task<ActionResult> ResetPassword(string userName, string token, string newPassword)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return BadRequest("�û�������");
        }
        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        if (result.Succeeded)
        {
            await userManager.ResetAccessFailedCountAsync(user);
            return Ok("��������ɹ���");
        }
        else
        {
            await userManager.AccessFailedAsync(user);
            return BadRequest("��������ʧ�ܣ�");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<string>> AddNewUser(AddNewUserRequest req)
    {
        MyUser user = new MyUser { UserName = req.UserName, Email = req.Email};
        await userManager.CreateAsync(user, req.Password).CheckAsync();
        return Ok("�û������ɹ���");
    }

}


public record CheckPwdRequest(string UserName, string Password);