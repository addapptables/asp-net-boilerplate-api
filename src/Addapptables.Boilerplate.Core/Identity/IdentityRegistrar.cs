using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Addapptables.Boilerplate.Authorization;
using Addapptables.Boilerplate.Authorization.Roles;
using Addapptables.Boilerplate.Authorization.Users;
using Addapptables.Boilerplate.Editions;
using Addapptables.Boilerplate.MultiTenancy;

namespace Addapptables.Boilerplate.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpLogInManager<LogInManager>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddPermissionChecker<PermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
