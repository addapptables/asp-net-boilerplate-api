using Abp.Authorization;
using Abp.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Addapptables.Boilerplate.Authorization.Pages
{
    public class Edition : PageAbstract
    {
        public const string Pages_Editions = "Pages.Editions";

        public const string Pages_Editions_Create = "Pages.Editions.Create";

        public const string Pages_Editions_Edit = "Pages.Editions.Edit";

        public const string Pages_Editions_Delete = "Pages.Editions.Delete";

        public Edition(Permission page, bool isMultiTenancyEnabled) : base(page, isMultiTenancyEnabled)
        {
        }

        public override void CreateChildPermission()
        {
            var editions = Page.CreateChildPermission(Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
        }
    }
}
