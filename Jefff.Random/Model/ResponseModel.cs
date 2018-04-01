﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jefff.Random.Model
{

    public class ResponseModel
    {
        public ResponseModel()
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
