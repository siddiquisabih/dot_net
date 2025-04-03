using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tatweer.YourServiceName.EntityFrameworkCore;

public class YourServiceNameHttpApiHostMigrationsDbContext : AbpDbContext<YourServiceNameHttpApiHostMigrationsDbContext>
{
    public YourServiceNameHttpApiHostMigrationsDbContext(DbContextOptions<YourServiceNameHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureYourServiceName();
    }
}
