using Abp.Auditing;
using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Authorization.Accounts.Dto
{
    public class AccountResetPasswordDto
    {
        [Range(1, long.MaxValue)]
        public long UserId { get; set; }

        [Required]
        public string ResetCode { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxPlainPasswordLength)]
        [DisableAuditing]
        public string Password { get; set; }
    }
}
