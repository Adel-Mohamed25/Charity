using Charity.Models.ResponseModels;
using Charity.Models.VolunteerActivity;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerActivities.Queries.GetPaginatedVolunteerActivities
{
    public record GetPaginatedVolunteerActivitiesQuery(PaginationModel Pagination)
        : IRequest<Response<IEnumerable<VolunteerActivityModel>>>;
}
