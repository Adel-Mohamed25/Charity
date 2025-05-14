using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.MonetaryDonations.Commands.CreatePaymentIntent
{
    public class CreatePaymentIntentCommandHandler :
        IRequestHandler<CreatePaymentIntentCommand, Response<string>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<CreatePaymentIntentCommandHandler> _logger;

        public CreatePaymentIntentCommandHandler(IUnitOfService unitOfService,
            ILogger<CreatePaymentIntentCommandHandler> logger)
        {
            _unitOfService = unitOfService;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(CreatePaymentIntentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sessionId = await _unitOfService.PaymentServices
                    .CreatePaymentIntentAsync(request.amount, request.donorId, request.projectId);
                return ResponseHandler.Success(data: sessionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create checkout session.");
                return ResponseHandler.BadRequest<string>(message: ex.Message);
            }
        }
    }
}
