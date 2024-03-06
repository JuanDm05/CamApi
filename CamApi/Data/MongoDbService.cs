using MongoDB.Driver;

namespace CamApi.Data
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase("SecurityCam");
        }

        public IMongoDatabase Database => _database;
    }
}
