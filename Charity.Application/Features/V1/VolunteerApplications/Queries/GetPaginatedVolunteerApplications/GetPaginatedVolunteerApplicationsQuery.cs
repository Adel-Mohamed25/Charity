using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetPaginatedVolunteerApplications
{
    public record GetPaginatedVolunteerApplicationsQuery(PaginationModel Pagination)
        : IRequest<Response<IEnumerable<VolunteerApplicationModel>>>;
}
