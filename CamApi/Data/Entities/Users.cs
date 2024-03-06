using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace CamApi.Data.Entities
{
    public class Users
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault] // Ignora la propiedad si su valor es el valor predeterminado
        [JsonIgnore] // Excluye la propiedad de la documentación de Swagger
        public string? Id { get; set; }

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("Email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }

        [BsonElement("PhoneNumber"), BsonRepresentation(BsonType.String)]
        public string? PhoneNumber { get; set; }

        [BsonElement("Password"), BsonRepresentation(BsonType.String)]
        public string? Password { get; set; }
    }
}