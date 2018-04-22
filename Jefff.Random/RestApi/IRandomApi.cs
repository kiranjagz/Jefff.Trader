using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jefff.Random.RestApi.Model;

namespace Jefff.Random.RestApi
{
    public interface IRandomApi
    {
        Task<ResponseModel> FactGet(int number, string type);
    }
}
