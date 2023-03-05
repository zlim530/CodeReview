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
		// 为了能在后台运行英汉字典导入，所以在这里丢弃也即故意不等待此方法的执行
		_ = importExecutor.ExecuteAsync(this.Context.ConnectionId);
		return Task.CompletedTask;
	}
}