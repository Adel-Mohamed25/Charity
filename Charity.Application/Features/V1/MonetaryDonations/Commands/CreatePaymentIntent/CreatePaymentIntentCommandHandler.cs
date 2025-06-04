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
                var clientSecret = await _unitOfService.PaymentServices
                    .CreatePaymentIntentAsync(request.PaymentModel);
                return ResponseHandler.Created(data: clientSecret);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create paymentIntent.");
                return ResponseHandler.BadRequest<string>(message: ex.Message);
            }
        }
    }
}
