using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Project.Customer.EntityFrameworkCore
{
    public static class CustomerDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CustomerDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CustomerDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
