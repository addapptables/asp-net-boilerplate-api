using Addapptables.Boilerplate.Authorization.Roles;
using Addapptables.Boilerplate.Authorization.Users;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Abp.IdentityFramework;
using Addapptables.Boilerplate.MultiTenancy.Rules.Models;

namespace Addapptables.Boilerplate.MultiTenancy.Rules
{
    public class RuleTenantDefaultUser : AddapptablesDomainServiceBase, IRuleAfterSaveTenant
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;

        public RuleTenantDefaultUser(
            RoleManager roleManager,
            UserManager userManager
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ApplyRules(Tenant tenant, TenantModel input)
        {
            using (CurrentUnitOfWork.SetTenantId(tenant.Id))
            {
                // Create static roles for new tenant
                CheckErrors(await _roleManager.CreateStaticRoles(tenant.Id));

                await CurrentUnitOfWork.SaveChangesAsync(); // To get static role ids

                // Grant all permissions to admin role
                var adminRole = _roleManager.Roles.Single(r => r.Name == StaticRoleNames.Tenants.Admin);
                await _roleManager.GrantAllPermissionsAsync(adminRole);

                // Create admin user for the tenant
                var adminUser = User.CreateTenantAdminUser(tenant.Id, input.AdminEmailAddress);
                adminUser.IsActive = true;
                await _userManager.InitializeOptionsAsync(tenant.Id);
                CheckErrors(await _userManager.CreateAsync(adminUser, User.DefaultPassword));
                await CurrentUnitOfWork.SaveChangesAsync(); // To get admin user's id

                // Assign admin user to role!
                CheckErrors(await _userManager.AddToRoleAsync(adminUser, adminRole.Name));

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
