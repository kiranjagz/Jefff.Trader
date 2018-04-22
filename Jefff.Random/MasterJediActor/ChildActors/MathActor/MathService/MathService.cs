using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Random.Database.Mongo;
using Jefff.Random.Database.Mongo.Model;
using Jefff.Random.RestApi;
using Jefff.Random.RestApi.Model;

namespace Jefff.Random.MasterJediActor.ChildActors.MathActor.MathService
{
    public class MathService : IMathService
    {
        private readonly IRandomApi _randomApi;

        public MathService( IRandomApi randomApi)
        {
            _randomApi = randomApi;
        }
        public async Task<ResponseModel> DoApiWork(RestRequestModel restRequestModel)
        {
            return await _randomApi.FactGet(restRequestModel.Number, restRequestModel.Type);
        }
    }
}
