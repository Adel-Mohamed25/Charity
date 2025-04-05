using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Project.Commands.CreateProject
{
    public record CreateProjectCommand(CreateProjectModel ProjectModel) : IRequest<Response<string>>;
}
