using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Addapptables.Boilerplate.Editions.Dto
{
    [AutoMapFrom(typeof(FeaturesEdition))]
    public class EditionDto : EntityDto
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public decimal? Price { get; set; }

        public EditionTypeEnum ? EditionType { get; set; }

        public bool IsFree {get;set;}

        public int? TrialDayCount { get; set; }

        public int ? NumberOfUsers { get; set; }
    }
}
