using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Addapptables.Boilerplate.Assemblies;
using Addapptables.Boilerplate.MultiTenancy.Rules.Models;

namespace Addapptables.Boilerplate.MultiTenancy.Rules
{
    public class RuleTenantManager: IRuleTenantManager
    {
        private readonly IIocResolver _iocResolver;
        private readonly IAssembly _assembly;

        public RuleTenantManager(IIocResolver iocResolver,
            IAssembly assembly)
        {
            _iocResolver = iocResolver;
            this._assembly = assembly;
        }

        public async Task ApplyRulesAfterSave(Tenant tenant, TenantModel input)
        {
            using (var scope = _iocResolver.CreateScope())
            {
                var assemblies = _assembly.GetAssembliesByType(typeof(IRuleAfterSaveTenant));
                var allScopeResolve = assemblies.Select(x => (IRuleAfterSaveTenant)scope.Resolve(x));
                var allTask = allScopeResolve
                    .Select(x => x.ApplyRules(tenant, input))
                    .ToList();
                await Task.WhenAll(allTask);
            }
        }

        public async Task ApplyRulesBeforeSave(Tenant tenant, TenantModel input)
        {
            using (var scope = _iocResolver.CreateScope())
            {
                var assemblies = _assembly.GetAssembliesByType(typeof(IRuleBeforeSaveTenant));
                var allScopeResolve = assemblies.Select(x => (IRuleBeforeSaveTenant)scope.Resolve(x));
                var allTask = allScopeResolve
                    .Select(x => x.ApplyRules(tenant, input))
                    .ToList();
                await Task.WhenAll(allTask);
            }
        }
    }
}
