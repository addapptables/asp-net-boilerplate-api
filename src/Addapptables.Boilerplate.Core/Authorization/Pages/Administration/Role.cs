using Abp.Authorization;

namespace Addapptables.Boilerplate.Authorization.Pages.Administration
{
    public class Role : PageAbstract
    {

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";

        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";

        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";

        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public Role(Permission page, bool isMultiTenancyEnabled) : base(page, isMultiTenancyEnabled)
        {
        }

        public override void CreateChildPermission()
        {
            var page = Page.CreateChildPermission(Pages_Administration_Roles, L("Roles"));
            page.CreateChildPermission(Pages_Administration_Roles_Create, L("CreatingNewRole"));
            page.CreateChildPermission(Pages_Administration_Roles_Edit, L("EditingRole"));
            page.CreateChildPermission(Pages_Administration_Roles_Delete, L("DeletingRole"));
        }
    }
}
