using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.MultiTenancy;
using System;

namespace Addapptables.Boilerplate.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }

        public int? EditionId { get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

        public bool? IsInTrialPeriod { get; set; }

        public bool IsSubscriptionExpired { get; set; }
    }
}
