using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Project.Customer.Repositories
{
    public interface CustomerRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IRepository, ITransientDependency
        where TEntity : class, IEntity<TPrimaryKey>
    {

    }

    public interface CustomerRepository<TEntity> : CustomerRepository<TEntity, int>, ITransientDependency
        where TEntity : class, IEntity<int>
    {

    }
}
