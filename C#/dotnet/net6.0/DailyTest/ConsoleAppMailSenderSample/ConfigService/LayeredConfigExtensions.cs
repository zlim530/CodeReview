using ConfigService;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class LayeredConfigExtensions
	{
		public static void AddLayeredConfig(this IServiceCollection services)
		{
			services.AddScoped<IConfigReader, LayeredConfigProvider>();
		}
	}
}