﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Addapptables.Boilerplate.Configuration;
using Addapptables.Boilerplate.Web;

namespace Addapptables.Boilerplate.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class BoilerplateDbContextFactory : IDesignTimeDbContextFactory<BoilerplateDbContext>
    {
        public BoilerplateDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BoilerplateDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            BoilerplateDbContextConfigurer.Configure(builder, configuration.GetConnectionString(BoilerplateConsts.ConnectionStringName));

            return new BoilerplateDbContext(builder.Options);
        }
    }
}
