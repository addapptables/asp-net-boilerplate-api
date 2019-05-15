using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Organizations.Dto
{
    public class GetOrganizationUnitUsersInput: PagedResultRequestDto
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }
    }
}
