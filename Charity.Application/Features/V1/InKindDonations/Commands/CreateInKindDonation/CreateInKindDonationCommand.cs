using Charity.Models.InKindDonation;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.InKindDonations.Commands.CreateInKindDonation
{
    public record CreateInKindDonationCommand(CreateInKindDonationModel InKindDonationModel) : IRequest<Response<string>>;
}
