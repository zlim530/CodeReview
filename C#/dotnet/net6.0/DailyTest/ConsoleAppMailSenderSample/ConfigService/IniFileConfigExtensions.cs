using ConfigService;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class IniFileConfigExtensions
	{
		public static void AddIniFileConfig(this IServiceCollection services, string filePath)
		{
			services.AddScoped(typeof(IConfigProvider), s => new IniFileConfigProvider { FilePath = filePath});
		}
	}
}