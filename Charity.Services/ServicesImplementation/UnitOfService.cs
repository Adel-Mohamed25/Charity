using Charity.Contracts.ServicesAbstraction;

namespace Charity.Services.ServicesImplementation
{
    public class UnitOfService : IUnitOfService
    {
        public IUnitOfServices AuthServices { get; private set; }
        public IFileServices FileServices { get; private set; }
        public IEmailServices EmailServices { get; private set; }
        public INotificationServices NotificationServices { get; private set; }
        public IProjectVolunteerServices ProjectVolunteerServices { get; private set; }
        public IUserVolunteerActivityServices UserVolunteerActivityServices { get; private set; }

        public UnitOfService(IUnitOfServices authServices,
            IFileServices fileServices,
            IEmailServices emailServices,
            INotificationServices notificationServices,
            IProjectVolunteerServices projectVolunteerServices,
            IUserVolunteerActivityServices userVolunteerActivityServices)
        {
            AuthServices = authServices;
            FileServices = fileServices;
            EmailServices = emailServices;
            NotificationServices = notificationServices;
            ProjectVolunteerServices = projectVolunteerServices;
            UserVolunteerActivityServices = userVolunteerActivityServices;
        }

    }
}
