using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;

namespace Charity.Application.Features.V1.ProjectVolunteers.Queries.GetAllVolunteersInProject
{
    public record GetAllVolunteersInProjectQuery(string ProjectId) : IRequest<Response<IEnumerable<VolunteerModel>>>;
}
