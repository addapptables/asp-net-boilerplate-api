using Abp.Dependency;
using Addapptables.Boilerplate.MultiTenancy.Rules.Models;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.MultiTenancy.Rules
{
    public interface IRuleBeforeSaveTenant: ITransientDependency
    {
        Task ApplyRules(Tenant tenant, TenantModel input);
    }
}
