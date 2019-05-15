using Abp.Domain.Repositories;
using Abp.Timing;
using Addapptables.Boilerplate.Editions;
using Addapptables.Boilerplate.MultiTenancy.Rules.Models;
using System;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.MultiTenancy.Rules
{
    public class RuleTenantEdition : IRuleBeforeSaveTenant
    {

        private readonly IRepository<FeaturesEdition> _editionRepository;

        public RuleTenantEdition(IRepository<FeaturesEdition> editionRepository)
        {
            _editionRepository = editionRepository;
        }

        public async Task ApplyRules(Tenant tenant, TenantModel input)
        {
            if (input.EditionId.HasValue)
            {
                var edition = await _editionRepository.GetAsync(tenant.EditionId.Value);
                tenant.EditionId = edition.Id;
                if (edition.IsFree.HasValue && !edition.IsFree.Value)
                {
                    var date = Clock.Now.ToUniversalTime();
                    tenant.IsInTrialPeriod = false;
                    if (edition.TrialDayCount.HasValue)
                    {
                        tenant.IsInTrialPeriod = true;
                        tenant.SubscriptionEndDate = date.AddDays(edition.TrialDayCount.Value);
                    }
                    else
                    {
                        DateTime lastOfThisMonth = date.AddMonths(1).AddDays(-1);
                        tenant.SubscriptionEndDate = lastOfThisMonth;
                    }
                    tenant.IsSubscriptionExpired = false;
                    tenant.NextPrice = edition.Price;
                }
            }
        }
    }
}
