using Charity.Models.Email;

namespace Charity.Contracts.ServicesAbstraction
{
    public interface IEmailServices
    {
        Task<SendEmailResponse> SendEmailAsync(SendEmailRequest sendEmailModel);
    }
}
