using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

/// <summary>
/// �������¼��ķ����ӳٵ������ı����޸�ʱ��ʵ����ֻ��ע��Ҫ�����������¼���Ȼ���������� ��SaveChanges����������ʱ�������ٷ����¼���
/// ���ۺϸ������¼�ע��Ľӿ�IDomainEvents
/// </summary>
public interface IDomainEvents
{
    IEnumerable<INotification> GetDomainEvents();

    void AddDomainEvent(INotification eventItem);

    void AddDomainEventIfAbsent(INotification eventItem);

    void ClearDomainEvents();
}