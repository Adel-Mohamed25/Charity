using Charity.Domain.Enum;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetActivitiesPaginatedByRequestStatus
{
    public record GetActivitiesPaginatedByRequestStatusQuery(RequestStatus RequestStatus, PaginationModel Pagination)
        : IRequest<Response<IEnumerable<VolunteerApplicationModel>>>;
}
