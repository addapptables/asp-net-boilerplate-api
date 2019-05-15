using Abp.Organizations;
using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Organizations.Dto
{
    public class CreateOrganizationUnitDto
    {
        public long? ParentId { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

    }
}
