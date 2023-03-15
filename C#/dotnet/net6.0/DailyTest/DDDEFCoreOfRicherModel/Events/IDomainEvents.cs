using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

/// <summary>
/// 把领域事件的发布延迟到上下文保存修改时。实体中只是注册要发布的领域事件，然后在上下文 的SaveChanges方法被调用时，我们再发布事件。
/// 供聚合根进行事件注册的接口IDomainEvents
/// </summary>
public interface IDomainEvents
{
    IEnumerable<INotification> GetDomainEvents();

    void AddDomainEvent(INotification eventItem);

    void AddDomainEventIfAbsent(INotification eventItem);

    void ClearDomainEvents();
}