using DDDEFCoreOfRicherModel.Events;
using MediatR;

namespace DDDEFCoreOfRicherModel.Controllers;

public class TestController
{
    private readonly IMediator mediator;

	public TestController(IMediator mediator)
	{
		this.mediator = mediator;
	}

    //����Ҫ������Ϣ�ĵ�����ע��IMediator���͵ķ���Ȼ�����ǵ���Publish������������Ϣ��Send()��������������һ��һ��Ϣ�ģ���Publish()��������������һ�Զ���Ϣ�ġ�
    public async void TestAsync(string body)
	{
		await mediator.Publish(new MyTestEvent(body + DateTime.Now));
		Console.WriteLine("OK");
	}
}