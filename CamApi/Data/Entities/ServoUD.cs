using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace CamApi.Data.Entities
{
    public class ServoUD
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault] // Ignora la propiedad si su valor es el valor predeterminado
        [JsonIgnore]
        public string? Id { get; set; }

        [BsonElement("Up")]
        public bool Up { get; set; }

        [BsonElement("Down")]
        public bool Down { get; set; }
    }
}
