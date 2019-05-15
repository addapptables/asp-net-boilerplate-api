using System.ComponentModel.DataAnnotations;

namespace Addapptables.Boilerplate.Storage
{
    public class GetFileDto
    {
        [Required]
        public string Id { get; set; }

        public GetFileDto()
        {

        }
    }
}
