using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero;
using Abp.Zero.Ldap;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;
using Tatweer.SafeCity.Common.DTOs;

using Tatweer.SafeCity.Common.Services;

namespace Tatweer.SafeCity.Common
{
    [DependsOn(typeof(AbpZeroCoreModule), typeof(AbpZeroLdapModule))]
    public class SafeCityCommonModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SafeCityCommonModule).GetAssembly());
        }
    }
}

