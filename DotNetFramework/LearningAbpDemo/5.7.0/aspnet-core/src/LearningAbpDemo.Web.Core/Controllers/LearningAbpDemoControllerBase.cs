using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace LearningAbpDemo.Controllers
{
    public abstract class LearningAbpDemoControllerBase: AbpController
    {
        protected LearningAbpDemoControllerBase()
        {
            LocalizationSourceName = LearningAbpDemoConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
