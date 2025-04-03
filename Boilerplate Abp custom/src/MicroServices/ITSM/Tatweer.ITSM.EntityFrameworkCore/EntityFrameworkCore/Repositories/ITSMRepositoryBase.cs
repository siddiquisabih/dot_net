using Abp.Dependency;
using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;
using Tatweer.ITSM.Repositories;

namespace Tatweer.ITSM.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// Base class for custom repositories of the application.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    //public abstract class SmartOfficerRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<SmartOfficerDbContext, TEntity, TPrimaryKey>
    //	where TEntity : class, IEntity<TPrimaryKey>
    //{
    //	protected SmartOfficerRepositoryBase(IDbContextProvider<SmartOfficerDbContext> dbContextProvider)
    //		: base(dbContextProvider)
    //	{
    //	}

    //	// Add your common methods for all repositories
    //}

    ///// <summary>
    ///// Base class for custom repositories of the application.
    ///// This is a shortcut of <see cref="SmartOfficerRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    ///// </summary>
    ///// <typeparam name="TEntity">Entity type</typeparam>
    //public abstract class SmartOfficerRepositoryBase<TEntity> : SmartOfficerRepositoryBase<TEntity, int>, IRepository<TEntity>
    //	where TEntity : class, IEntity<int>
    //{
    //	protected SmartOfficerRepositoryBase(IDbContextProvider<SmartOfficerDbContext> dbContextProvider)
    //		: base(dbContextProvider)
    //	{
    //	}

    //	// Do not add any method here, add to the class above (since this inherits it)!!!
    //}
    public class ITSMRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<ITSMDbContext, TEntity, TPrimaryKey>, IITSMRepository<TEntity, TPrimaryKey>, ITransientDependency
	   where TEntity : class, IEntity<TPrimaryKey>
	{
		public ITSMRepositoryBase(IDbContextProvider<ITSMDbContext> dbContextProvider)
			: base(dbContextProvider)
		{

		}

		// Add your common methods for all repositories
	}

	/// <summary>
	/// Base class for custom repositories of the application.
	/// This is a shortcut of <see cref="ITSMRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	public class ITSMRepositoryBase<TEntity> : ITSMRepositoryBase<TEntity, int>, IITSMRepository<TEntity>, ITransientDependency
		where TEntity : class, IEntity<int>
	{
		public ITSMRepositoryBase(IDbContextProvider<ITSMDbContext> dbContextProvider)
			: base(dbContextProvider)
		{

		}

		// Do not add any method here, add to the class above (since this inherits it)!!!
	}
}
