using Charity.Domain.Enum;
using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Project.Queries.GetPaginatedProjectsByStatus
{
    public record GetPaginatedProjectsByStatusQuery(ProjectStatus Status, PaginationModel Pagination)
        : IRequest<Response<IEnumerable<ProjectModel>>>;
}
