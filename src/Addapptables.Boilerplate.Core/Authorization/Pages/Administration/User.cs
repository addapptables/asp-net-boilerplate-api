using Abp.Authorization;

namespace Addapptables.Boilerplate.Authorization.Pages.Administration
{
    public class User : PageAbstract
    {

        public const string Pages_Administration_Users = "Pages.Administration.Users";

        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";

        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";

        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";

        public User(Permission page, bool isMultiTenancyEnabled) : base(page, isMultiTenancyEnabled)
        {
        }

        public override void CreateChildPermission()
        {
            var users = Page.CreateChildPermission(Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(Pages_Administration_Users_Delete, L("DeletingUser"));
        }
    }
}
