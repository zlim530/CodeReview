using RabbitMQ.Client;
using System.Text;

namespace DailyTest;

/// <summary>
/// �����¼���΢�����ھۺ�֮����¼����ݣ�ʹ�� MediatR
/// �����¼��ǿ�΢������¼����ݣ�ʹ�� RabbitMQ 
/// </summary>
public class RabbitMQSendDemoTest
{
    static void Main(string[] args)
    {
        var connFactory = new ConnectionFactory();
        connFactory.HostName = "127.0.0.1"; // ���� RabbitMQ ���ԣ�����ֱ��ʹ�ñ������ƣ���������������д RabbitMQ ����װ�ķ����� IP ��ַ����
        connFactory.DispatchConsumersAsync = true;
        var connection = connFactory.CreateConnection();
        string exchangeName = "exchange1"; // ������������
        string eventName = "myEventKey1"; // routingKey ��ֵ

        // ���Է���3����Ϣ����
        for (int i = 0; i < 3; i++)
        {
            string msg = DateTime.Now.TimeOfDay.ToString();// ��������Ϣ
            using var channel = connection.CreateModel(); // �����ŵ�����ͬһ�� TCP �����п����ж���ŵ�����������ŵ����Ը���ͬһ�� TCP ����
            var prop = channel.CreateBasicProperties();
            prop.DeliveryMode = 2;// �־û�ģʽ
            channel.ExchangeDeclare(exchangeName, "direct"); // ����������
            byte[] body = Encoding.UTF8.GetBytes(msg);
            channel.BasicPublish(exchangeName, routingKey: eventName, mandatory: true, basicProperties: prop, body: body); // ������Ϣ
            Console.WriteLine($"send done: {msg}");
            Thread.Sleep(1000);// ÿ��1�뷢�͵�ǰʱ�䵽��������
        }

    }
}