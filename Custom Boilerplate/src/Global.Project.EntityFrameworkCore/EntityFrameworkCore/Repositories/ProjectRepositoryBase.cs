using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace Global.Project.EntityFrameworkCore.Repositories
{
  
    public abstract class ProjectRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<ProjectDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ProjectRepositoryBase(IDbContextProvider<ProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    
    public abstract class ProjectRepositoryBase<TEntity> : ProjectRepositoryBase<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        protected ProjectRepositoryBase(IDbContextProvider<ProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

    }
}
