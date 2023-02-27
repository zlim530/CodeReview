using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySeverDemo.Controllers;

[ApiController]
[Route("[controller]/[action]")]
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
}


public record CheckPwdRequest(string UserName, string Password);