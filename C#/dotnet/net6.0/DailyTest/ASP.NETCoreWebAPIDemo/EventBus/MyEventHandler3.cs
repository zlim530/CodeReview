using Zack.EventBus;

namespace ASP.NETCoreWebAPIDemo.EventBus;

[EventName("OrderCreated")]// ʹ�ö�̬���ʹ�����ղ����������Ϣ
public class MyEventHandler3 : DynamicIntegrationEventHandler
{
    public override Task HandleDynamic(string eventName, dynamic eventData)
    {
        Console.WriteLine($"����΢����2�� EventHandler3��Dynamic �յ��˶�����Id Ϊ��{eventData.Id}������Ϊ��{eventData.Name}");
        return Task.CompletedTask;
    }
}