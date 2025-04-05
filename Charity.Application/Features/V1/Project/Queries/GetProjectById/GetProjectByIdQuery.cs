using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Project.Queries.GetProjectById
{
    public record GetProjectByIdQuery(string Id) : IRequest<Response<ProjectModel>>;
}
