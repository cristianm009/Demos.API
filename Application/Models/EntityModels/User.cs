using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demos.API.Application.Models.EntityModels
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string DNIType { get; set; }
        public string DNI { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        
    }

}
