using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.MonetaryDonations.Commands.CreatePaymentIntent
{
    public record CreatePaymentIntentCommand(decimal amount,
        string donorId,
        string? projectId) : IRequest<Response<string>>;
}
