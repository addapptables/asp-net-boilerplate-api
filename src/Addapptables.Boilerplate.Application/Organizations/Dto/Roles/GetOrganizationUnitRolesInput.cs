using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Addapptables.Boilerplate.Organizations.Dto.Roles
{
    public class GetOrganizationUnitRolesInput : PagedResultRequestDto
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }
    }
}
