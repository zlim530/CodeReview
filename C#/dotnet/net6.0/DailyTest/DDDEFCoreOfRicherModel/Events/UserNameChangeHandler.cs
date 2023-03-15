using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

public class UserNameChangeHandler : INotificationHandler<UserNameChangeNotification>
{
    public Task Handle(UserNameChangeNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Update UserName from {notification.OldName} to {notification.NewName}");
        return Task.CompletedTask;
    }
}