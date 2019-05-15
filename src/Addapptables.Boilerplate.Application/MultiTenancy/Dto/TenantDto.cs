using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Editions.Dto;
using System;

namespace Addapptables.Boilerplate.MultiTenancy.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public int? EditionId { get; set; }

        public string ConnectionString { get; set; }

        public EditionMinimalDto Edition { get; set; }

        public DateTime? SubscriptionEndDate { get; set; }

        public bool? IsInTrialPeriod { get; set; }

        public bool? IsSubscriptionExpired { get; set; }

        public decimal? NextPrice { get; set; }
    }
}
