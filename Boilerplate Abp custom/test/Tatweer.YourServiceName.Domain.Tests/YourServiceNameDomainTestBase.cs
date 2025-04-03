using Volo.Abp.Modularity;

namespace Tatweer.YourServiceName;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class YourServiceNameDomainTestBase<TStartupModule> : YourServiceNameTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
