using Charity.Models.InKindDonation;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.InKindDonations.Queries.GetAllinKindDonations
{
    public record GetAllinKindDonationsQuery() : IRequest<Response<IEnumerable<InKindDonationModel>>>;

}
