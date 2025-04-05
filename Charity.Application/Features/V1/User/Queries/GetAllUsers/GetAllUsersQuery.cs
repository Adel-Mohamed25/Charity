using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;

namespace Charity.Application.Features.V1.User.Queries.GetAllUsers
{
    public record GetAllUsersQuery() : IRequest<Response<IEnumerable<UserModel>>>;
}
