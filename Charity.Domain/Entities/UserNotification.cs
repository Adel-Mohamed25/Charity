using Charity.Domain.Entities.IdentityEntities;

namespace Charity.Domain.Entities
{
    public class UserNotification
    {
        public string UserId { get; set; }
        public CharityUser User { get; set; }

        public string NotificationId { get; set; }
        public Notification Notification { get; set; }

        public bool IsRead { get; set; } = false;
    }
}
