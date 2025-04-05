namespace Charity.Contracts.ServicesAbstraction
{
    public interface INotificationServices
    {
        Task SendNotification(string userId, string title, string message);
    }
}
