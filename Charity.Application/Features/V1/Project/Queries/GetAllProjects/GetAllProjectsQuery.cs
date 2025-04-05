using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Project.Queries.GetAllProjects
{
    public record GetAllProjectsQuery() : IRequest<Response<IEnumerable<ProjectModel>>>;
}
