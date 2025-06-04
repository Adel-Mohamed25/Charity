using Charity.Contracts.ServicesAbstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.MonetaryDonations.Commands.Webhook
{
    public class WebhookCommandHandler : IRequestHandler<WebhookCommand, IActionResult>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<WebhookCommandHandler> _logger;

        public WebhookCommandHandler(IUnitOfService unitOfService,
            ILogger<WebhookCommandHandler> logger)
        {
            _unitOfService = unitOfService;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(WebhookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfService.PaymentServices.HandleStripeWebhookAsync(request.Request, cancellationToken);
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during send event about webhook.");
                return new BadRequestObjectResult(new { Message = "Failed to process webhook", Error = ex.Message });
            }
        }
    }
}
