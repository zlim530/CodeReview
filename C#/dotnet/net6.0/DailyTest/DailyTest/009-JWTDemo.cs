using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DailyTest;

public class JWTDemo
{
    private static string key = "abfaajdklasjd123189274839*((q1w2e3r4_";

    static void Main0(string[] args)
    {
        var token = CreateJWT();
        string[] segments = token.Split('.');
        // JWT 是明文存储的，可以对 JWT 进行解码操作获取对应的 payload 信息，因此不需要客户端知道的信息就不要放在 payload 中存储
        var head = JWTDecode(segments[0]);
        var payload = JWTDecode(segments[1]);
        Console.WriteLine("head:");
        Console.WriteLine(head);
        Console.WriteLine("payload:");
        Console.WriteLine(payload);
        //string jwt = Console.ReadLine();
        //// 输入被篡改的 token 会抛出异常
        //VerifyJWT(jwt);
        VerifyJWT(token);
    }

    /// <summary>
    /// 创建 JWT
    /// Token 是存储在客户端的
    /// </summary>
    /// <returns></returns>
    static string CreateJWT()
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, "yzk"));
        claims.Add(new Claim(ClaimTypes.Name, "yzk"));
        claims.Add(new Claim(ClaimTypes.Role, "manager"));
        claims.Add(new Claim(ClaimTypes.Role, "admin"));
        claims.Add(new Claim(ClaimTypes.HomePhone, "123456"));
        claims.Add(new Claim("WeChatAccount", "yzk.com"));// 也可以自定义键的值但是需要与客户端约定好
        DateTime expiredTime = DateTime.Now.AddHours(1);// 设置 Token 过期时间为1小时
        byte[] secBytes = Encoding.UTF8.GetBytes(key);
        var secKey = new SymmetricSecurityKey(secBytes);
        var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expiredTime, signingCredentials: credentials);
        string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        Console.WriteLine(jwt);
        return jwt;
    }

    /// <summary>
    /// 解码 JTW 令牌
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    static string JWTDecode(string token)
    {
        token = token.Replace('-', '+').Replace('_','/');
        switch (token.Length % 4)
        {
            case 2:
                token += "==";
                break;
            case 3:
                token += "=";
                break;
        }
        var bytes = Convert.FromBase64String(token);
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    /// 用JwtSecurityTokenHandler对JWT解码：可以判断 JWT 是否被篡改
    /// 服务器端拿到客户端传过来的 token 进行解析，将 header、payload 和 秘钥（仅服务器知道）算出对应的签名与传过来的 token 中的签名进行对比是否一致，不一致则说明发生了篡改
    /// </summary>
    /// <param name="token"></param>
    static void VerifyJWT(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new ();
        TokenValidationParameters tokenValidation = new ();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        tokenValidation.IssuerSigningKey = securityKey;
        tokenValidation.ValidateIssuer = false;
        tokenValidation.ValidateAudience = false;
        ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidation, out SecurityToken secToken);
        foreach (var claim in claimsPrincipal.Claims)
        {
            Console.WriteLine($"{claim.Type} = {claim.Value}");
        }
    }

}