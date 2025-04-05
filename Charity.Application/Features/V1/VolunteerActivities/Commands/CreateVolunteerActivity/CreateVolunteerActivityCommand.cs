using Charity.Models.ResponseModels;
using Charity.Models.VolunteerActivity;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerActivities.Commands.CreateVolunteerActivity
{
    public record CreateVolunteerActivityCommand(CreateVolunteerActivityModel VolunteerActivityModel) : IRequest<Response<string>>;
}
