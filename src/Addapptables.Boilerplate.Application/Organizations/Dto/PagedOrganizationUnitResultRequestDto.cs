using Abp.Application.Services.Dto;

namespace Addapptables.Boilerplate.Organizations.Dto
{
    public class PagedOrganizationUnitResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}
