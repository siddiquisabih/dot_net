using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Tatweer.ITSM.Repositories
{
    public interface IITSMRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IRepository, ITransientDependency
        where TEntity : class, IEntity<TPrimaryKey>
    {

    }

    public interface IITSMRepository<TEntity> : IITSMRepository<TEntity, int>, ITransientDependency
        where TEntity : class, IEntity<int>
    {

    }
}
