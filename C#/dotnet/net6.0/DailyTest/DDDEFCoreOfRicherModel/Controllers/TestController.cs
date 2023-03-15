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

    //在需要发布消息的的类中注入IMediator类型的服务，然后我们调用Publish方法来发布消息。Send()方法是用来发布一对一消息的，而Publish()方法是用来发布一对多消息的。
    public async void TestAsync(string body)
	{
		await mediator.Publish(new MyTestEvent(body + DateTime.Now));
		Console.WriteLine("OK");
	}
}