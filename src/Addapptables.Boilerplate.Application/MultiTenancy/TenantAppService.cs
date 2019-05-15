using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Addapptables.Boilerplate.MultiTenancy.Dto;
using Addapptables.Boilerplate.MultiTenancy.Rules;
using Addapptables.Boilerplate.MultiTenancy.Rules.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Abp.UI;

namespace Addapptables.Boilerplate.MultiTenancy
{
    [AbpAuthorize(Authorization.Pages.Tenant.Pages_Tenants)]
    public class TenantAppService : AsyncCrudAppService<Tenant, TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, UpdateTenantDto, TenantDto>, ITenantAppService
    {
        private readonly TenantManager _tenantManager;
        private readonly IRuleTenantManager _ruleTenantManager;
        private readonly IAbpZeroDbMigrator _abpZeroDbMigrator;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public TenantAppService(
            IRepository<Tenant, int> repository,
            TenantManager tenantManager,
            IRuleTenantManager ruleTenantManager,
            IAbpZeroDbMigrator abpZeroDbMigrator,
            IUnitOfWorkManager unitOfWorkManager)
            : base(repository)
        {
            _tenantManager = tenantManager;
            _ruleTenantManager = ruleTenantManager;
            _abpZeroDbMigrator = abpZeroDbMigrator;
            _unitOfWorkManager = unitOfWorkManager;
        }

        [AbpAuthorize(Authorization.Pages.Tenant.Pages_Tenants_Create)]
        public override async Task<TenantDto> Create(CreateTenantDto input)
        {
            using (var uow = _unitOfWorkManager.Begin(TransactionScopeOption.RequiresNew))
            {
                // Create tenant
                var tenant = ObjectMapper.Map<Tenant>(input);
                tenant.ConnectionString = input.ConnectionString.IsNullOrEmpty()
                    ? null
                    : SimpleStringCipher.Instance.Encrypt(input.ConnectionString);

                var tenantModel = ObjectMapper.Map<TenantModel>(input);
                await _tenantManager.CreateAsync(tenant, tenantModel);
                await CurrentUnitOfWork.SaveChangesAsync(); // To get new tenant's id.

                // Create tenant database
                _abpZeroDbMigrator.CreateOrMigrateForTenant(tenant);
                await CurrentUnitOfWork.SaveChangesAsync();

                await _ruleTenantManager.ApplyRulesAfterSave(tenant, tenantModel);
                await uow.CompleteAsync();
                return MapToEntityDto(tenant);
            }
        }

        protected override IQueryable<Tenant> CreateFilteredQuery(PagedTenantResultRequestDto input)
        {
            return Repository.GetAll()
                .Include(x=> x.Edition)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.TenancyName.Contains(input.Keyword) || x.Name.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive);
        }

        [AbpAuthorize(Authorization.Pages.Tenant.Pages_Tenants_Edit)]
        public override async Task<TenantDto> Update(UpdateTenantDto input)
        {
            var tenant = await Repository.GetAsync(input.Id);
            if(tenant.TenancyName != input.TenancyName)
            {
                var anyTenantName = await Repository.GetAll().AnyAsync(x => x.TenancyName == input.TenancyName);
                if (anyTenantName)
                {
                    throw new UserFriendlyException(string.Format(L("TenancyNameIsAlreadyTaken"), tenant.TenancyName));
                }
            }
            ObjectMapper.Map(input, tenant);
            await _ruleTenantManager.ApplyRulesBeforeSave(tenant, ObjectMapper.Map<TenantModel>(input));
            await Repository.UpdateAsync(tenant);
            return MapToEntityDto(tenant);
        }

        [AbpAuthorize(Authorization.Pages.Tenant.Pages_Tenants_Delete)]
        public override async Task Delete(EntityDto<int> input)
        {
            var tenant = await _tenantManager.GetByIdAsync(input.Id);
            await _tenantManager.DeleteAsync(tenant);
        }

        private void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

