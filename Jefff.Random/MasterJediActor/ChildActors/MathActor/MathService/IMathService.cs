using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Random.Database.Mongo.Model;
using Jefff.Random.RestApi;
using Jefff.Random.RestApi.Model;

namespace Jefff.Random.MasterJediActor.ChildActors.MathActor.MathService
{
    public interface IMathService
    {
        Task<ResponseModel> DoApiWork(RestRequestModel restRequestModel);
    }
}
