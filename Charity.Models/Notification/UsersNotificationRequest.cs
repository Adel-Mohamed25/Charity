namespace Charity.Models.Notification
{
    public class UsersNotificationRequest
    {
        public IEnumerable<string> UserIds { get; set; }
        public object Message { get; set; }
    }
}
