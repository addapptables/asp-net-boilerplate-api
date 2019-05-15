using Abp.Domain.Services;
using Addapptables.Boilerplate.MultiTenancy.Rules.Models;
using System.Threading.Tasks;

namespace Addapptables.Boilerplate.MultiTenancy.Rules
{
    public interface IRuleTenantManager : IDomainService
    {
        Task ApplyRulesBeforeSave(Tenant tenant, TenantModel input);
        Task ApplyRulesAfterSave(Tenant tenant, TenantModel input);
    }
}
