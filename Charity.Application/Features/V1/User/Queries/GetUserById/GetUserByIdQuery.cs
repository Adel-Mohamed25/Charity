using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;

namespace Charity.Application.Features.V1.User.Queries.GetUserById
{
    public record GetUserByIdQuery(string Id) : IRequest<Response<UserModel>>;
}
