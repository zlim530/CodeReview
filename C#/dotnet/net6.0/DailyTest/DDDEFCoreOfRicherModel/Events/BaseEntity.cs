using DDDEFCoreOfRicherModel.Events;
using MediatR;

namespace DDDEFCoreOfRicherModel;

/// <summary>
/// ���оۺϸ�ʵ��̳� BaseEntity ����
/// </summary>
public record BaseEntity : IDomainEvents
{
    private IList<INotification> DomainEvents = new List<INotification>();

    public void AddDomainEvent(INotification eventItem)
    {
        DomainEvents.Add(eventItem);
    }

    public void AddDomainEventIfAbsent(INotification eventItem)
    {
        if (!DomainEvents.Contains(eventItem))
        {
            DomainEvents.Add(eventItem);
        }
    }

    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }

    public IEnumerable<INotification> GetDomainEvents()
    {
        return DomainEvents;
    }
}