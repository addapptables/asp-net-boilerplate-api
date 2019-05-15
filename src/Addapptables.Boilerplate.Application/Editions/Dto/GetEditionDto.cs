using Abp.Application.Services.Dto;

namespace Addapptables.Boilerplate.Editions.Dto
{
    public class GetEditionDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
