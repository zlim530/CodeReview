using Microsoft.AspNetCore.Identity;

namespace IdentitySeverDemo.Model;

public class MyUser : IdentityUser<long>
{
    public string? WeChatAccount { get; set; }

    public long JWTVersion { get; set; }
}