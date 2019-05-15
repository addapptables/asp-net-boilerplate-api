using Abp.Application.Services.Dto;

namespace Addapptables.Boilerplate.Users.Dto
{
    public class UpdateUserDto : CreateUserDto, IEntityDto<long>
    {
        public long Id { get; set; }
    }
}
