using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace CamApi.Data.Entities
{
    public class Camara
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault] // Ignora la propiedad si su valor es el valor predeterminado
        [JsonIgnore]
        public string? Id { get; set; }

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]

        public string Name { get; set; }

        [BsonElement("Photo"), BsonRepresentation(BsonType.String)]

        public string Photo { get; set; }    
    }
}
