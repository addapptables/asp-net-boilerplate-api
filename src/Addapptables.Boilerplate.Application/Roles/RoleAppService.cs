using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Addapptables.Boilerplate.Authorization.Roles;
using Addapptables.Boilerplate.Authorization.Users;
using Addapptables.Boilerplate.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.Roles
{
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleEditDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;
        private readonly IRepository<UserRole, long> _userRoleRepository;

        public RoleAppService(
            IRepository<Role> repository, 
            RoleManager roleManager, 
            UserManager userManager,
            IRepository<UserRole, long> userRoleRepository
            )
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userRoleRepository = userRoleRepository;
            CreatePermissionName = Authorization.Pages.Administration.Role.Pages_Administration_Roles_Create;
            UpdatePermissionName = Authorization.Pages.Administration.Role.Pages_Administration_Roles_Edit;
            GetAllPermissionName = Authorization.Pages.Administration.Role.Pages_Administration_Roles;
            DeletePermissionName = Authorization.Pages.Administration.Role.Pages_Administration_Roles_Delete;
            GetPermissionName = Authorization.Pages.Administration.Role.Pages_Administration_Roles;
        }

        [AbpAuthorize(
            Authorization.Pages.Administration.Role.Pages_Administration_Roles,
            Authorization.Pages.Administration.User.Pages_Administration_Users_Create,
            Authorization.Pages.Administration.User.Pages_Administration_Users_Edit,
            Authorization.Pages.Administration.User.Pages_Administration_Users
        )]

        public override Task<PagedResultDto<RoleDto>> GetAll(PagedRoleResultRequestDto input)
        {
            return base.GetAll(input);
        }

        public override async Task<RoleDto> Create(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();
            role.DisplayName = role.Name;

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public async Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
        {
            CheckGetPermission();
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }

        [AbpAuthorize(
            Authorization.Pages.Administration.Role.Pages_Administration_Roles,
            Authorization.Pages.Administration.User.Pages_Administration_Users_Create,
            Authorization.Pages.Administration.User.Pages_Administration_Users_Edit,
            Authorization.Pages.Administration.User.Pages_Administration_Users
        )]
        public async Task<IList<RoleMinimalDto>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return ObjectMapper.Map<List<RoleMinimalDto>>(roles);
        }

        public override async Task<RoleDto> Update(RoleEditDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            role.DisplayName = role.Name;

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.Permissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public override async Task Delete(EntityDto<int> input)
        {
            CheckDeletePermission();
            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            await _userRoleRepository.DeleteAsync(x => x.RoleId == role.Id);
            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        public Task<ListResultDto<PermissionDto>> GetAllPermissions()
        {
            var permissions = PermissionManager.GetAllPermissions();

            return Task.FromResult(new ListResultDto<PermissionDto>(
                ObjectMapper.Map<List<PermissionDto>>(permissions)
            ));
        }

        protected override IQueryable<Role> CreateFilteredQuery(PagedRoleResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.DisplayName.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword));
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedRoleResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            CheckUpdatePermission();
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
    }
}

