﻿using Abp.MultiTenancy;
using Tatweer.SafeCity.Authorization.Users;

namespace Tatweer.SafeCity.MultiTenancy
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
