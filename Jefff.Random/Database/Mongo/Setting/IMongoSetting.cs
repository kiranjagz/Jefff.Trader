namespace Jefff.Random.Database.Mongo.Setting
{ 
    public interface IMongoSetting
    {
        string ConnectionString { get; }
        string CollectionName { get; }
        string Database { get; }
    }
}
