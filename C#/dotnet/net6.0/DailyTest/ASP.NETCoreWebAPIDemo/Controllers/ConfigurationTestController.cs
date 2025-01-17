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
	private readonly IOptions<SmtpSettings> _options;
	private readonly IConnectionMultiplexer _connectionMultiplexer;

	/// <summary>
	/// 在控制器中获取环境变量的值：使用构造函数依赖注入的方式
	/// </summary>
	/// <param name="webHostEnvironment"></param>
	public ConfigurationTestController(IWebHostEnvironment webHostEnvironment,
		IOptions<SmtpSettings> options,
		IConnectionMultiplexer connectionMultiplexer)
	{
		_webHostConfiguration= webHostEnvironment;
		_options= options;
		_connectionMultiplexer = connectionMultiplexer;
	}

	[HttpGet]
	public string GetRedisAndSmtp()
	{
		var ping = _connectionMultiplexer.GetDatabase(0).Ping();
		return _options.Value.ToString() + " " + ping;
	}

	[HttpGet]
	public string GetEnv1()
	{
		return _webHostConfiguration.EnvironmentName;
	}
}