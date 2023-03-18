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
        // JWT �����Ĵ洢�ģ����Զ� JWT ���н��������ȡ��Ӧ�� payload ��Ϣ����˲���Ҫ�ͻ���֪������Ϣ�Ͳ�Ҫ���� payload �д洢
        var head = JWTDecode(segments[0]);
        var payload = JWTDecode(segments[1]);
        Console.WriteLine("head:");
        Console.WriteLine(head);
        Console.WriteLine("payload:");
        Console.WriteLine(payload);
        //string jwt = Console.ReadLine();
        //// ���뱻�۸ĵ� token ���׳��쳣
        //VerifyJWT(jwt);
        VerifyJWT(token);
    }

    /// <summary>
    /// ���� JWT
    /// Token �Ǵ洢�ڿͻ��˵�
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
        claims.Add(new Claim("WeChatAccount", "yzk.com"));// Ҳ�����Զ������ֵ������Ҫ��ͻ���Լ����
        DateTime expiredTime = DateTime.Now.AddHours(1);// ���� Token ����ʱ��Ϊ1Сʱ
        byte[] secBytes = Encoding.UTF8.GetBytes(key);
        var secKey = new SymmetricSecurityKey(secBytes);
        var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(claims: claims, expires: expiredTime, signingCredentials: credentials);
        string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        Console.WriteLine(jwt);
        return jwt;
    }

    /// <summary>
    /// ���� JTW ����
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
    /// ��JwtSecurityTokenHandler��JWT���룺�����ж� JWT �Ƿ񱻴۸�
    /// ���������õ��ͻ��˴������� token ���н������� header��payload �� ��Կ����������֪���������Ӧ��ǩ���봫������ token �е�ǩ�����жԱ��Ƿ�һ�£���һ����˵�������˴۸�
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