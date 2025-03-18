namespace MyWorkDeskHelpers.Server.Domain.Entities
{
    public class UserContactInfo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }  
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string TelegramUsername { get; set; } = string.Empty;
    }
}
