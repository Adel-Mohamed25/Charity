namespace Charity.Models.Notification
{
    public class UpdateNotificationModel
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? Message { get; set; }
    }
}
