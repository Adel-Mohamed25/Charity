using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.InKindDonations.Commands.DeleteInKindDonation
{
    public record DeleteInKindDonationCommand(string Id) : IRequest<Response<string>>;
}
