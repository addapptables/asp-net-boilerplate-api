using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Addapptables.Boilerplate.Authorization.Users;
using Addapptables.Boilerplate.Editions;
using Addapptables.Boilerplate.MultiTenancy.Rules;
using Addapptables.Boilerplate.MultiTenancy.Rules.Models;

namespace Addapptables.Boilerplate.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {

        private readonly IRuleTenantManager _ruleTenantManager;

        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore,
            IRuleTenantManager ruleTenantManager) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore)
        {
            _ruleTenantManager = ruleTenantManager;
        }

        public async Task CreateAsync(Tenant tenant, TenantModel input)
        {
            await _ruleTenantManager.ApplyRulesBeforeSave(tenant, input);
            await base.CreateAsync(tenant);
        }
    }
}
