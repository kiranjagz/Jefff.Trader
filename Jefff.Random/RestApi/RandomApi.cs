using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Jefff.Random.RestApi.Model;

namespace Jefff.Random.RestApi
{
    public class RandomApi : IRandomApi
    {
        public async Task<ResponseModel> FactGet(int number, string type)
        {
            var url = $"http://numbersapi.com/" + number + "/" + type + "?json";
            using (var client = new HttpClient())
            {
                var response = await client.GetByteArrayAsync(url);
                var responseString = Encoding.UTF8.GetString(response);
                if (!string.IsNullOrWhiteSpace(responseString))
                {
                    return JsonConvert.DeserializeObject<ResponseModel>(responseString);
                }
            }
            return null;
        }
    }
}
