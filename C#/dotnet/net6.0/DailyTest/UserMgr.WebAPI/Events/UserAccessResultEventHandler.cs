using MediatR;
using UserMgr.Domain.Events;
using UserMgr.Domain.Interfaces;
using UserMgr.Infrastracture.DbContexts;

namespace UserMgr.WebAPI.Events;

/// <summary>
/// ������¼ʧ�ܻ��߳ɹ��������¼� UserAccessResultEvent����¼�� UserLoginHistory
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
        await userDomainRepository.AddNewLoginHistoryAsync(notification.PhoneNumber, $"��¼����ǣ�{notification.Result}");
        await userDbContext.SaveChangesAsync();// ��Ϊ�ⲻ�� Action ������������Ҫ�ֶ����� SaveChangeAsync() �����������
    }
}