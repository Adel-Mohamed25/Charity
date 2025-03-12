using Charity.Contracts.ServicesAbstractions;

namespace Charity.Services.Implementations
{
    public class UnitOfService : IUnitOfService
    {
        public IUnitOfServices AuthServices { get; private set; }
        public IFileServices FileServices { get; private set; }
        public IEmailServices EmailServices { get; private set; }

        public UnitOfService(IUnitOfServices authServices,
            IFileServices fileServices,
            IEmailServices emailServices)
        {
            AuthServices = authServices;
            FileServices = fileServices;
            EmailServices = emailServices;
        }

    }
}
