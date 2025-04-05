using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerApplications.Commands.CreateVolunteerApplication
{
    public record CreateVolunteerApplicationCommand(CreateVolunteerApplicationModel VolunteerApplicationModel) : IRequest<Response<string>>;
}
