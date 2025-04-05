using Charity.Models.ResponseModels;
using Charity.Models.VolunteerActivity;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerActivities.Queries.GetAllVolunteerActivities
{
    public record GetAllVolunteerActivitiesQuery() : IRequest<Response<IEnumerable<VolunteerActivityModel>>>;
}
