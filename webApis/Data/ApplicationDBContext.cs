using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEBAPIS.Models;


namespace WEBAPIS.DATA
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions DbContextOptions)
        : base(DbContextOptions)
        {

        }


        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}