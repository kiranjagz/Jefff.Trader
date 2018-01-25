using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.RestApi.RestActor
{
    public abstract class BaseRequest
    {
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public Guid UniqueId { get; set; } = Guid.NewGuid();
    }
}
