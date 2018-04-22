using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Jefff.Random.RestApi.Model
{
    public class ResponseModel
    {
        public ResponseModel()
        {
            DateCreated = DateTime.Now;
        }

        public ObjectId Id { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
        public bool Found { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }
    }

}
