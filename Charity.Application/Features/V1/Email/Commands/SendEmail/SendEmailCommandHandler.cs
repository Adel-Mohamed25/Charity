using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.ServicesAbstractions;
using Charity.Models.Email;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Email.Commands.SendEmail
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Response<EmailModel>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<SendEmailCommandHandler> _logger;

        public SendEmailCommandHandler(IUnitOfService unitOfService,
            ILogger<SendEmailCommandHandler> logger)
        {
            _unitOfService = unitOfService;
            _logger = logger;
        }

        public async Task<Response<EmailModel>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _unitOfService.EmailServices.SendEmailAsync(request.SendEmail);
                if (response.IsSuccess)
                    return ResponseHandler.Success(response);
                return ResponseHandler.Conflict(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during send email");
                return ResponseHandler.Conflict<EmailModel>(errors: ex.Message);
            }
        }
    }
}
