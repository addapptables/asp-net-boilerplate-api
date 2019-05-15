using AutoMapper;
using Abp.Application.Editions;

namespace Addapptables.Boilerplate.Editions.Dto
{
    public class EditionMapProfile: Profile
    {
        public EditionMapProfile()
        {
            CreateMap<EditionDto, Edition>();
            CreateMap<Edition, EditionDto >();
            CreateMap<Edition, FeaturesEdition>();
            CreateMap<Edition, FeaturesEdition>();
            CreateMap<CreateEditionDto, FeaturesEdition>();
            CreateMap<FeaturesEdition, EditionDto>();
            CreateMap<FeaturesEdition, EditionMinimalDto>();
        } 
    }
}
