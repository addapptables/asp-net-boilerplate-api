using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Users.Account.Dto
{
    public class SendPasswordResetCodeDto
    {
        [Required]
        public string UserNameOrEmail { get; set; }
    }
}
