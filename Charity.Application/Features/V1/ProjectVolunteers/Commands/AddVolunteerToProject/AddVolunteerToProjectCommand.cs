using Charity.Models.ProjectVolunteer;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.ProjectVolunteers.Commands.AddVolunteerToProject
{
    public record AddVolunteerToProjectCommand(ProjectVolunteerModel ProjectVolunteer) : IRequest<Response<string>>;
}
