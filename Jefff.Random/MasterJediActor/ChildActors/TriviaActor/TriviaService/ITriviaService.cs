using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Random.RestApi;
using Jefff.Random.RestApi.Model;

namespace Jefff.Random.MasterJediActor.ChildActors.TriviaActor.TriviaService
{
    public interface ITriviaService
    {
        Task<ResponseModel> DoApiWork(RestRequestModel restRequestModel);
    }
}
