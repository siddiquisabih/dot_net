using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Global.Project.Model;
using Global.Project.Authorization.Users;
using Global.Project.Model.Departments;
using Global.Project.MultiTenancy;
using Global.Project.Authorization.Roles;
namespace Global.Project.EntityFrameworkCore
{
    public class ProjectDbContext : AbpZeroDbContext<Tenant, Role, User, ProjectDbContext>
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserDepartmentSupervisor> UserDepartmentSupervisors { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<AuditType> AuditTypes { get; set; }
        public DbSet<UserSignalrConnection> UserSignalrConnections { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationGroup> NotificationGroups { get; set; }
        public DbSet<UserNotificationGroup> UserNotificationGroup { get; set; }
 


        public DbSet<ProjectAttachment> Attachments { get; set; }
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
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

        }
    }
}
