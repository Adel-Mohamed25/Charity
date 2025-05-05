using Charity.Domain.Commons;
using Charity.Domain.Entities.IdentityEntities;

namespace Charity.Domain.Entities
{
    public class Notification : BaseEntity<string>
    {
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string SenderId { get; set; }
        public CharityUser Sender { get; set; }
        public string? ReceiverId { get; set; }
        public CharityUser? Receiver { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }

    }
}
