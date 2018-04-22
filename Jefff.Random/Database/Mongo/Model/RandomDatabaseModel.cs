using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Jefff.Random.Database.Mongo.Model
{
    public class RandomDatabaseModel
    {
        public RandomDatabaseModel()
        {
            DateCreated = DateTime.Now;
        }
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
        public bool Found { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
