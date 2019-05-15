using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Organizations;
using Addapptables.Boilerplate.Organizations.Dto;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using Addapptables.Boilerplate.Authorization.Users;
using System.Collections.Generic;
using Abp.Timing;
using Addapptables.Boilerplate.Authorization.Roles;
using Addapptables.Boilerplate.Organizations.Dto.Roles;

namespace Addapptables.Boilerplate.Organizations
{
    public class OrganizationUnitAppService : AsyncCrudAppService<OrganizationUnit, OrganizationUnitDto, long, PagedOrganizationUnitResultRequestDto, CreateOrganizationUnitDto, UpdateOrganizationUnitDto, OrganizationUnitDto>, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<OrganizationUnitRole, long> _roleOrganizationUnitRepository;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        public OrganizationUnitAppService(
            IRepository<OrganizationUnit, long> repository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            OrganizationUnitManager organizationUnitManager,
            UserManager userManager,
            IRepository<OrganizationUnitRole, long> roleOrganizationUnitRepository,
            RoleManager roleManager
            )
            : base(repository)
        {
            _organizationUnitManager = organizationUnitManager;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _userManager = userManager;
            _roleOrganizationUnitRepository = roleOrganizationUnitRepository;
            _roleManager = roleManager;
            CreatePermissionName = Authorization.Pages.Administration.OrganizationUnits.Pages_Administration_OrganizationUnits_Create;
            UpdatePermissionName = Authorization.Pages.Administration.OrganizationUnits.Pages_Administration_OrganizationUnits_Edit;
            DeletePermissionName = Authorization.Pages.Administration.OrganizationUnits.Pages_Administration_OrganizationUnits_Delete;
            GetAllPermissionName = Authorization.Pages.Administration.OrganizationUnits.Pages_Administration_OrganizationUnits;
            GetPermissionName = Authorization.Pages.Administration.OrganizationUnits.Pages_Administration_OrganizationUnits;
        }

