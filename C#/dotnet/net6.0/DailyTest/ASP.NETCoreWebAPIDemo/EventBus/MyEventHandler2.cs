using Zack.EventBus;

namespace ASP.NETCoreWebAPIDemo.EventBus;

[EventName("OrderCreated")]// 使用强类型处理接收并处理队列消息
public class MyEventHandler2 : JsonIntegrationEventHandler<OrderData>
{
    public override Task HandleJson(string eventName, OrderData? eventData)
    {
        Console.WriteLine($"我是微服务2的 EventHandler2，收到了订单。Id 为：{eventData.Id}，名称为：{eventData.Name}");
        return Task.CompletedTask;
    }
}

public record OrderData(long Id, string Name, DateTime Date);
