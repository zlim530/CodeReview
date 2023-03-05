using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Helpers;

namespace SignalRDemo.Hubs;

public class ImportHub : Hub
{
    private readonly ImportExecutor importExecutor;

	public ImportHub(ImportExecutor importExecutor)
	{
		this.importExecutor = importExecutor;
	}

	public Task ImportEcDict()
	{
		// Ϊ�����ں�̨����Ӣ���ֵ䵼�룬���������ﶪ��Ҳ�����ⲻ�ȴ��˷�����ִ��
		_ = importExecutor.ExecuteAsync(this.Context.ConnectionId);
		return Task.CompletedTask;
	}
}