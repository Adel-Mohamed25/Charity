using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetPaginatedAssistanceRequests
{
    public record GetPaginatedAssistanceRequestsQuery(PaginationModel Pagination)
        : IRequest<ResponsePagination<IEnumerable<AssistanceRequestModel>>>;
}
