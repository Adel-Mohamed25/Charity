using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerActivities.Commands.DeleteVolunteerActivity
{
    public record DeleteVolunteerActivityCommand(string Id) : IRequest<Response<string>>;
}
