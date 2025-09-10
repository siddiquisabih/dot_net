using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Global.Project.Authorization.Users;
using Global.Project.Model;
using Global.Project.MultiTenancy;
using Global.Project.Authorization.Roles;
using Global.Project.Lookups;
using Global.Project.Model.Customers;


namespace Global.Project.EntityFrameworkCore
{
    public class ProjectDbContext : AbpZeroDbContext<Tenant, Role, User, ProjectDbContext>
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AuditType> AuditTypes { get; set; }
        public DbSet<UserSignalrConnection> UserSignalrConnections { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationGroup> NotificationGroups { get; set; }
        public DbSet<UserNotificationGroup> UserNotificationGroup { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CustomerInformation> CustomerInformations { get; set; }
        public DbSet<CustomerDocument> CustomerDocuments { get; set; } 

        public DbSet<UserProfileImage> UserProfileImages { get; set; }
         

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Country>()
                .HasOne(c => c.Region)
                .WithMany()
                .HasForeignKey(c => c.RegionId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany()
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
                .HasOne(c => c.State)
                .WithMany()
                .HasForeignKey(c => c.StateId)
                .OnDelete(DeleteBehavior.Restrict);

        }



    }
}
