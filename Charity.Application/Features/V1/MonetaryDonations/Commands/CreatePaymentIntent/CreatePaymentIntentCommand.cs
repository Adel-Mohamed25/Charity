using Charity.Models.MonetaryDonation;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.MonetaryDonations.Commands.CreatePaymentIntent
{
    public record CreatePaymentIntentCommand(CreatePaymentModel PaymentModel) : IRequest<Response<string>>;
}
