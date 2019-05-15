using Addapptables.Boilerplate.Dto;
using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Organizations.Dto.Roles
{
    public class FindOrganizationUnitRolesInput : PagedAndFilteredInputDto
    {
        [Range(1, long.MaxValue)]
        public long OrganizationUnitId { get; set; }
    }
}
