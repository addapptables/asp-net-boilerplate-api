using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Roles;

namespace Addapptables.Boilerplate.Roles.Dto
{
    [AutoMapTo(typeof(Role))]
    public class RoleEditDto : CreateRoleDto, IEntityDto<int>
    {
        public int Id { get; set; }
    }
}