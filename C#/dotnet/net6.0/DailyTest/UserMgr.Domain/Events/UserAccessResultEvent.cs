using MediatR;
using UserMgr.Domain.Enums;
using UserMgr.Domain.ValueObjects;

namespace UserMgr.Domain.Events
{
    /// <summary>
    /// 事件发布模型类：用于事件数据传递
    /// </summary>
    public record UserAccessResultEvent(PhoneNumber PhoneNumber, UserAccessResult Result) : INotification;
}
