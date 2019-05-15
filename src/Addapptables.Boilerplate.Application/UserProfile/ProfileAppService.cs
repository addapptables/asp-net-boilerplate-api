using Abp.Authorization;
using Abp.UI;
using Addapptables.Boilerplate.Authorization.Users;
using Addapptables.Boilerplate.UserProfile.Dto;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.UserProfile
{
    public class ProfileAppService : BoilerplateAppServiceBase, IProfileAppService
    {

        [AbpAuthorize()]
        public async Task<ProfileDto> GetProfile()
        {
            var user = await UserManager.GetUserByIdAsync(AbpSession.UserId.Value);
            return ObjectMapper.Map<ProfileDto>(user);
        }

        [AbpAuthorize()]
        public async Task<ProfileDto> UpdateProfile(UpdateProfileDto input)
        {
            var user = await UserManager.GetUserByIdAsync(AbpSession.UserId.Value);
            await Validate(user, input);
            ObjectMapper.Map(input, user);
            CheckErrors(await UserManager.UpdateAsync(user));
            return ObjectMapper.Map<ProfileDto>(user);
        }

        private async Task Validate(User user, UpdateProfileDto input)
        {
            if (user.UserName != input.UserName)
            {
                var existUser = await UserManager.FindByNameAsync(input.UserName);
                if (existUser != null)
                {
                    throw new UserFriendlyException(L("UserNameIsAlreadyTaken"));
                }
            }
            if (user.EmailAddress != input.EmailAddress)
            {
                var existUser = await UserManager.FindByEmailAsync(input.EmailAddress);
                if (existUser != null)
                {
                    throw new UserFriendlyException(L("EmailAdressIsAlreadyTaken"));
                }
            }
        }
    }
}
