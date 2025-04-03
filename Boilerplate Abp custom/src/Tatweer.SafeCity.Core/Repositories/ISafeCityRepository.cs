using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Tatweer.SafeCity.Repositories
{
    public interface ISafeCityRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IRepository, ITransientDependency
        where TEntity : class, IEntity<TPrimaryKey>
    {

    }

    public interface ISafeCityRepository<TEntity> : ISafeCityRepository<TEntity, int>, ITransientDependency
        where TEntity : class, IEntity<int>
    {

    }
}
