using Charity.Models.ResponseModels;
using Charity.Models.UserVolunteerActivity;
using MediatR;

namespace Charity.Application.Features.V1.UserVolunteerActivities.Commands.RemoveVolunteerFromActivity
{
    public record RemoveVolunteerFromActivityCommand(UserVolunteerActivityModel UserVolunteerActivity) : IRequest<Response<string>>;
}
