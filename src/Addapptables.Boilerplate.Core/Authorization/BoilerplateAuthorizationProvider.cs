using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Addapptables.Boilerplate.Authorization.Pages;

namespace Addapptables.Boilerplate.Authorization
{
    public class BoilerplateAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public BoilerplateAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public BoilerplateAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var buildPage = new BuildPage(context, _isMultiTenancyEnabled);
            buildPage.BuildPages();
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BoilerplateConsts.LocalizationSourceName);
        }
    }
}
