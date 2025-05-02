using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Project.Customer.EntityFrameworkCore;
using Project.Customer.Repositories;

namespace Project.Customer.EntityFrameworkCore.Repositories
{
   
    public class CustomerRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<CustomerDbContext, TEntity, TPrimaryKey>, CustomerRepository<TEntity, TPrimaryKey>, ITransientDependency
	   where TEntity : class, IEntity<TPrimaryKey>
	{
		public CustomerRepositoryBase(IDbContextProvider<CustomerDbContext> dbContextProvider)
			: base(dbContextProvider)
		{

		}

	}

	public class CustomerRepositoryBase<TEntity> : CustomerRepositoryBase<TEntity, int>, CustomerRepository<TEntity>, ITransientDependency
		where TEntity : class, IEntity<int>
	{
		public CustomerRepositoryBase(IDbContextProvider<CustomerDbContext> dbContextProvider)
			: base(dbContextProvider)
		{

		}
	}
}
