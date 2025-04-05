namespace Charity.Contracts.ServicesAbstraction
{
    public interface IUnitOfService
    {
        public IUnitOfServices AuthServices { get; }
        public IFileServices FileServices { get; }
        public IEmailServices EmailServices { get; }
        public INotificationServices NotificationServices { get; }
        public IUserVolunteerActivityServices UserVolunteerActivityServices { get; }
        public IProjectVolunteerServices ProjectVolunteerServices { get; }
    }
}
