using Zack.EventBus;

namespace SignalRDemo.EventBus;

[EventName("OrderCreated")]
[EventName("OrderAdded")]// ͬһ�� EventHandler ���Լ�������¼�
// ͬһ��΢�����ڲ��� Event Ҳ���Ա��ڲ����Զ���� EventHandler ����
public class MyEventHandler1 : IIntegrationEventHandler
{
    public Task Handle(string eventName, string eventData)
    {
        if (eventName == "OrderCreated")
        {
            Console.WriteLine($"Recieved Order, OrderData = {eventData}");
        }

        return Task.CompletedTask;
    }
}