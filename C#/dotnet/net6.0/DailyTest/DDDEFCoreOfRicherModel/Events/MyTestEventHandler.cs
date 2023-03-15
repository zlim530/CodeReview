using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

// 消息的处理者要实现 NotificationHandler 接口，其中的泛型参数 TNotification 代表此消息处理者要处理的消息类型。
public class MyTestEventHandler : INotificationHandler<MyTestEvent>
{
    public Task Handle(MyTestEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"MyTestEventHandler Received {notification.Body}");
        return Task.CompletedTask;
    }
}