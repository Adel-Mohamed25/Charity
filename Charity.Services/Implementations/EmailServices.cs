using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstractions;
using Charity.Infrastructure.Settings;
using Charity.Models.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Charity.Services.Implementations
{
    public class EmailServices : IEmailServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailServices> _logger;

        public EmailServices(IUnitOfWork unitOfWork,
            IOptions<EmailSettings> _emailSettings,
            ILogger<EmailServices> logger)
        {
            _unitOfWork = unitOfWork;
            this._emailSettings = _emailSettings.Value;
            _logger = logger;
        }

        public async Task<EmailModel> SendEmailAsync(SendEmailModel sendEmailModel)
        {
            var emailResponse = new EmailModel
            {
                To = sendEmailModel.To,
                Subject = sendEmailModel.Subject,
                Body = sendEmailModel.Body,
                From = _emailSettings.From
            };

            try
            {
                using var smtpClient = new SmtpClient();
                smtpClient.Host = _emailSettings.Host;
                smtpClient.Port = _emailSettings.Port;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.From, _emailSettings.Password);
                smtpClient.EnableSsl = true;


                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.From, _emailSettings.DisplayName),
                    Subject = sendEmailModel.Subject,
                    Body = sendEmailModel.Body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(sendEmailModel.To);

                if (sendEmailModel.Attachments is not null)
                {
                    foreach (var attachments in sendEmailModel.Attachments)
                    {
                        using var stream = attachments.OpenReadStream();
                        using var memoryStream = new MemoryStream();
                        await stream.CopyToAsync(memoryStream);
                        mailMessage.Attachments.Add(new Attachment(new MemoryStream(memoryStream.ToArray()), attachments.FileName));
                    }
                }

                await smtpClient.SendMailAsync(mailMessage);
                emailResponse.IsSuccess = true;
            }
            catch (SmtpException ex)
            {
                emailResponse.IsSuccess = false;
                _logger.LogError(ex, $"An error occurred while sending the email to{sendEmailModel.To}");
            }

            return emailResponse;
        }


    }
}
