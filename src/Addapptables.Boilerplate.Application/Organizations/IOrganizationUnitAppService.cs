using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Addapptables.Boilerplate.Organizations.Dto;
using Addapptables.Boilerplate.Organizations.Dto.Roles;
using Addapptables.Boilerplate.Users.Dto;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.Organizations
{
    public interface IOrganizationUnitAppService : IAsyncCrudAppService<OrganizationUnitDto, long, PagedOrganizationUnitResultRequestDto, CreateOrganizationUnitDto, UpdateOrganizationUnitDto, OrganizationUnitDto>
    {
        Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits();

        Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);

        Task<PagedResultDto<OrganizationUnitRolesListDto>> GetOrganizationUnitRoles(GetOrganizationUnitUsersInput input);

        Task<PagedResultDto<AssociateUserOrganizationUnitDto>> GetUsers(FindOrganizationUnitUsersInput input);

        Task<PagedResultDto<AssociateRoleToOrganizationUnitDto>> GetRoles(FindOrganizationUnitRolesInput input);

        Task<ListResultDto<OrganizationUnitUserListDto>> AddUsersToOrganizationUnit(UsersToOrganizationUnitInput input);

        Task<ListResultDto<OrganizationUnitRolesListDto>> AddRolesToOrganizationUnit(RolesToOrganizationUnitInput input);

        Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input);

        Task RemoveRoleFromOrganizationUnit(RoleToOrganizationUnitInput input);

    }
}
