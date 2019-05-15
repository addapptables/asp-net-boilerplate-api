using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Users;
using System;

namespace Addapptables.Boilerplate.Organizations.Dto
{
    [AutoMapFrom(typeof(User))]
    public class OrganizationUnitUserListDto : EntityDto<long>
    {
        public long UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public string FullName { get; set; }

        public long OrganizationUnitId { get; set; }

        public DateTime AddedTime { get; set; }
    }
}
