using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Global.Project.Model;
using Global.Project.Authorization.Users;
using Global.Project.Model.Departments;
using Global.Project.MultiTenancy;
using Global.Project.Authorization.Roles;
using Global.Project.Model.Questions;
using Global.Project.Model.Options;
using Global.Project.Model.Exams;
using Global.Project.Model.SubmitExams;
using Global.Project.Model.ExamResults;
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

        public DbSet<Question> Questions { get; set; }

        public DbSet<Option> Options { get; set; }

        public DbSet<Exam> Exams { get; set; }

        public DbSet<SubmitExam> SubmitExams { get; set; }

        public DbSet<ExamResult> ExamResults { get; set; }


        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<SubmitExam>()
                .HasOne(se => se.Exam)
                .WithMany() // Assuming Exam doesn't have a collection of SubmitExams
                .HasForeignKey(se => se.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubmitExam>()
                .HasOne(se => se.Question)
                .WithMany() // Assuming Question doesn't have a collection of SubmitExams
                .HasForeignKey(se => se.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubmitExam>()
                .HasOne(se => se.Option)
                .WithMany() // Assuming Option doesn't have a collection of SubmitExams
                .HasForeignKey(se => se.OptionId)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Department>()
                 .HasIndex(x => x.Code)
                 .IsUnique();


            modelBuilder.Entity<Role>()
          .Property(e => e.Id)
          .ValueGeneratedNever();

        }
    }
}
