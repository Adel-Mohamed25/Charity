using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;

namespace Charity.Domain.Entities
{
    public class Notification : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string SenderId { get; set; }
        public CharityUser Sender { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public ICollection<UserNotification> UserNotifications { get; set; } = new List<UserNotification>();
    }
}
