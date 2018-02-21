using Jefff.Random.Model;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Jefff.Random.Database.Mongo
{
    public class MongoRepository : IMongoRepository
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;
        private IMongoSetting _mongoSettings;

        public MongoRepository(IMongoSetting mongoSettings)
        {
            _mongoSettings = mongoSettings;
            _mongoClient = new MongoClient(_mongoSettings.ConnectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_mongoSettings.Database);
        }

        public async Task FindOneAndReplace(ResponseModel value)
        {
            var filter = Builders<ResponseModel>.Filter.Eq(x => x.Number, value.Number);
            var findOne = _mongoDatabase.GetCollection<ResponseModel>(_mongoSettings.CollectionName);
            await findOne.FindOneAndReplaceAsync<ResponseModel>(filter, value);
        }

        public async Task Save(ResponseModel value)
        {
            try
            {
                var index = _mongoDatabase.GetCollection<ResponseModel>(_mongoSettings.CollectionName).Indexes
                    .CreateOneAsync(Builders<ResponseModel>.IndexKeys.Ascending(x => x.DateCreated));
                var save = _mongoDatabase.GetCollection<ResponseModel>(_mongoSettings.CollectionName);
                await save.InsertOneAsync(value);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
