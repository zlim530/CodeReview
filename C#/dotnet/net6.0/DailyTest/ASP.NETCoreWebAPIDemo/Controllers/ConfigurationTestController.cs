using ASP.NETCoreWebAPIDemo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace ASP.NETCoreWebAPIDemo;

[Route("api/[controller]/[action]")]
[ApiController]
public class ConfigurationTestController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostConfiguration;
	private readonly IOptionsSnapshot<SmtpSettings> _optionsSnapshot;
	private readonly IConnectionMultiplexer _connectionMultiplexer;

	/// <summary>
	/// �ڿ������л�ȡ����������ֵ��ʹ�ù��캯������ע��ķ�ʽ
	/// </summary>
	/// <param name="webHostEnvironment"></param>
	public ConfigurationTestController(IWebHostEnvironment webHostEnvironment,
		IOptionsSnapshot<SmtpSettings> optionsSnapshot,
		IConnectionMultiplexer connectionMultiplexer)
	{
		_webHostConfiguration= webHostEnvironment;
		_optionsSnapshot= optionsSnapshot;
		_connectionMultiplexer = connectionMultiplexer;
	}

	[HttpGet]
	public string GetRedisAndSmtp()
	{
		var ping = _connectionMultiplexer.GetDatabase(0).Ping();
		return _optionsSnapshot.Value.ToString() + " " + ping;
	}

	[HttpGet]
	public string GetEnv1()
	{
		return _webHostConfiguration.EnvironmentName;
	}
}