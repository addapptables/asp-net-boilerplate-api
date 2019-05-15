using Abp.Dependency;
using Addapptables.Boilerplate.Configuration;
using Addapptables.Boilerplate.Url;

namespace Addapptables.Boilerplate.Web.Host.Url
{
    public class WebUrlService : WebUrlServiceBase, IWebUrlService, ITransientDependency
    {
        public WebUrlService(
            IAppConfigurationAccessor configurationAccessor) :
            base(configurationAccessor)
        {
        }

        public override string WebSiteRootAddressFormatKey => "App:ClientRootAddress";

        public override string ServerRootAddressFormatKey => "App:ServerRootAddress";
    }
}
