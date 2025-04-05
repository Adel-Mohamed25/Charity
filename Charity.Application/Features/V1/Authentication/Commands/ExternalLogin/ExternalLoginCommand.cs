using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace Charity.Application.Features.V1.Authentication.Commands.ExternalLogin
{
    public record ExternalLoginCommand(string provider, string? returnUrl = null) : IRequest<Response<AuthenticationProperties>>;
}
