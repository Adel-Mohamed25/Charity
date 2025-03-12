namespace Charity.Contracts.ServicesAbstractions
{
    public interface IUnitOfService
    {
        public IUnitOfServices AuthServices { get; }
        public IFileServices FileServices { get; }
        public IEmailServices EmailServices { get; }
    }
}
