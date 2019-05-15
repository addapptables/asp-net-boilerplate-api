using Abp.MultiTenancy;
using Addapptables.Boilerplate.Url;

namespace Addapptables.Boilerplate.Web.Host.Url
{
    public class AngularAppUrlService : AppUrlServiceBase
    {
        public override string EmailActivationRoute => "confirm-email";

        public override string PasswordResetRoute => "reset-password";

        public AngularAppUrlService(
                IWebUrlService webUrlService,
                ITenantCache tenantCache
            ) : base(
                webUrlService,
                tenantCache
            )
        {

        }
    }
}
