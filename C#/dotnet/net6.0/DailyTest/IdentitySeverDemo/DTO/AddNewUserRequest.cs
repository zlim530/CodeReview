using System.ComponentModel.DataAnnotations;

namespace IdentitySeverDemo.DTO;

public class AddNewUserRequest
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    //[Compare(nameof(Password))]// 表示 Password2 的值必须与 Password 一致
    public string Password2 { get; set; }
}