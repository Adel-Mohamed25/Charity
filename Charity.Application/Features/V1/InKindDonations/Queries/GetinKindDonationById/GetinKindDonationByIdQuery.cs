using Charity.Models.InKindDonation;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.InKindDonations.Queries.GetinKindDonationById
{
    public record GetinKindDonationByIdQuery(string Id) : IRequest<Response<InKindDonationModel>>;
}
