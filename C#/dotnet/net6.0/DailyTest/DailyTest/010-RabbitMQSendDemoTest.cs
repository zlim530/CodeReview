using RabbitMQ.Client;
using System.Text;

namespace DailyTest;

/// <summary>
/// 领域事件是微服务内聚合之间的事件传递：使用 MediatR
/// 集成事件是跨微服务的事件传递：使用 RabbitMQ 
/// </summary>
public class RabbitMQSendDemoTest
{
    static void Main(string[] args)
    {
        var connFactory = new ConnectionFactory();
        connFactory.HostName = "127.0.0.1"; // 本地 RabbitMQ 测试，所以直接使用本地名称，真是生产环境填写 RabbitMQ 所安装的服务器 IP 地址即可
        connFactory.DispatchConsumersAsync = true;
        var connection = connFactory.CreateConnection();
        string exchangeName = "exchange1"; // 交换机的名字
        string eventName = "myEventKey1"; // routingKey 的值

        // 测试发送3条消息数据
        for (int i = 0; i < 3; i++)
        {
            string msg = DateTime.Now.TimeOfDay.ToString();// 待发送消息
            using var channel = connection.CreateModel(); // 创建信道：在同一个 TCP 连接中可以有多个信道，多个虚拟信道可以复用同一个 TCP 连接
            var prop = channel.CreateBasicProperties();
            prop.DeliveryMode = 2;// 持久化模式
            channel.ExchangeDeclare(exchangeName, "direct"); // 声明交换机
            byte[] body = Encoding.UTF8.GetBytes(msg);
            channel.BasicPublish(exchangeName, routingKey: eventName, mandatory: true, basicProperties: prop, body: body); // 发布消息
            Console.WriteLine($"send done: {msg}");
            Thread.Sleep(1000);// 每隔1秒发送当前时间到交换机中
        }

    }
}