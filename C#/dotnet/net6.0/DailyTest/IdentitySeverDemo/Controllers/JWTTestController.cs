using IdentitySeverDemo.Helper;
using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentitySeverDemo.Controllers;


[ApiController]
[Route("[controller]/[action]")]
public class JWTTestController : ControllerBase
{
    private readonly UserManager<MyUser> userManager;
    private readonly RoleManager<MyRole> roleManager;
    private readonly IOptionsSnapshot<JWTSettings> snapshot;

    public JWTTestController(UserManager<MyUser> userManager,
        RoleManager<MyRole> roleManager,
        IOptionsSnapshot<JWTSettings> snapshot)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.snapshot = snapshot;
    }

    [HttpPost]
    public async Task<ActionResult<string>> LoginAsync(string userName, string password)
    {
        var user = await userManager.FindByNameAsync(userName);
        var success = await userManager.CheckPasswordAsync(user,password);
        if (!success)
        {
            return BadRequest("用户名或密码错误");
        }
        if (await userManager.CheckPasswordAsync(user, password))
        {
            // failed count 重置为0
            await userManager.ResetAccessFailedCountAsync(user).CheckAsync();
            user.JWTVersion++;// 用户的登录后就让 JWTVersion 字段自增1
            await userManager.UpdateAsync(user);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("JWTVersion", user.JWTVersion.ToString()));
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                // 一个用户可以拥有多个角色，不同角色共同存储在 Role 这个角色数组中
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            string jwtTokens = BuildToken(claims, snapshot.Value);
            return Ok(jwtTokens);
        }
        else
        {
            await userManager.AccessFailedAsync(user).CheckAsync();
            return BadRequest("用户名或密码错误");
        }
    }

    /// <summary>
    /// 创建 Token
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="opt"></param>
    /// <returns></returns>
    private string BuildToken(List<Claim> claims, JWTSettings opt)
    {
        string expireSeconds = opt.ExpireSeconds;
        DateTime expiredTime = DateTime.Now.AddSeconds(Convert.ToDouble(expireSeconds));// 设置 Token 过期时间为1小时
        byte[] secBytes = Encoding.UTF8.GetBytes(opt.SecKey);
        var secKey = new SymmetricSecurityKey(secBytes);
        var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expiredTime, signingCredentials: credentials);
        string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return jwt;
    }


}