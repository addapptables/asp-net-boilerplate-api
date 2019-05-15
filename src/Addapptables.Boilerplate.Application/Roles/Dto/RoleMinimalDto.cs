using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Roles;

namespace Addapptables.Boilerplate.Roles.Dto
{
    [AutoMap(typeof(Role))]
    public class RoleMinimalDto : EntityDto<int>
    {
        public string DisplayName { get; set; }

        public string NormalizedName { get; set; }
    }
}
