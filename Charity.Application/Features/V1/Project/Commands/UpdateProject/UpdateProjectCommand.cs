using Charity.Models.Project;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Project.Commands.UpdateProject
{
    public record UpdateProjectCommand(UpdateProjectModel ProjectModel) : IRequest<Response<string>>;

}
