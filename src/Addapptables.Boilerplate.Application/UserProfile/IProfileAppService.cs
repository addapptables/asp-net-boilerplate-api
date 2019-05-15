using Abp.Application.Services;
using Addapptables.Boilerplate.UserProfile.Dto;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.UserProfile
{
    public interface IProfileAppService : IApplicationService
    {
        Task<ProfileDto> GetProfile();
        Task<ProfileDto> UpdateProfile(UpdateProfileDto input);
    }
}
