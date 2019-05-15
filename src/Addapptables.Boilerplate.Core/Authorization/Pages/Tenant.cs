using Abp.Authorization;
using Abp.MultiTenancy;

namespace Addapptables.Boilerplate.Authorization.Pages
{
    public class Tenant : PageAbstract
    {
        public const string Pages_Tenants = "Pages.Tenants";

        public const string Pages_Tenants_Create = "Pages.Tenants.Create";

        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";

        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";

        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";

        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public Tenant(Permission page, bool isMultiTenancyEnabled) : base(page, isMultiTenancyEnabled)
        {
        }

        public override void CreateChildPermission()
        {
            var tenants = Page.CreateChildPermission(Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);
        }
    }
}
