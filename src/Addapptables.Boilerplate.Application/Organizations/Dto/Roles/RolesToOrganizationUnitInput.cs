using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Organizations.Dto.Roles
{
    public class RolesToOrganizationUnitInput
    {
        public int[] RoleIds { get; set; }

        [Range(1, long.MaxValue)]
        public long OrganizationUnitId { get; set; }
    }
}
