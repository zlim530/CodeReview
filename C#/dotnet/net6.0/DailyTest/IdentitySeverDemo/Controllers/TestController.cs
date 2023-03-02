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
[Authorize]// 在需要登录才能访问的控制器或者 Action 方法上添加此特性
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
    [Authorize(Roles ="admin")]// 表示只有 admin 角色的用户才可以访问此接口
    public ActionResult<string> Hello()
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var userName = this.User.FindFirst(ClaimTypes.Name)!.Value;
        var roles = this.User.FindAll(ClaimTypes.Role);
        var roleName = string.Join(",", roles.Select(r => r.Value));
        return Ok($"Id = {userId}, UserName = {userName}, roleNames = {roleName}");
    }

    [HttpGet]
    [AllowAnonymous]// 表示不登录也可以访问此接口方法
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
                return BadRequest("用户名或角色名错误");
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
            return BadRequest("用户名或密码错误");
        }
        if (await userManager.IsLockedOutAsync(user))
        {
            return BadRequest($"用户已被锁定，锁定结束时间为：{user.LockoutEnd}");
        }
        if (await userManager.CheckPasswordAsync(user, password))
        {
            await userManager.ResetAccessFailedCountAsync(user);
            return Ok("登录成功");
        }
        else
        {
            await userManager.AccessFailedAsync(user);
            return BadRequest("用户名或密码错误");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<string>> SendResetPasswordToken(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return BadRequest("用户名错误！");
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
            return BadRequest("用户名错误！");
        }
        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        if (result.Succeeded)
        {
            await userManager.ResetAccessFailedCountAsync(user);
            return Ok("重置密码成功！");
        }
        else
        {
            await userManager.AccessFailedAsync(user);
            return BadRequest("密码重置失败！");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<string>> AddNewUser(AddNewUserRequest req)
    {
        MyUser user = new MyUser { UserName = req.UserName, Email = req.Email};
        await userManager.CreateAsync(user, req.Password).CheckAsync();
        return Ok("用户创建成功！");
    }

}


public record CheckPwdRequest(string UserName, string Password);