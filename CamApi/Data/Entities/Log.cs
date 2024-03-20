    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson;
    using System.Text.Json.Serialization;

    namespace CamApi.Data.Entities
    {
        public class Log
        {
            [BsonId]
            [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
            [BsonIgnoreIfDefault]
            [JsonIgnore]
            public string? Id { get; set; }

            [BsonElement("Image"), BsonRepresentation(BsonType.String)]
            public string? Image { get; set; }

        [BsonElement("Dateofbirth"), BsonRepresentation(BsonType.Int32)]
        public int DateOfBirth { get; set; }




    }
}

