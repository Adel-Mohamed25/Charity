using Charity.Models.MonetaryDonation;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.MonetaryDonations.Queries.GetMonetaryDonationById
{
    public record GetMonetaryDonationByIdQuery(string Id) : IRequest<Response<MonetaryDonationModel>>;
}
