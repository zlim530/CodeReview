using Microsoft.AspNetCore.SignalR;

namespace SignalRDemo.Hubs;

public class MyHub : Hub
{
    /// <summary>
    /// 如果方法中只有一个 Async 方法并且直接返回了，则不需要在方法前面写 await，对应的方法声明 Task 前面也需要标注 async
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task SendPublicMsg(string message)
    {
        string connId = Context.ConnectionId;
        string msgToSend = $"{connId} - {DateTime.Now} : {message}";
        return Clients.All.SendAsync("PublicMsgReceived", msgToSend);// 服务器端把消息群发给所有客户端
    }
}