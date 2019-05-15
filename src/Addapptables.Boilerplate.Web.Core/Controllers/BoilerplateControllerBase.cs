using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Addapptables.Boilerplate.Controllers
{
    public abstract class BoilerplateControllerBase: AbpController
    {
        protected BoilerplateControllerBase()
        {
            LocalizationSourceName = BoilerplateConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
