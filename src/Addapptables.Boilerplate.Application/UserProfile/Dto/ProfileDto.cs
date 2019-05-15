using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Users;
using System;

namespace Addapptables.Boilerplate.UserProfile.Dto
{
    [AutoMapFrom(typeof(User))]
    public class ProfileDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string EmailAddress { get; set; }

        public string FullName { get; set; }

        public DateTime? LastLoginTime { get; set; }

        public string UserName { get; set; }

        public string ProfilePictureBase64 { get; set; }
    }
}
