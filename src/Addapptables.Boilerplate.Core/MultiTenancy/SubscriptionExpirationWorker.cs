using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using System.Threading.Tasks;
using System.Linq;
using Abp.Threading;
using System;
using Addapptables.Boilerplate.Editions;

namespace Addapptables.Boilerplate.MultiTenancy
{
    public class SubscriptionExpirationWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private const int CheckPeriodAsMilliseconds = 1 * 60 * 60 * 1000 * 24; //1 day

        private readonly IRepository<Tenant> _tenantRepository;

        private readonly IRepository<FeaturesEdition> _editionRepository;

        public SubscriptionExpirationWorker(
            AbpTimer timer,
            IRepository<Tenant> tenantRepository,
            IRepository<FeaturesEdition> editionRepository
            ) : base(timer)
        {
            _tenantRepository = tenantRepository;
            _editionRepository = editionRepository;
            Timer.Period = CheckPeriodAsMilliseconds;
            Timer.RunOnStart = true;
            LocalizationSourceName = BoilerplateConsts.LocalizationSourceName;
        }

        protected override void DoWork()
        {
            try
            {
                var now = Clock.Now.ToUniversalTime();
                var subscriptionExpiredTenants = _tenantRepository.GetAllList(
                    tenant => tenant.EditionId != null && 
                              ((FeaturesEdition)tenant.Edition).IsFree == false &&
                              tenant.SubscriptionEndDate != null &&
                              tenant.IsActive &&
                              tenant.IsSubscriptionExpired == false &&
                              tenant.SubscriptionEndDate.Value.Date < now.Date
                );
                var allTenantTask = subscriptionExpiredTenants.Select(x => ProcessExpiredTenant(x));
                foreach (var tenantProcess in allTenantTask)
                {
                    AsyncHelper.RunSync(() => tenantProcess);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
            }
        }

        private async Task ProcessExpiredTenant(Tenant tenant)
        {
            try
            {
                tenant.IsInTrialPeriod = false;
                var edition = await _editionRepository.GetAsync(tenant.EditionId.Value);
                tenant.NextPrice = edition?.Price;
                tenant.IsSubscriptionExpired = edition != null && edition.IsFree == false ? true: false;
                tenant.SubscriptionEndDate = edition != null && edition.IsFree == false ? tenant.SubscriptionEndDate : null;
                await _tenantRepository.UpdateAsync(tenant);
            }
            catch (Exception ex)
            {
                Logger.Error($"Subscription of tenant {tenant.TenancyName} has been expired but tenant couldn't be made passive !");
                Logger.Error(ex.Message, ex);
            }
        }
    }
}
