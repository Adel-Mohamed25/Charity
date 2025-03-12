using Charity.Models.Email;

namespace Charity.Contracts.ServicesAbstractions
{
    public interface IEmailServices
    {
        Task<EmailModel> SendEmailAsync(SendEmailModel sendEmailModel);
    }
}
