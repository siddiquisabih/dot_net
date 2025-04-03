using Abp.MultiTenancy;
using Tatweer.RadarManagment.Authorization.Users;

namespace Tatweer.RadarManagment.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
