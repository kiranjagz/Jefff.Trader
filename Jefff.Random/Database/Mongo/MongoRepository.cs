using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Jefff.Random.Database.Mongo.Model;
using Jefff.Random.Database.Mongo.Setting;
using Jefff.Random.RestApi.Model;

namespace Jefff.Random.Database.Mongo
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoSetting _mongoSettings;
        private readonly IMongoCollection<RandomDatabaseModel> _collection;

        public MongoRepository(IMongoSetting mongoSettings)
        {
            _mongoSettings = mongoSettings;
            var mongoClient = new MongoClient(_mongoSettings.ConnectionString);
            _mongoDatabase = mongoClient.GetDatabase(_mongoSettings.Database);
            _collection = _mongoDatabase.GetCollection<RandomDatabaseModel>(_mongoSettings.CollectionName);
            _collection.Indexes.CreateOneAsync(Builders<RandomDatabaseModel>.IndexKeys.Ascending(x => x.DateCreated));
        }

        public async Task<RandomDatabaseModel> FindOneAndReplace(RandomDatabaseModel value)
        {
            var filter = Builders<RandomDatabaseModel>.Filter.Eq(x => x.Number, value.Number);
            return await _collection
                .FindOneAndReplaceAsync(filter,
                    value, new FindOneAndReplaceOptions<RandomDatabaseModel, RandomDatabaseModel>
                        { IsUpsert = true, ReturnDocument = ReturnDocument.After });
        }

        public async Task Save(RandomDatabaseModel value)
        {
            try
            {
                await _collection.InsertOneAsync(value);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
