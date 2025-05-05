namespace Charity.Models.Notification
{
    public class GroupNotificationExceptRequest
    {
        public string Group { get; set; }
        public IEnumerable<string> ExcludedConnectionIds { get; set; }
        public object Message { get; set; }
    }
}
