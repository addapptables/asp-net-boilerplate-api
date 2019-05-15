using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Addapptables.Boilerplate.Authorization.Roles;
using Addapptables.Boilerplate.Authorization.Users;
using Addapptables.Boilerplate.MultiTenancy;
using Addapptables.Boilerplate.Editions;

namespace Addapptables.Boilerplate.EntityFrameworkCore
{
    public class BoilerplateDbContext : AbpZeroDbContext<Tenant, Role, User, BoilerplateDbContext>
    {
        public virtual DbSet<FeaturesEdition> FeaturesEditions { get; set; }

        public BoilerplateDbContext(DbContextOptions<BoilerplateDbContext> options)
            : base(options)
        {
        }
    }
}
