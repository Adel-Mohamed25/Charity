using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Project.Commands.DeleteProject
{
    public record DeleteProjectCommand(string Id) : IRequest<Response<string>>;
}
