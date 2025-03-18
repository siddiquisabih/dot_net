using connect_sql.Models;
using Microsoft.EntityFrameworkCore;


namespace connect_sql.Data

{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions DbContextOptions)
        : base(DbContextOptions)
        {

        }


        public DbSet<AppUser> AppUsers { get; set; }

    }
}