using Charity.Models.ResponseModels;
using Charity.Models.VolunteerApplication;
using MediatR;

namespace Charity.Application.Features.V1.VolunteerApplications.Queries.GetVolunteerApplicationById
{
    public record GetVolunteerApplicationByIdQuery(string Id) : IRequest<Response<VolunteerApplicationModel>>;
}
