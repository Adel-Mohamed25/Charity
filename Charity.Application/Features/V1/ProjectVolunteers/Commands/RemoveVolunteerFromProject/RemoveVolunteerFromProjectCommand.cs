using Charity.Models.ProjectVolunteer;
using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.ProjectVolunteers.Commands.RemoveVolunteerFromProject
{
    public record RemoveVolunteerFromProjectCommand(ProjectVolunteerModel ProjectVolunteer) : IRequest<Response<string>>;
}
