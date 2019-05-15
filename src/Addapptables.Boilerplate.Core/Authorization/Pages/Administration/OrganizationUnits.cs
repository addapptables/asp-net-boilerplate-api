using Abp.Authorization;

namespace Addapptables.Boilerplate.Authorization.Pages.Administration
{
    public class OrganizationUnits : PageAbstract
    {

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";

        public const string Pages_Administration_OrganizationUnits_Create = "Pages.Administration.OrganizationUnits.Create";

        public const string Pages_Administration_OrganizationUnits_Edit = "Pages.Administration.OrganizationUnits.Edit";

        public const string Pages_Administration_OrganizationUnits_Delete = "Pages.Administration.OrganizationUnits.Delete";

        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";

        public OrganizationUnits(Permission page, bool isMultiTenancyEnabled) : base(page, isMultiTenancyEnabled)
        {
        }

        public override void CreateChildPermission()
        {
            var page = Page.CreateChildPermission(Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            page.CreateChildPermission(Pages_Administration_OrganizationUnits_Create, L("CreatingNewOrganizationUnit"));
            page.CreateChildPermission(Pages_Administration_OrganizationUnits_Edit, L("EditingOrganizationUnit"));
            page.CreateChildPermission(Pages_Administration_OrganizationUnits_Delete, L("DeletingOrganizationUnit"));
            page.CreateChildPermission(Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembersOrganizationUnit"));
        }
    }
}
