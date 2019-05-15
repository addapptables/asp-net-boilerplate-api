using Abp.Application.Editions;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Addapptables.Boilerplate.Editions.Dto
{
    [AutoMapFrom(typeof(Edition))]
    public class EditionMinimalDto : EntityDto
    {
        public string DisplayName { get; set; }
    }
}
