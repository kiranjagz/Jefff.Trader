using System.Threading.Tasks;
using Jefff.Random.Database.Mongo.Model;
using Jefff.Random.RestApi.Model;

namespace Jefff.Random.Database.Mongo
{
    public interface IMongoRepository
    {
        Task Save(RandomDatabaseModel value);

        Task<RandomDatabaseModel> FindOneAndReplace(RandomDatabaseModel value);
    }
}
