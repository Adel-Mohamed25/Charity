using Charity.Domain.Enum;
using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetPaginatedByRequestStatus
{
    public record GetPaginatedByRequestStatusQuery(RequestStatus RequestStatus, PaginationModel Pagination)
        : IRequest<Response<IEnumerable<AssistanceRequestModel>>>;
}
