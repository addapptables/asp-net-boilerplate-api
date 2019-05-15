using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Auditing;
using Addapptables.Boilerplate.Sessions.Dto;

namespace Addapptables.Boilerplate.Sessions
{
    public class SessionAppService : BoilerplateAppServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                ImpersonatorUserId = AbpSession.ImpersonatorUserId,
                ImpersonatorTenantId = AbpSession.ImpersonatorTenantId,
                Application = new ApplicationInfoDto
                {
                    Version = AppVersionHelper.Version,
                    ReleaseDate = AppVersionHelper.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                var currentUser = await GetCurrentUserAsync();
                output.User = ObjectMapper.Map<UserLoginInfoDto>(currentUser);
                var grantedPermissions = await UserManager.GetGrantedPermissionsAsync(currentUser);
                output.User.Permissions = grantedPermissions.Select(x => x.Name).ToList();
            }

            return output;
        }
    }
}
