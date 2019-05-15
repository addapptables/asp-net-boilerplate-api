using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}