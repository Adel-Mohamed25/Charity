namespace Charity.Models.Notification
{
    public class GroupsNotificationRequest
    {
        public IEnumerable<string> Groups { get; set; }
        public object Message { get; set; }
    }
}
