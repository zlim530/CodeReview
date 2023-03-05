using IdentitySeverDemo.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace IdentitySeverDemo.Hubs;

// SignalR �������֤������ Identity �� JWT
//[Authorize]
/// <summary>
/// �������� Hub ��ִ�г�����Ϣ�ַ�֮���ҵ���߼�����
/// </summary>
public class MyHub : Hub
{
    private readonly UserManager<MyUser> userManager;

    public MyHub(UserManager<MyUser> userManager)
    {
        this.userManager = userManager;
    }

    /// <summary>
    /// ��ָ���û�������Ϣ��ʵ��˽�ĵ�Ч��
    /// </summary>
    /// <param name="toUserName"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    public async Task SendPrivateMsg(string toUserName, string msg)
    {
        var user = await userManager.FindByNameAsync(toUserName);
        long userId = user.Id;
        // ��ȡ��ǰ�û����û���
        string currentUserName = this.Context.User.Identity.Name;
        await this.Clients.User(userId.ToString()).SendAsync("PrivateMsgRecevied", currentUserName, msg);
    }

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