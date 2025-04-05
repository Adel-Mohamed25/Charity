using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;

namespace Charity.Application.Features.V1.UserVolunteerActivities.Queries.GetAllVolunteersInActivity
{
    public record GetAllVolunteersInActivityQuery(string VolunteerActivityId) : IRequest<Response<IEnumerable<VolunteerModel>>>;
}
