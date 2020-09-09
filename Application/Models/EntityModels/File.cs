using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Demos.API.Application.Models.EntityModels
{
    public class File
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RequestId { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public string BlobType { get; set; }
        public string Url { get; set; }
    }
}
