using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Tatweer.SafeCity.Configuration;
using Tatweer.SafeCity.Web;

namespace Tatweer.SafeCity.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class SafeCityDbContextFactory : IDesignTimeDbContextFactory<SafeCityDbContext>
    {
        public SafeCityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SafeCityDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            SafeCityDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SafeCityConsts.ConnectionStringName));

            return new SafeCityDbContext(builder.Options);
        }
    }
}
