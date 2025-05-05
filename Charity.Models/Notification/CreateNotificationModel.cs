namespace Charity.Models.Notification
{
    public class CreateNotificationModel
    {
        public string SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? Message { get; set; }
    }
}
