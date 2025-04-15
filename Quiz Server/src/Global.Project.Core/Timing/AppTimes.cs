using System;
using Abp.Dependency;

namespace Global.Project.Timing
{
    public class AppTimes : ISingletonDependency
    {
        public DateTime StartupTime { get; set; }
    }
}
