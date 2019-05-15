using Abp.Authorization;
using System.Collections.Generic;

namespace Addapptables.Boilerplate.Authorization.Pages.Administration
{
    public class BuildAdministration : PageAbstract
    {
        public const string Pages_Administration = "Pages.Administration";

        public List<PageAbstract> Childrens { get; set; }


        public BuildAdministration(Permission page, bool isMultiTenancyEnabled) : base(page, isMultiTenancyEnabled)
        {
            var administration = page.CreateChildPermission(Pages_Administration, L("Administration"));
            Childrens = new List<PageAbstract>();
            Childrens.Add(new User(administration, isMultiTenancyEnabled));
            Childrens.Add(new Role(administration, isMultiTenancyEnabled));
            Childrens.Add(new OrganizationUnits(administration, isMultiTenancyEnabled));
        }

        public override void CreateChildPermission()
        {
            Childrens.ForEach(x => x.CreateChildPermission());
        }
    }
}
