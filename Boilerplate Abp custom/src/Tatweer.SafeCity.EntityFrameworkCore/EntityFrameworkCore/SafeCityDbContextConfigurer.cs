using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Tatweer.SafeCity.EntityFrameworkCore
{
    public static class SafeCityDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SafeCityDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<SafeCityDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
