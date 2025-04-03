using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tatweer.ITSM.EntityFrameworkCore.Repositories;
using Tatweer.ITSM.Repositories;


namespace Tatweer.ITSM.EntityFrameworkCore
{
    [AutoRepositoryTypes(
        typeof(IITSMRepository<>),
        typeof(IITSMRepository<,>),
        typeof(ITSMRepositoryBase<>),
        typeof(ITSMRepositoryBase<,>)
        )]
    public class ITSMDbContext : AbpDbContext
    {

        public ITSMDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Add a default schema for this MicroService
            modelBuilder.HasDefaultSchema(ITSMConsts.DatabaseSchema);
        }
    }
}
