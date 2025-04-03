using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Tatweer.ITSM.Configuration
{
	/// <summary>
	/// This is a class that needs to be extracted from Main Projects to a shared library
	/// </summary>
	public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IHostingEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}
