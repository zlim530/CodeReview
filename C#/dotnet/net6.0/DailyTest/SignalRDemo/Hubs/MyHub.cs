using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs;

public class MyHub : Hub
{
    /// <summary>
    /// ���������ֻ��һ�� Async ��������ֱ�ӷ����ˣ�����Ҫ�ڷ���ǰ��д await����Ӧ�ķ������� Task ǰ��Ҳ��Ҫ��ע async
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task SendPublicMsg(string message)
    {
        string connId = Context.ConnectionId;
        string msgToSend = $"{connId} - {DateTime.Now} : {message}";
        return Clients.All.SendAsync("PublicMsgReceived", msgToSend);// �������˰���ϢȺ�������пͻ���
    }
}