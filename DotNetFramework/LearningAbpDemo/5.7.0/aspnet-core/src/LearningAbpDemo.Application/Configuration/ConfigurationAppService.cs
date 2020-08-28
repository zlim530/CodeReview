using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using LearningAbpDemo.Configuration.Dto;

namespace LearningAbpDemo.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LearningAbpDemoAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
