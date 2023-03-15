using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

public class NewUserInfoHandler : INotificationHandler<NewUserInfoNotification>
{
    public Task Handle(NewUserInfoNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"At {notification.CreatedDateTime}, Add New User, UserName is {notification.UserName}");
        return Task.CompletedTask;
    }
}