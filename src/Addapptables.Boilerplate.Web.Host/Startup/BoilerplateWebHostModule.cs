using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Addapptables.Boilerplate.Configuration;
using Abp.Configuration.Startup;
using Abp.Threading.BackgroundWorkers;
using Addapptables.Boilerplate.MultiTenancy;

namespace Addapptables.Boilerplate.Web.Host.Startup
{
    [DependsOn(
       typeof(BoilerplateWebCoreModule))]
    public class BoilerplateWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public BoilerplateWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Modules.AbpWebCommon().MultiTenancy.DomainFormat = _appConfiguration["App:ServerRootAddress"] ?? "http://localhost:22742/";
        }

        public override void PostInitialize()
        {
            if (IocManager.Resolve<IMultiTenancyConfig>().IsEnabled)
            {
                var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
                workManager.Add(IocManager.Resolve<SubscriptionExpirationWorker>());
            }
            base.PostInitialize();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BoilerplateWebHostModule).GetAssembly());
        }
    }
}
