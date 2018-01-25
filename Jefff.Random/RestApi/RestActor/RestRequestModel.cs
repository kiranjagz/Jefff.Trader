using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.RestApi.RestActor
{
    public class RestRequestModel : BaseRequest
    {
        public RestRequestModel(int number, string type)
        {
            Number = number;
            Type = type;
        }
        public int Number { get; set; }
        public string Type { get; set; }
    }
}
