using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.ServicesAbstractions;
using Charity.Models.Email;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Response<EmailConfirmationResponse>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<ConfirmEmailCommandHandler> _logger;

        public ConfirmEmailCommandHandler(IUnitOfService unitOfService,
            ILogger<ConfirmEmailCommandHandler> logger)
        {
            _unitOfService = unitOfService;
            _logger = logger;
        }
        public async Task<Response<EmailConfirmationResponse>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _unitOfService.AuthServices.ConfirmEmailAsync(request.EmailConfirmation);
                if (response.IsConfirmed)
                    return ResponseHandler.Success(response);
                return ResponseHandler.Conflict(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during confirm email.");
                return ResponseHandler.BadRequest<EmailConfirmationResponse>(errors: ex.Message);
            }

        }
    }
}
