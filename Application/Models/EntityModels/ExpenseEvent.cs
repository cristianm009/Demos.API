using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Demos.API.Application.Models.EntityModels
{
    public class ExpenseEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Type { get; set; }
        public string ExpenseId { get; set; }
        public string Data { get; set; }
        public string Author { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
