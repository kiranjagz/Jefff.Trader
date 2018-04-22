namespace Jefff.Random.RestApi
{
    public class RestRequestModel : BaseRequest
    {
        public RestRequestModel(int number, string type)
        {
            Number = number;
            Type = type;
        }
        public int Number { get; private set; }
        public string Type { get; private set; }
    }
}
