namespace Charity.Models.Notification
{
    public class NotificationExceptRequest
    {
        public IEnumerable<string> ExcludedConnectionIds { get; set; }
        public object Message { get; set; }
    }
}
