using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Project.Queries.GetPaginatedProjects
{
    public record GetPaginatedProjectsQuery(PaginationModel Pagination) : IRequest<ResponsePagination<IEnumerable<ProjectModel>>>;
}
