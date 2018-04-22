using System;

namespace Jefff.Random.RestApi
{
    public abstract class BaseRequest
    {
        public DateTime CreatedDateTime { get; } = DateTime.Now;
        public Guid UniqueId { get;  } = Guid.NewGuid();
    }
}
