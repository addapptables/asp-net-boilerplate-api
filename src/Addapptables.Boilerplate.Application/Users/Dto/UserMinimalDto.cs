using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Users;

namespace Addapptables.Boilerplate.Users.Dto
{

    [AutoMapFrom(typeof(User))]
    public class UserMinimalDto : EntityDto
    {
        public string FullName { get; set; }

        public string EmailAddress { get; set; }
    }
}
