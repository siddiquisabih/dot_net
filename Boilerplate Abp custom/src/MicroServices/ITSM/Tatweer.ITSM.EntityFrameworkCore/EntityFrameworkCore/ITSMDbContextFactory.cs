using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Tatweer.ITSM.Configuration;
using Tatweer.ITSM.Web;

namespace Tatweer.ITSM.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ITSMDbContextFactory : IDesignTimeDbContextFactory<ITSMDbContext>
    {
        public ITSMDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ITSMDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ITSMDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ITSMConsts.ConnectionStringName));

            return new ITSMDbContext(builder.Options);
        }
    }
}
