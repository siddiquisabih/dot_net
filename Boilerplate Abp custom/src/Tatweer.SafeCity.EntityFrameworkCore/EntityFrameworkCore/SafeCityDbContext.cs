using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Tatweer.SafeCity.Authorization.Roles;
using Tatweer.SafeCity.Authorization.Users;
using Tatweer.SafeCity.MultiTenancy;
using Tatweer.SafeCity.Models;
using Tatweer.SafeCity.Model;
using Tatweer.SafeCity.Model.Departments;
using Tatweer.SafeCity.Model.Customer;
namespace Tatweer.SafeCity.EntityFrameworkCore
{
    public class SafeCityDbContext : AbpZeroDbContext<Tenant, Role, User, SafeCityDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserDepartmentSupervisor> UserDepartmentSupervisors { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AuditType> AuditTypes { get; set; }
        public DbSet<UserSignalrConnection> UserSignalrConnections { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationGroup> NotificationGroups { get; set; }
        public DbSet<UserNotificationGroup> UserNotificationGroup { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }


        public DbSet<SafeCityAttachment> Attachments { get; set; }
        public SafeCityDbContext(DbContextOptions<SafeCityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>()
                 .HasIndex(x => x.Code)
                 .IsUnique();


            modelBuilder.Entity<Role>()
          .Property(e => e.Id)
          .ValueGeneratedNever();

            //// Configure Entity Relationship for this MicroService
            //ConfigureEntities(modelBuilder);

            // Add a default schema for this MicroService

        }
    }
}
