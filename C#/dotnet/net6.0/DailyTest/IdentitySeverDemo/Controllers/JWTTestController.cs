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
            return BadRequest("�û������������");
        }
        if (await userManager.CheckPasswordAsync(user, password))
        {
            // failed count ����Ϊ0
            await userManager.ResetAccessFailedCountAsync(user).CheckAsync();
            user.JWTVersion++;// �û��ĵ�¼����� JWTVersion �ֶ�����1
            await userManager.UpdateAsync(user);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("JWTVersion", user.JWTVersion.ToString()));
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                // һ���û�����ӵ�ж����ɫ����ͬ��ɫ��ͬ�洢�� Role �����ɫ������
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            string jwtTokens = BuildToken(claims, snapshot.Value);
            return Ok(jwtTokens);
        }
        else
        {
            await userManager.AccessFailedAsync(user).CheckAsync();
            return BadRequest("�û������������");
        }
    }

    /// <summary>
    /// ���� Token
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="opt"></param>
    /// <returns></returns>
    private string BuildToken(List<Claim> claims, JWTSettings opt)
    {
        string expireSeconds = opt.ExpireSeconds;
        DateTime expiredTime = DateTime.Now.AddSeconds(Convert.ToDouble(expireSeconds));// ���� Token ����ʱ��Ϊ1Сʱ
        byte[] secBytes = Encoding.UTF8.GetBytes(opt.SecKey);
        var secKey = new SymmetricSecurityKey(secBytes);
        var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expiredTime, signingCredentials: credentials);
        string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        return jwt;
    }


}