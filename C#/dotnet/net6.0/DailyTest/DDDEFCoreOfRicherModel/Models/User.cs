using System.ComponentModel.DataAnnotations.Schema;
using Zack.Commons;

namespace DDDEFCoreOfRicherModel.Models;

/// <summary>
/// EFCore ��ʵ�ֳ�Ѫģ��
/// </summary>
public record User
{
    // ������ֻ���Ļ���ֻ�ܱ��ڲ��Ĵ����޸ġ�
    // init ��ʾֻ���ڶ����ʼ��ʱ���и�ֵ
    public int Id { get; init; }

    public DateTime CreatedDateTime { get; init; }

    // ֻ�������ڲ�����
    public string UserName { get; private set; }

    public int Credit { get; set; }

    // û�ж�Ӧ���ԣ�������Щ��Ա������Ҫӳ��Ϊ���ݱ��е��У�Ҳ����������Ҫ��˽�г�Ա����ӳ�䵽���ݱ��е���
    private string? passwordHash { get; set; }

    // [Column("Remark")]
    // �ֶ�
    private string? remark;
    //[NotMapped]
    // ֻ�����ԣ�Ҳ��������ֵ�Ǵ����ݿ��ж�ȡ�����ģ��������ǲ����޸�����ֵ
    public string? Remark
    {
        get { return remark; }
    }

    // �е����Բ���Ҫӳ�䵽�����У���������ʱ��ʹ�á�
    public string? Tag { get; set; }

    // �� EFCore ��ܴ����ݿ��м�������Ȼ������ User ����ʱ��ֵ�����õģ�EFCore ���Է��ʱ������ɵĺ�̨�ֶ�
    private User()
    {

    }

    // �����в����Ĺ��췽����������ʹ��
    public User(string accountName)
    {
        UserName = accountName;
        CreatedDateTime = DateTime.Now;
        Credit = 10;
    }

    public void ChangeUserName(string newValue)
    {
        if (newValue.Length > 5)
        {
            throw new ArgumentException("�û������Ȳ��ܴ���5");
        }
        UserName = newValue;
    }

    public void ChangePassword(string newValue)
    {
        if (newValue.Length < 6)
        {
            throw new ArgumentException("���볤�Ȳ���С��6");
        }
        passwordHash = HashHelper.ComputeSha256Hash(newValue);
    }

}