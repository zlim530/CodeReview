using Zack.EventBus;

namespace ASP.NETCoreWebAPIDemo.EventBus;

[EventName("OrderCreated")]// ʹ��ǿ���ʹ�����ղ����������Ϣ
public class MyEventHandler2 : JsonIntegrationEventHandler<OrderData>
{
    public override Task HandleJson(string eventName, OrderData? eventData)
    {
        Console.WriteLine($"����΢����2�� EventHandler2���յ��˶�����Id Ϊ��{eventData.Id}������Ϊ��{eventData.Name}");
        return Task.CompletedTask;
    }
}

public record OrderData(long Id, string Name, DateTime Date);
