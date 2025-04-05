using Charity.Models.InKindDonation;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.InKindDonations.Commands.UpdateInKindDonation
{
    public record UpdateInKindDonationCommand(UpdateInKindDonationModel UpdateInKindDonation) : IRequest<Response<string>>;
}
