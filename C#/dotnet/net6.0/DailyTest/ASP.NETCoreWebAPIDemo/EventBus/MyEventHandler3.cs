using Zack.EventBus;

namespace ASP.NETCoreWebAPIDemo.EventBus;

[EventName("OrderCreated")]// 使用动态类型处理接收并处理队列消息
public class MyEventHandler3 : DynamicIntegrationEventHandler
{
    public override Task HandleDynamic(string eventName, dynamic eventData)
    {
        Console.WriteLine($"我是微服务2的 EventHandler3，Dynamic 收到了订单，Id 为：{eventData.Id}，名称为：{eventData.Name}");
        return Task.CompletedTask;
    }
}