using Charity.Domain.Enum;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetProjectsPaginatedByRequestStatus
{
    public record GetProjectsPaginatedByRequestStatusQuery(RequestStatus RequestStatus, PaginationModel Pagination)
        : IRequest<Response<IEnumerable<VolunteerApplicationModel>>>;
}
