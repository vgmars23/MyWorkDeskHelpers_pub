using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MyWorkDeskHelpers.Server.Domain.Entities
{
    public class Birthday
    {
        [BsonId]
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public long DateTimestamp { get; set; }

        [BsonIgnore] 
        public DateTime Date
        {
            get => DateTimeOffset.FromUnixTimeSeconds(DateTimestamp).UtcDateTime;
            set => DateTimestamp = new DateTimeOffset(DateTime.SpecifyKind(value, DateTimeKind.Utc)).ToUnixTimeSeconds();
        }

        public string Details { get; set; } = string.Empty;
        public int ReminderDays { get; set; }

        public string ContactInfoId { get; set; } = string.Empty;
    }
}
