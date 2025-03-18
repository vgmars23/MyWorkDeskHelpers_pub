using MyWorkDeskHelpers.Server.Domain.Entities;

namespace MyWorkDeskHelpers.Server.Application.Interfaces
{
    public interface IUserContactService
    {
        Task<UserContactInfo> CreateUserContactAsync(UserContactInfo contact);
        Task<UserContactInfo?> GetUserContactByIdAsync(string id);
        Task<bool> UpdateUserContactAsync(string id, UserContactInfo contact);
        Task<bool> DeleteUserContactAsync(string id);
        Task EnsureDefaultContactExistsAsync();

    }
}
