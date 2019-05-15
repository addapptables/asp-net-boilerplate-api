using Abp.Domain.Services;

namespace Addapptables.Boilerplate
{
    public class AddapptablesDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected AddapptablesDomainServiceBase()
        {
            LocalizationSourceName = BoilerplateConsts.LocalizationSourceName;
        }
    }
}
