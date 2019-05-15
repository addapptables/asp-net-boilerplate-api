using System.Collections.Generic;
using System.Linq;
using Abp.Authorization;
using Addapptables.Boilerplate.Roles.Dto;

namespace Addapptables.Boilerplate.Permissions
{
    public class PermissionAppService : BoilerplateAppServiceBase, IPermissionAppService
    {
        public PermissionAppService() { }

        [AbpAuthorize()]
        public IList<FlatPermissionDto> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();
            return ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList();
        }
    }
}
