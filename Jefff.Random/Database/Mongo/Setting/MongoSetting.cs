namespace Jefff.Random.Database.Mongo.Setting
{
    public class MongoSetting : IMongoSetting
    {
        public string ConnectionString { get; private set; } = "mongodb://localhost:27017/";
        public string CollectionName { get; private set; } = "random";
        public string Database { get; private set; } = "jefffs_db";
    }
}
