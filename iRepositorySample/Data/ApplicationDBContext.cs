using Microsoft.EntityFrameworkCore;
using IREPOSITORYSAMPLE.Models;


namespace IREPOSITORYSAMPLE.DATA
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions DbContextOptions)
        : base(DbContextOptions)
        {

        }


        public DbSet<Todo> Todo { get; set; }

    }
}