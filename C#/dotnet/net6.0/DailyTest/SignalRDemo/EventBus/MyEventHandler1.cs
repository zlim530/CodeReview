using Zack.EventBus;

namespace SignalRDemo.EventBus;

[EventName("OrderCreated")]
[EventName("OrderAdded")]// 同一个 EventHandler 可以监听多个事件
// 同一个微服务内部的 Event 也可以被内部的自定义的 EventHandler 监听
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