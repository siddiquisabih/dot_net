using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.Customer.EntityFrameworkCore.Repositories;
using Project.Customer.Repositories;


namespace Project.Customer.EntityFrameworkCore
{
    [AutoRepositoryTypes(
        typeof(CustomerRepository<>),
        typeof(CustomerRepository<,>),
        typeof(CustomerRepositoryBase<>),
        typeof(CustomerRepositoryBase<,>)
        )]
    public class CustomerDbContext : AbpDbContext
    {

        public CustomerDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Add a default schema for this MicroService
            modelBuilder.HasDefaultSchema(CustomerConsts.DatabaseSchema);
        }
    }
}
