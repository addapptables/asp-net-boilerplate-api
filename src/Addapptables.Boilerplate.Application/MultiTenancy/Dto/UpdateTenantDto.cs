using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.MultiTenancy.Dto
{
    [AutoMapTo(typeof(Tenant))]
    public class UpdateTenantDto: IEntityDto<int>
    {

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(AbpTenantBase.MaxTenancyNameLength)]
        [RegularExpression(AbpTenantBase.TenancyNameRegex)]
        public string TenancyName { get; set; }

        [Required]
        [StringLength(AbpTenantBase.MaxNameLength)]
        public string Name { get; set; }


        public int? EditionId { get; set; }


        public bool IsActive { get; set; }
    }
}
