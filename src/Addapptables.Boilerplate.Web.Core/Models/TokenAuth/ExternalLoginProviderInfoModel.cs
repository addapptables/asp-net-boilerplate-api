using Abp.AutoMapper;
using Addapptables.Boilerplate.Authentication.External;

namespace Addapptables.Boilerplate.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
