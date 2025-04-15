using System;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Global.Project.EntityFrameworkCore.Seed.Host;
using Global.Project.EntityFrameworkCore.Seed.Tenants;

namespace Global.Project.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<ProjectDbContext>(iocResolver, SeedHostDb);
        }

        public static void SeedHostDb(ProjectDbContext context)
        {
            context.SuppressAutoSetTenantId = true;

            new InitialHostDbBuilder(context).Create();

            new DefaultTenantBuilder(context).Create();
            new TenantRoleAndUserBuilder(context, 1).Create();
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}
