using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Roles;
using System;

namespace Addapptables.Boilerplate.Organizations.Dto.Roles
{
    [AutoMapFrom(typeof(Role))]
    public class OrganizationUnitRolesListDto : EntityDto<long>
    {
        public string NormalizedName { get; set; }

        public int RoleId { get; set; }

        public long OrganizationUnitId { get; set; }

        public DateTime AddedTime { get; set; }
    }
}
