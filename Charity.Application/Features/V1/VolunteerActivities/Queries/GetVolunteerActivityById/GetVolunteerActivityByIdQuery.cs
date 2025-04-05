using Charity.Models.ResponseModels;
using Charity.Models.VolunteerActivity;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerActivities.Queries.GetVolunteerActivityById
{
    public record GetVolunteerActivityByIdQuery(string Id) : IRequest<Response<VolunteerActivityModel>>;
}
