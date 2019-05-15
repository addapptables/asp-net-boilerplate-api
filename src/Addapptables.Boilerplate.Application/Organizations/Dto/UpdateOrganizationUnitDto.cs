using Abp.Application.Services.Dto;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Addapptables.Boilerplate.Organizations.Dto
{
    public class UpdateOrganizationUnitDto : IEntityDto<long>
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
    }
}
