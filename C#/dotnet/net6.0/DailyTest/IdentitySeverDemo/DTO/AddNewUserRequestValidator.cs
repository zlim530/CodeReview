using FluentValidation;
using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;

namespace IdentitySeverDemo.DTO;

public class AddNewUserRequestValidator : AbstractValidator<AddNewUserRequest>
{
	public AddNewUserRequestValidator(UserManager<MyUser> userManager)
	{
		RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress()
			.WithMessage("邮箱不合法")
			.Must(x => x.EndsWith("@163.com") || x.EndsWith("@126.com")) // Must 中可以编写自定义的检验规则
			.WithMessage("仅支持网易邮箱")
			;
		RuleFor(x => x.UserName).NotNull().NotEmpty().Length(3, 10)
			.WithMessage("用户名不合法")
			.MustAsync(async (x, _) => await userManager.FindByNameAsync(x) == null)// 判断当前用户名是否在数据库中也存在：也可以将此设计到查数据库的业务校验写到 Controller 中
			.WithMessage(x => $"用户名:{x.UserName}重复")
			;
		RuleFor(x => x.Password).Equal(x => x.Password2)
			.WithMessage("两次密码输入不一致");// 自定义错误消息
	}
}