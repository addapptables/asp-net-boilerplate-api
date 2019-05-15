using Abp.Authorization;
using Abp.Localization;
using Addapptables.Boilerplate.Authorization.Pages.Administration;
using System.Collections.Generic;

namespace Addapptables.Boilerplate.Authorization.Pages
{
    public class BuildPage
    {
        public const string Pages = "Pages";

        public List<PageAbstract> Childrens { get; set; }

        public BuildPage(IPermissionDefinitionContext context, bool isMultiTenancyEnabled)
        {
            var page = context.GetPermissionOrNull(Pages) ?? context.CreatePermission(Pages, L("Pages"));
            Childrens = new List<PageAbstract>();
            Childrens.Add(new Edition(page, isMultiTenancyEnabled));
            Childrens.Add(new Tenant(page, isMultiTenancyEnabled));
            Childrens.Add(new BuildAdministration(page, isMultiTenancyEnabled));
        }

        public void BuildPages()
        {
            foreach (var children in Childrens)
            {
                children.CreateChildPermission();
            }
        }

        protected static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BoilerplateConsts.LocalizationSourceName);
        }
    }
}
