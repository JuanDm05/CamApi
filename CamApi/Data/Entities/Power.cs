using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace CamApi.Data.Entities
{
    public class Power
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault] // Ignora la propiedad si su valor es el valor predeterminado
        [JsonIgnore]
        public string? Id { get; set; }

        [BsonElement("On")]
        public bool On { get; set; }

        [BsonElement("Off")]
        public bool Off { get; set; }
    }
}
