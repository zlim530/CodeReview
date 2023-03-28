using MediatR;
using UserMgr.Domain.Events;
using UserMgr.Domain.Interfaces;
using UserMgr.Infrastracture.DbContexts;

namespace UserMgr.WebAPI.Events;

/// <summary>
/// 监听登录失败或者成功的领域事件 UserAccessResultEvent，记录到 UserLoginHistory
/// </summary>
public class UserAccessResultEventHandler : INotificationHandler<UserAccessResultEvent>
{
    private readonly IUserDomainRepository userDomainRepository;
    private readonly UserDbContext userDbContext;

    public UserAccessResultEventHandler(IUserDomainRepository userDomainRepository, UserDbContext userDbContext)
    {
        this.userDomainRepository = userDomainRepository;
        this.userDbContext = userDbContext;
    }

    public async Task Handle(UserAccessResultEvent notification, CancellationToken cancellationToken)
    {
        await userDomainRepository.AddNewLoginHistoryAsync(notification.PhoneNumber, $"登录结果是：{notification.Result}");
        await userDbContext.SaveChangesAsync();// 因为这不是 Action 方法，所以需要手动调用 SaveChangeAsync() 方法保存更改
    }
}