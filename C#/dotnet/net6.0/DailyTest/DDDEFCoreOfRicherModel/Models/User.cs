using DDDEFCoreOfRicherModel.Events;
using System.ComponentModel.DataAnnotations.Schema;
using Zack.Commons;

namespace DDDEFCoreOfRicherModel.Models;

/// <summary>
/// EFCore 中实现充血模型
/// 实体中的逻辑代码：管理实体的创建、状态等非业务逻辑
/// 领域服务：聚合内的业务逻辑
/// 应用服务：聚合之间，以及和外部系统间的业务逻辑
/// 聚合：高内聚，低耦合
/// 把关系强的实体放在同一个聚合中，把其中一个实体做为“聚合根”，对于同一个聚合内的其他实体，都通过聚合根实体进行操作
/// 划分聚合，是为了便于以后进行微服务的拆分
/// 跨聚合进行实体引用，只能引用根实体（聚合根），并且只能引用实体的标识符，而不能引用实体根对象
/// </summary>
public record User : BaseEntity
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
        AddDomainEvent(new NewUserInfoNotification(UserName, this.CreatedDateTime));// 注册事件
    }

    public void ChangeUserName(string newValue)
    {
        if (newValue.Length > 5)
        {
            throw new ArgumentException("用户名长度不能大于5");
        }
        string oldName = UserName;
        UserName = newValue;
        AddDomainEvent(new UserNameChangeNotification(oldName, UserName));
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