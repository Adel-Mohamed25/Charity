using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetAllVolunteerApplications
{
    public record GetAllVolunteerApplicationsQuery() : IRequest<Response<IEnumerable<VolunteerApplicationModel>>>;
}
