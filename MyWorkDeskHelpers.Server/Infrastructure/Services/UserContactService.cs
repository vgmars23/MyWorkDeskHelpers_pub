using MongoDB.Driver;
using MyWorkDeskHelpers.Server.Application.Interfaces;
using MyWorkDeskHelpers.Server.Domain.Entities;

namespace MyWorkDeskHelpers.Server.Infrastructure.Services
{
    public class UserContactService : IUserContactService
    {
        private readonly IMongoCollection<UserContactInfo> _contactCollection;

        public UserContactService(IMongoDatabase database)
        {
            _contactCollection = database.GetCollection<UserContactInfo>("UserContacts");
        }


        public async Task EnsureDefaultContactExistsAsync()
        {
            var count = await _contactCollection.CountDocumentsAsync(_ => true);
            if (count == 0) 
            {
                var defaultContact = new UserContactInfo
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "",
                    TelegramUsername = ""
                };

                await _contactCollection.InsertOneAsync(defaultContact);
                Console.WriteLine("✅ Добавлена тестовая запись в UserContactInfo.");
            }
        }

        public async Task<UserContactInfo> CreateUserContactAsync(UserContactInfo contact)
        {
            await _contactCollection.InsertOneAsync(contact);
            return contact;
        }

        public async Task<UserContactInfo?> GetUserContactByIdAsync(string id)
        {
            return await _contactCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateUserContactAsync(string id, UserContactInfo contact)
        {
            var result = await _contactCollection.ReplaceOneAsync(c => c.Id == id, contact);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteUserContactAsync(string id)
        {
            var result = await _contactCollection.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
