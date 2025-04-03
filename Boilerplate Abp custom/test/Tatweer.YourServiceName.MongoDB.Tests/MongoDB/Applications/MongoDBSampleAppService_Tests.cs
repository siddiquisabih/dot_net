using Tatweer.YourServiceName.MongoDB;
using Tatweer.YourServiceName.Samples;
using Xunit;

namespace Tatweer.YourServiceName.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<YourServiceNameMongoDbTestModule>
{

}
