using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerApplications.Commands.UpdateVolunteerApplication
{
    public record UpdateVolunteerApplicationCommand(UpdateVolunteerApplicationModel VolunteerApplicationModel) : IRequest<Response<string>>;
}
