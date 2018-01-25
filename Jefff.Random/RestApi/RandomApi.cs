using Jefff.Random.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.RestApi
{
    public class RandomApi
    {
        public async Task<ResponseModel> FactGet(int number, string type)
        {
            string url = $"http://numbersapi.com/" + number + "/" + type + "?json";
            using (HttpClient client = new HttpClient())
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
