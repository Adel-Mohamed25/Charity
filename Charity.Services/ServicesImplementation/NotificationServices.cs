using Charity.Contracts.ServicesAbstraction;

namespace Charity.Services.ServicesImplementation
{
    public class NotificationServices : INotificationServices
    {
        //public NotificationServices(IHubContext<NotificationHub> hubContext)
        //{

        //}
        public async Task SendNotification(string userId, string title, string message)
        {
            // 1️⃣ حفظ الإشعار في قاعدة البيانات
            //var notification = new Notification
            //{
            //    UserId = userId,
            //    Title = title,
            //    Message = message,
            //    IsRead = false,
            //    CreatedDate = DateTime.UtcNow
            //};

            //_context.Notifications.Add(notification);
            //await _context.SaveChangesAsync();

            //// 2️⃣ إرسال الإشعار عبر SignalR
            //await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", title, message);
        }
    }
}
