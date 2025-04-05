using Charity.Application.Helper.ResponseServices;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authentication.Commands.ExternalLogin
{
    public class ExternalLoginCommandHandler : IRequestHandler<ExternalLoginCommand, Response<AuthenticationProperties>>
    {
        private readonly ILogger<ExternalLoginCommandHandler> _logger;

        public ExternalLoginCommandHandler(ILogger<ExternalLoginCommandHandler> logger)
        {
            _logger = logger;
        }
        public async Task<Response<AuthenticationProperties>> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.provider))
                {
                    return ResponseHandler.BadRequest<AuthenticationProperties>(message: "Provider is required.");
                }

                var redirectUri = request.returnUrl;

                var authenticationProperties = new AuthenticationProperties
                {
                    RedirectUri = redirectUri
                };

                authenticationProperties.Items["LoginProvider"] = request.provider;

                return ResponseHandler.Success(data: authenticationProperties, message: "Redirecting to external provider.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during external login.");
                return ResponseHandler.BadRequest<AuthenticationProperties>(message: $"An error occurred while processing your request. Exception: {ex.Message} .");
            }
        }
    }
}
