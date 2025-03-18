using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MyWorkDeskHelpers.Server.Domain.Entities
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)] 
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Board")]
        public string Board { get; set; } 

        [BsonElement("Description")] 
        public string Description { get; set; } = "";
    }
}
