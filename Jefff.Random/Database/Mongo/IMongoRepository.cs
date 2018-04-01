using Jefff.Random.Model;
using System.Threading.Tasks;

namespace Jefff.Random.Database.Mongo
{
    public interface IMongoRepository
    {
        Task Save(ResponseModel value);

        Task<ResponseModel> FindOneAndReplace(ResponseModel value);
    }
}
