using Tatweer.YourServiceName.Samples;
using Xunit;

namespace Tatweer.YourServiceName.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<YourServiceNameMongoDbTestModule>
{

}
