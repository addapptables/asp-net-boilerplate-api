using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Editions.Dto
{
    [AutoMapFrom(typeof(FeaturesEdition))]
    public class CreateEditionDto
    {
        [Required]
        public string Name { get; set; }

        public decimal? Price { get; set; }

        public EditionTypeEnum? EditionType { get; set; }

        public int? TrialDayCount { get; set; }

        public int? NumberOfUsers { get; set; }

    }
}
