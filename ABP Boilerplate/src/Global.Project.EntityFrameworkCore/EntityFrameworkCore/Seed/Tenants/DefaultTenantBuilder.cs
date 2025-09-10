using System.Linq;
using Microsoft.EntityFrameworkCore;
using Abp.MultiTenancy;
using Global.Project.Editions;
using Global.Project.MultiTenancy;
using Global.Project.Features;

namespace Global.Project.EntityFrameworkCore.Seed.Tenants
{
    public class DefaultTenantBuilder
    {
        private readonly ProjectDbContext _context;

        public DefaultTenantBuilder(ProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
        }

        private void CreateDefaultTenant()
        {
            var defaultTenant = _context.Tenants.IgnoreQueryFilters().FirstOrDefault(t => t.TenancyName == AbpTenantBase.DefaultTenantName);
            if (defaultTenant == null)
            {
                defaultTenant = new Tenant(AbpTenantBase.DefaultTenantName, AbpTenantBase.DefaultTenantName);

                var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
                if (defaultEdition != null)
                {
                    defaultTenant.EditionId = defaultEdition.Id;
                }

                _context.Tenants.Add(defaultTenant);
                _context.SaveChanges();
            }

            var features = FeatureFinder
           .GetAllFeatures(new ProjectFeatureProvider())
           .ToList();


            foreach (var feature in features)
            {
                CreateFeatureIfNotExists(defaultTenant.Id, feature.Name, true);
            }
        }

        private void CreateFeatureIfNotExists(int tenantId, string featureName, bool isEnabled)
        {
            if (_context.EditionFeatureSettings.IgnoreQueryFilters().Any(ef => ef.TenantId == tenantId && ef.Name == featureName))
            {
                return;
            }
            _context.TenantFeatureSettings.Add(new TenantFeatureSetting
            {
                Name = featureName,
                Value = isEnabled.ToString(),
                TenantId = tenantId
            });
            _context.SaveChanges();
        }

    }
}


