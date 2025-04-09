using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Project.Customer.Configuration;
using Project.Customer.Web;

namespace Project.Customer.EntityFrameworkCore
{
    public class CustomerDbContextFactory : IDesignTimeDbContextFactory<CustomerDbContext>
    {
        public CustomerDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CustomerDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            CustomerDbContextConfigurer.Configure(builder, configuration.GetConnectionString(CustomerConsts.ConnectionStringName));

            return new CustomerDbContext(builder.Options);
        }
    }
}
