using Abp.MultiTenancy;
using Abp.Timing;
using Addapptables.Boilerplate.Authorization.Users;
using Addapptables.Boilerplate.Editions;
using System;

namespace Addapptables.Boilerplate.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {

        public DateTime? SubscriptionEndDate { get; set; }

        public bool? IsInTrialPeriod { get; set; }

        public bool IsSubscriptionExpired { get; set; }

        public decimal? NextPrice { get; set; }

        public Tenant()
        {
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }

        public bool IsSubscriptionEnded()
        {
            return SubscriptionEndDate < Clock.Now.ToUniversalTime();
        }

        public int CalculateRemainingDayCount()
        {
            return SubscriptionEndDate != null ? (SubscriptionEndDate.Value - Clock.Now.ToUniversalTime()).Days : 0;
        }
    }
}
