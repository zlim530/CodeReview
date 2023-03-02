using System.ComponentModel.DataAnnotations;

namespace IdentitySeverDemo.DTO;

public class AddNewUserRequest
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    //[Compare(nameof(Password))]// ��ʾ Password2 ��ֵ������ Password һ��
    public string Password2 { get; set; }
}