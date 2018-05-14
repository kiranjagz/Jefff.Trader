using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Random.RestApi;
using Jefff.Random.RestApi.Model;

namespace Jefff.Random.MasterJediActor.ChildActors.TriviaActor.TriviaService
{
    public class TriviaService : ITriviaService
    {
        private readonly IRandomApi _randomApi;

        public TriviaService(IRandomApi randomApi)
        {
            _randomApi = randomApi;
        }
        public async Task<TriviaResultModel> DoApiWork(RestRequestModel restRequestModel)
        {
            var response = await _randomApi.FactGet(restRequestModel.Number, restRequestModel.Type);

            if (response == null)
                throw new Exception($"An error has occurred in the {typeof(TriviaService).Name}, response was null");

            return new TriviaResultModel
            {
                Found = response.Found,
                Number = response.Number,
                Text = response.Text,
                Type = response.Type
            };
        }
    }
}
