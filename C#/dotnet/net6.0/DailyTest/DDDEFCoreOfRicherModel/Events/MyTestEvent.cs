using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

// ����һ������Ϣ�ķ����ߺʹ�����֮��������ݴ��ݵ��࣬�������Ҫʵ��INotification�ӿڡ�һ����record����
public record MyTestEvent(string Body) : INotification;