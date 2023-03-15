using MediatR;

namespace DDDEFCoreOfRicherModel.Events;

// 定义一个在消息的发布者和处理者之间进行数据传递的类，这个类需要实现INotification接口。一般用record类型
public record MyTestEvent(string Body) : INotification;