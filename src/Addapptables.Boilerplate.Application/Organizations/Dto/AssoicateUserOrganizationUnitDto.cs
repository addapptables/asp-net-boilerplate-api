using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Users;
using Addapptables.Boilerplate.Users.Dto;

namespace Addapptables.Boilerplate.Organizations.Dto
{
    [AutoMapFrom(typeof(User))]
    public class AssociateUserOrganizationUnitDto : UserMinimalDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
