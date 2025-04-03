using Volo.Abp.Modularity;

namespace Tatweer.YourServiceName;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class YourServiceNameApplicationTestBase<TStartupModule> : YourServiceNameTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