        public async Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnits()
        {
            CheckGetAllPermission();
            var query =
                from ou in Repository.GetAll()
                join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
                select new { ou, memberCount = g.Count() };

            var items = await query.ToListAsync();

            return new ListResultDto<OrganizationUnitDto>(
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<OrganizationUnitDto>(item.ou);
                    dto.MemberCount = item.memberCount;
                    return dto;
                }).ToList());
        }

        public async Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input)
        {
            CheckGetAllPermission();
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                        join ou in Repository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        join user in _userManager.Users on uou.UserId equals user.Id
                        where uou.OrganizationUnitId == input.Id
                        select new { uou, user };

            var totalCount = await query.CountAsync();
            var items = await query.PageBy(input).ToListAsync();

            return new PagedResultDto<OrganizationUnitUserListDto>(
                totalCount,
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<OrganizationUnitUserListDto>(item.user);
                    dto.OrganizationUnitId = input.Id;
                    dto.UserId = item.user.Id;
                    dto.Id = item.uou.Id;
                    dto.AddedTime = item.uou.CreationTime;
                    return dto;
                }).ToList());
        }

        protected override IQueryable<OrganizationUnit> CreateFilteredQuery(PagedOrganizationUnitResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.DisplayName.Contains(input.Keyword));
        }

        public override async Task<OrganizationUnitDto> Create(CreateOrganizationUnitDto input)
        {
            CheckCreatePermission();
            var organizationUnit = new OrganizationUnit(AbpSession.TenantId, input.DisplayName, input.ParentId);

            await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
        }

        public override async Task<OrganizationUnitDto> Update(UpdateOrganizationUnitDto input)
        {
            CheckUpdatePermission();
            var organizationUnit = await Repository.GetAsync(input.Id);

            organizationUnit.DisplayName = input.DisplayName;

            await _organizationUnitManager.UpdateAsync(organizationUnit);

            return await CreateOrganizationUnitDto(organizationUnit);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            CheckDeletePermission();
            await _organizationUnitManager.DeleteAsync(input.Id);
        }

        private async Task<OrganizationUnitDto> CreateOrganizationUnitDto(OrganizationUnit organizationUnit)
        {
            var dto = ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
            dto.MemberCount = await _userOrganizationUnitRepository.CountAsync(uou => uou.OrganizationUnitId == organizationUnit.Id);
            return dto;
        }

        [AbpAuthorize(Authorization.Pages.Administration.OrganizationUnits.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input)
        {
            await _userManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }

        [AbpAuthorize(Authorization.Pages.Administration.OrganizationUnits.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task<ListResultDto<OrganizationUnitUserListDto>> AddUsersToOrganizationUnit(UsersToOrganizationUnitInput input)
        {
            var users = new List<User>();
            foreach (var userId in input.UserIds)
            {
                await _userManager.AddToOrganizationUnitAsync(userId, input.OrganizationUnitId);
                var user = await _userManager.GetUserByIdAsync(userId);
                users.Add(user);
            }
            var listResultDto = new ListResultDto<OrganizationUnitUserListDto>();
            listResultDto.Items = users.Select(user =>
            {
                var dto = ObjectMapper.Map<OrganizationUnitUserListDto>(user);
                dto.OrganizationUnitId = input.OrganizationUnitId;
                dto.UserId = user.Id;
                dto.AddedTime = Clock.Now;
                return dto;
            }).ToList();
            return listResultDto;
        }

        [AbpAuthorize(Authorization.Pages.Administration.OrganizationUnits.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task<PagedResultDto<AssociateUserOrganizationUnitDto>> GetUsers(FindOrganizationUnitUsersInput input)
        {
            var userIdsInOrganizationUnit = _userOrganizationUnitRepository.GetAll()
                .Where(uou => uou.OrganizationUnitId == input.OrganizationUnitId)
                .Select(uou => uou.UserId);

            var query = _userManager.Users
                .Where(u => !userIdsInOrganizationUnit.Contains(u.Id))
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter) ||
                        u.Surname.Contains(input.Filter) ||
                        u.UserName.Contains(input.Filter) ||
                        u.EmailAddress.Contains(input.Filter)
                );

            var userCount = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.Name)
                .PageBy(input)
                .ToListAsync();

            var usersToAssociate = ObjectMapper.Map<List<AssociateUserOrganizationUnitDto>>(users);
            usersToAssociate.ForEach(x => x.OrganizationUnitId = input.OrganizationUnitId);
            return new PagedResultDto<AssociateUserOrganizationUnitDto>(
                userCount,
                usersToAssociate
            );
        }

        public async Task<ListResultDto<OrganizationUnitRolesListDto>> AddRolesToOrganizationUnit(RolesToOrganizationUnitInput input)
        {
            var roles = new List<Role>();
            foreach (var roleId in input.RoleIds)
            {
                await _roleManager.AddToOrganizationUnitAsync(roleId, input.OrganizationUnitId, AbpSession.TenantId);
                var role = await _roleManager.GetRoleByIdAsync(roleId);
                roles.Add(role);
            }
            var listResultDto = new ListResultDto<OrganizationUnitRolesListDto>();
            listResultDto.Items = roles.Select(role =>
            {
                var dto = ObjectMapper.Map<OrganizationUnitRolesListDto>(role);
                dto.OrganizationUnitId = input.OrganizationUnitId;
                dto.RoleId = role.Id;
                dto.AddedTime = Clock.Now;
                return dto;
            }).ToList();
            return listResultDto;
        }

        public Task RemoveRoleFromOrganizationUnit(RoleToOrganizationUnitInput input)
        {
            return _roleManager.RemoveFromOrganizationUnitAsync(input.RoleId, input.OrganizationUnitId);
        }

        public async Task<PagedResultDto<OrganizationUnitRolesListDto>> GetOrganizationUnitRoles(GetOrganizationUnitUsersInput input)
        {
            CheckGetAllPermission();
            var query = from uou in _roleOrganizationUnitRepository.GetAll()
                        join ou in Repository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        join role in _roleManager.Roles on uou.RoleId equals role.Id
                        where uou.OrganizationUnitId == input.Id
                        select new { uou, role };

            var totalCount = await query.CountAsync();
            var items = await query.PageBy(input).ToListAsync();

            return new PagedResultDto<OrganizationUnitRolesListDto>(
                totalCount,
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<OrganizationUnitRolesListDto>(item.role);
                    dto.OrganizationUnitId = input.Id;
                    dto.RoleId = item.role.Id;
                    dto.Id = item.uou.Id;
                    dto.AddedTime = item.uou.CreationTime;
                    return dto;
                }).ToList());
        }

        public async Task<PagedResultDto<AssociateRoleToOrganizationUnitDto>> GetRoles(FindOrganizationUnitRolesInput input)
        {
            var roleIdsInOrganizationUnit = _roleOrganizationUnitRepository.GetAll()
            .Where(uou => uou.OrganizationUnitId == input.OrganizationUnitId)
            .Select(uou => uou.RoleId);

            var query = _roleManager.Roles
                .Where(u => !roleIdsInOrganizationUnit.Contains(u.Id))
                .WhereIf(
                    !input.Filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(input.Filter)
                );

            var roleCount = await query.CountAsync();
            var roles = await query
                .OrderBy(u => u.Name)
                .PageBy(input)
                .ToListAsync();

            var rolesToAssociate = ObjectMapper.Map<List<AssociateRoleToOrganizationUnitDto>>(roles);
            rolesToAssociate.ForEach(x => x.OrganizationUnitId = input.OrganizationUnitId);
            return new PagedResultDto<AssociateRoleToOrganizationUnitDto>(
                roleCount,
                rolesToAssociate
            );
        }
    }
}
