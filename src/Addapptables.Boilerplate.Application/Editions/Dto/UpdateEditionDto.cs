using Abp.Application.Services.Dto;

namespace Addapptables.Boilerplate.Editions.Dto
{
    public class UpdateEditionDto : CreateEditionDto, IEntityDto
    {
        public int Id { get; set; }
    }
}
