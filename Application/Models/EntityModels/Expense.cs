using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Demos.API.Application.Models.EntityModels
{
    public class Expense
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid ExpenseId { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
    }
}
