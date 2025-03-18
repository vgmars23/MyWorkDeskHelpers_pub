using MongoDB.Bson;
using MongoDB.Driver;
using MyWorkDeskHelpers.Server.Application.Interfaces;
using MyWorkDeskHelpers.Server.Domain.Entities;

namespace MyWorkDeskHelpers.Server.Infrastructure.Services
{
    public class BirthdayService : IBirthdayService
    {
        private readonly IMongoCollection<Birthday> _birthdays;

        public BirthdayService(IMongoDatabase database)
        {
            _birthdays = database.GetCollection<Birthday>("Birthdays");
        }

        public async Task<List<Birthday>> GetAllBirthdaysAsync()
        {
            return await _birthdays.Find(_ => true).ToListAsync();
        }

        public async Task<Birthday> CreateBirthdayAsync(Birthday birthday)
        {
            if (string.IsNullOrEmpty(birthday.Id))
            {
                birthday.Id = ObjectId.GenerateNewId().ToString();
            }

            await _birthdays.InsertOneAsync(birthday);
            return birthday;
        }

        public async Task<Birthday> UpdateBirthdayAsync(string id, Birthday birthday)
        {
            var filter = Builders<Birthday>.Filter.Eq(b => b.Id, id);
            var update = Builders<Birthday>.Update
                .Set(b => b.Name, birthday.Name)
                .Set(b => b.Date, birthday.Date)
                .Set(b => b.Details, birthday.Details)
                .Set(b => b.ReminderDays, birthday.ReminderDays);

            var result = await _birthdays.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<Birthday>
            {
                ReturnDocument = ReturnDocument.After
            });

            return result;
        }

        public async Task<bool> DeleteBirthdayAsync(string id)
        {
            var filter = Builders<Birthday>.Filter.Eq(b => b.Id, id);
            var result = await _birthdays.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }

        public async Task<List<Birthday>> GetTodayBirthdaysAsync()
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

            DateTime todayLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone).Date;
            DateTime tomorrowLocal = todayLocal.AddDays(1);

            long todayTimestamp = new DateTimeOffset(todayLocal).ToUnixTimeSeconds();
            long tomorrowTimestamp = new DateTimeOffset(tomorrowLocal).ToUnixTimeSeconds();

            var filter = Builders<Birthday>.Filter.Gte(b => b.DateTimestamp, todayTimestamp) &
                         Builders<Birthday>.Filter.Lt(b => b.DateTimestamp, tomorrowTimestamp);

            return await _birthdays.Find(filter).ToListAsync();
        }

        public async Task<List<Birthday>> GetBirthdaysToRemindAsync(long todayTimestamp)
        {
            var filter = Builders<Birthday>.Filter.Where(b =>
                b.DateTimestamp <= todayTimestamp + (b.ReminderDays * 86400));

            return await _birthdays.Find(filter).ToListAsync();
        }

        public async Task<List<Birthday>> GetBirthdaysByContactInfoAsync(string contactInfoId)
        {
            var filter = Builders<Birthday>.Filter.Eq(b => b.ContactInfoId, contactInfoId);
            return await _birthdays.Find(filter).ToListAsync();
        }
    }
}

