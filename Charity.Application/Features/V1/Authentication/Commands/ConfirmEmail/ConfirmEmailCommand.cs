using Charity.Models.Email;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Charity.Application.Features.V1.Authentication.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(EmailConfirmationRequest EmailConfirmation) : IRequest<IActionResult>;
}
