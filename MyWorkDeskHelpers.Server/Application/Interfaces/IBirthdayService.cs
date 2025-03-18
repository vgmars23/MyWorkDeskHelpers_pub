using MyWorkDeskHelpers.Server.Domain.Entities;

namespace MyWorkDeskHelpers.Server.Application.Interfaces
{
    public interface IBirthdayService
    {
        Task<List<Birthday>> GetAllBirthdaysAsync();
        Task<Birthday> CreateBirthdayAsync(Birthday birthday);
        Task<Birthday> UpdateBirthdayAsync(string id, Birthday birthday);
        Task<bool> DeleteBirthdayAsync(string id);
        Task<List<Birthday>> GetTodayBirthdaysAsync();
        Task<List<Birthday>> GetBirthdaysToRemindAsync(long todayTimestamp);
        Task<List<Birthday>> GetBirthdaysByContactInfoAsync(string contactInfoId);
    }
}
