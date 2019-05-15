using AutoMapper;
using Addapptables.Boilerplate.Authorization.Users;

namespace Addapptables.Boilerplate.Users.Dto
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<UserDto, User>();
            CreateMap<UserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x => x.CreationTime, opt => opt.Ignore());

            CreateMap<UpdateUserDto, User>()
                .ForMember(x => x.Roles, opt => opt.Ignore())
                .ForMember(x=> x.Password, opt => opt.Ignore());
            CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

        }
    }
}
