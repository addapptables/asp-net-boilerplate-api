using Abp.Application.Services;
using Addapptables.Boilerplate.Roles.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Addapptables.Boilerplate.Permissions
{
    public interface IPermissionAppService: IApplicationService
    {
        IList<FlatPermissionDto> GetAllPermissions();
    }
}
