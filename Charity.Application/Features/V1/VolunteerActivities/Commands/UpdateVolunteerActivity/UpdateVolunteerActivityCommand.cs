using Charity.Models.ResponseModels;
using Charity.Models.VolunteerActivity;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerActivities.Commands.UpdateVolunteerActivity
{
    public record UpdateVolunteerActivityCommand(UpdateVolunteerActivityModel VolunteerActivityModel) : IRequest<Response<string>>;
}
