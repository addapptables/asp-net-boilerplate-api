using Addapptables.Boilerplate.Authorization.Users;
using AutoMapper;

namespace Addapptables.Boilerplate.UserProfile.Dto
{
    public class ProfileMap : Profile
    {
        public ProfileMap()
        {
            CreateMap<UpdateProfileDto, User>();
            CreateMap<User, ProfileDto>();
        }
    }
}
