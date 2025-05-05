using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;

namespace Charity.Application.Features.V1.User.Commands.UpdateUser
{
    public record UpdateUserCommand(UpdateUserModel UpdateUser) : IRequest<Response<string>>;
}
