using Abp.Authorization.Users;
using Abp.AutoMapper;
using Addapptables.Boilerplate.Authorization.Users;
using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.UserProfile.Dto
{
    [AutoMapTo(typeof(User))]
    public class UpdateProfileDto
    {
        [Required]
        [StringLength(AbpUserBase.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public string ProfilePictureBase64 { get; set; }
    }
}
