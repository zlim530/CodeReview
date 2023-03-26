using UserMgr.Domain.ValueObjects;

namespace UserMgr.Domain.Interfaces
{
    /// <summary>
    /// 发送短信的防腐层接口
    /// </summary>
    public interface ISmsCodeSender
    {
        Task SendCodeAsync(PhoneNumber phoneNumber, string code);
    }
}
