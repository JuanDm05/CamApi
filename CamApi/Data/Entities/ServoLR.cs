using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace CamApi.Data.Entities
{
    public class ServoLR
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault] // Ignora la propiedad si su valor es el valor predeterminado
        [JsonIgnore]
        public string? Id { get; set; }

        [BsonElement("Left")]
        public bool Left { get; set; }

        [BsonElement("Rigth")]
        public bool Rigth { get; set; }
    }
}
