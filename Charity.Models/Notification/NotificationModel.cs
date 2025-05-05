namespace Charity.Models.Notification
{
    public class NotificationModel
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
