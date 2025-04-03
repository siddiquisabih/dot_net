using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Tatweer.ITSM.EntityFrameworkCore
{
    public static class ITSMDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ITSMDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ITSMDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
