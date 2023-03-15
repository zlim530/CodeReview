using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

// ��Ϣ�Ĵ�����Ҫʵ�� NotificationHandler �ӿڣ����еķ��Ͳ��� TNotification �������Ϣ������Ҫ�������Ϣ���͡�
public class MyTestEventHandler : INotificationHandler<MyTestEvent>
{
    public Task Handle(MyTestEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"MyTestEventHandler Received {notification.Body}");
        return Task.CompletedTask;
    }
}