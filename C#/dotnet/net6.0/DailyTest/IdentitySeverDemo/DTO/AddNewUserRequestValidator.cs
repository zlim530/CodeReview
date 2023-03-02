using FluentValidation;
using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;

namespace IdentitySeverDemo.DTO;

public class AddNewUserRequestValidator : AbstractValidator<AddNewUserRequest>
{
	public AddNewUserRequestValidator(UserManager<MyUser> userManager)
	{
		RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress()
			.WithMessage("���䲻�Ϸ�")
			.Must(x => x.EndsWith("@163.com") || x.EndsWith("@126.com")) // Must �п��Ա�д�Զ���ļ������
			.WithMessage("��֧����������")
			;
		RuleFor(x => x.UserName).NotNull().NotEmpty().Length(3, 10)
			.WithMessage("�û������Ϸ�")
			.MustAsync(async (x, _) => await userManager.FindByNameAsync(x) == null)// �жϵ�ǰ�û����Ƿ������ݿ���Ҳ���ڣ�Ҳ���Խ�����Ƶ������ݿ��ҵ��У��д�� Controller ��
			.WithMessage(x => $"�û���:{x.UserName}�ظ�")
			;
		RuleFor(x => x.Password).Equal(x => x.Password2)
			.WithMessage("�����������벻һ��");// �Զ��������Ϣ
	}
}