using System.ComponentModel.DataAnnotations.Schema;
using Zack.Commons;

namespace DDDEFCoreOfRicherModel.Models;

/// <summary>
/// EFCore 中实现充血模型
/// </summary>
public record User
{
    // 属性是只读的或者只能被内部的代码修改。
    // init 表示只能在对象初始化时进行赋值
    public int Id { get; init; }

    public DateTime CreatedDateTime { get; init; }

    // 只能在类内部访问
    public string UserName { get; private set; }

    public int Credit { get; set; }

    // 没有对应属性，但是这些成员变量需要映射为数据表中的列，也就是我们需要把私有成员变量映射到数据表中的列
    private string? passwordHash { get; set; }

    // [Column("Remark")]
    // 字段
    private string? remark;
    //[NotMapped]
    // 只读属性，也就是它的值是从数据库中读取出来的，但是我们不能修改属性值
    public string? Remark
    {
        get { return remark; }
    }

    // 有的属性不需要映射到数据列，仅在运行时被使用。
    public string? Tag { get; set; }

    // 给 EFCore 框架从数据库中加载数据然后生成 User 对象时赋值返回用的：EFCore 可以访问编译生成的后台字段
    private User()
    {

    }

    // 定义有参数的构造方法：供程序使用
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
            throw new ArgumentException("用户名长度不能大于5");
        }
        UserName = newValue;
    }

    public void ChangePassword(string newValue)
    {
        if (newValue.Length < 6)
        {
            throw new ArgumentException("密码长度不能小于6");
        }
        passwordHash = HashHelper.ComputeSha256Hash(newValue);
    }

}