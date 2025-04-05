using Charity.Models.ResponseModels;
using Charity.Models.UserVolunteerActivity;
using MediatR;

namespace Charity.Application.Features.V1.UserVolunteerActivities.Commands.AddVolunteerToActivity
{
    public record AddVolunteerToActivityCommand(UserVolunteerActivityModel UserVolunteerActivity) : IRequest<Response<string>>;
}
