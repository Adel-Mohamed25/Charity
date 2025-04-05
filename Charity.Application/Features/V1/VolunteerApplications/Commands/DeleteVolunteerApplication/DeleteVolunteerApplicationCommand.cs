using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerApplications.Commands.DeleteVolunteerApplication
{
    public record DeleteVolunteerApplicationCommand(string Id) : IRequest<Response<string>>;
}
