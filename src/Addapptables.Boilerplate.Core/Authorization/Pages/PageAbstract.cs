using Abp.Authorization;
using Abp.Localization;

namespace Addapptables.Boilerplate.Authorization.Pages
{
    public abstract class PageAbstract
    {
        private bool isMultiTenancyEnabled;
        protected bool IsMultiTenancyEnabled { get => isMultiTenancyEnabled; set => isMultiTenancyEnabled = value; }

        private Permission page;
        protected Permission Page { get => page; set => page = value; }

        public PageAbstract(Permission page, bool isMultiTenancyEnabled)
        {
            Page = page;
            IsMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public abstract void CreateChildPermission();

        protected static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BoilerplateConsts.LocalizationSourceName);
        }
    }
}
