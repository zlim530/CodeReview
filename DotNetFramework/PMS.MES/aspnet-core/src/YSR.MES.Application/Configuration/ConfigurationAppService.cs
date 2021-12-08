﻿using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using YSR.MES.Configuration.Dto;

namespace YSR.MES.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MESAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
