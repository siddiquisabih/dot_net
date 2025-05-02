using System;
using Abp.Dependency;

namespace Project.Customer.Timing
{
    public class AppTimes : ISingletonDependency
    {
        public DateTime StartupTime { get; set; }
    }
}
