using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Addapptables.Boilerplate.Roles.Dto;
using Addapptables.Boilerplate.Users.Dto;

namespace Addapptables.Boilerplate.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UpdateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
