using Addapptables.Boilerplate.Dto;
using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Organizations.Dto
{
    public class FindOrganizationUnitUsersInput : PagedAndFilteredInputDto
    {
        [Range(1, long.MaxValue)]
        public long OrganizationUnitId { get; set; }
    }
}
