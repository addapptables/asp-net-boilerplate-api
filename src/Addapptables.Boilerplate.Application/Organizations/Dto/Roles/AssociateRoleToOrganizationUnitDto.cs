using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Roles;

namespace Addapptables.Boilerplate.Organizations.Dto.Roles
{
    [AutoMapFrom(typeof(Role))]
    public class AssociateRoleToOrganizationUnitDto: EntityDto
    {
        public string NormalizedName { get; set; }

        public long OrganizationUnitId { get; set; }
    }
}
