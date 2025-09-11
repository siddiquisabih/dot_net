using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Global.Project.Repositories
{
    public interface IProjectRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IRepository, ITransientDependency
        where TEntity : class, IEntity<TPrimaryKey>
    {

    }

    public interface IProjectRepository<TEntity> : IProjectRepository<TEntity, int>, ITransientDependency
        where TEntity : class, IEntity<int>
    {

    }
}
