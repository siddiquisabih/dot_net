using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Tatweer.YourServiceName.EntityFrameworkCore;

public class YourServiceNameHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<YourServiceNameHttpApiHostMigrationsDbContext>
{
    public YourServiceNameHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<YourServiceNameHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("YourServiceName"));

        return new YourServiceNameHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
