using ConfigService;
using LogService;
using MailService;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ConsoleAppMailSenderSample
{
    internal class Program
    {
        static void Main0(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<ILogProvider, ConsoleLogProvider>();
            services.AddScoped<IConfigProvider, EnvVarConfigProvider>();
            //services.AddScoped(typeof(IConfigProvider), s => new IniFileConfigProvider { FilePath = "mail.ini"});
            services.AddIniFileConfig("mail.ini");// 通过扩展方法实现 AddXXX：用 ServiceCollection 对象可以自动提示出来，并且这种方法可以不用让服务实现类用 public 关键字修饰
            services.AddLayeredConfig();
            services.AddScoped<IMailProvider, MailProvider>();

            using (var sp = services.BuildServiceProvider())
            {
                var mailService = sp.GetRequiredService<IMailProvider>();
                mailService.Send("Hello","trump@usa.gov","Hello Trump");
            }
        }

        /// <summary>
        /// RabbitMQ 消费者控制台程序 Demo
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var connFactory = new ConnectionFactory();
            connFactory.HostName = "127.0.0.1";
            connFactory.DispatchConsumersAsync = true;
            using var connection = connFactory.CreateConnection();
            string exchangeName = "exchange1";
            string queueName = "queue1";
            string routingkey = "myEventKey1";
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchangeName, "direct");
            channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queueName, exchangeName, routingkey);

            AsyncEventingBasicConsumer consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queueName, autoAck: false, consumer: consumer);
            Console.WriteLine("按回车退出");// 因为是控制台程序，所以要在这里认为暂停，否则程序执行完就直接关闭了
            Console.ReadLine();
            async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
            {
                try
                {
                    byte[] body = eventArgs.Body.ToArray();
                    string text = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Received msg: {text}");
                    // DeliveryTag 就是消息的编号；BasicAck：人为告诉交换机消费者确认接收到指定的消息
                    channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
                    await Task.Delay(800);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("处理收到的消息出错:" + ex);
                    channel.BasicReject(eventArgs.DeliveryTag, true);
                }
            }
        }
    }
}