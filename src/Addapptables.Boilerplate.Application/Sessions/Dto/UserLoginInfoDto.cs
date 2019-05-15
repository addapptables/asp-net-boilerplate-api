﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Users;
using System.Collections.Generic;

namespace Addapptables.Boilerplate.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public IList<string> Permissions { get; set; }

        public string ProfilePictureBase64 { get; set; }
    }
}
