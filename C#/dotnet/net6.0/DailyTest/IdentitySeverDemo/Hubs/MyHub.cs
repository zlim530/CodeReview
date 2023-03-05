using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace IdentitySeverDemo.Hubs;

// SignalR 的身份认证：借助 Identity 和 JWT
//[Authorize]
/// <summary>
/// 不建议在 Hub 中执行除了消息分发之外的业务逻辑操作
/// </summary>
public class MyHub : Hub
{
    private readonly UserManager<MyUser> userManager;

    public MyHub(UserManager<MyUser> userManager)
    {
        this.userManager = userManager;
    }

    /// <summary>
    /// 向指定用户发送消息：实现私聊的效果
    /// </summary>
    /// <param name="toUserName"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public async Task SendPrivateMsg(string toUserName, string msg)
    {
        var user = await userManager.FindByNameAsync(toUserName);
        long userId = user.Id;
        // 获取当前用户的用户名
        string currentUserName = this.Context.User.Identity.Name;
        await this.Clients.User(userId.ToString()).SendAsync("PrivateMsgRecevied", currentUserName, msg);
    }

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