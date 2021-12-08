using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace YSR.MES.Controllers
{
    public abstract class MESControllerBase: AbpController
    {
        protected MESControllerBase()
        {
            LocalizationSourceName = MESConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
