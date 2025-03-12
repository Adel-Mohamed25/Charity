using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstractions;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;

        public ResetPasswordCommandHandler(IUnitOfService unitOfService,
            IUnitOfWork unitOfWork,
            ILogger<ResetPasswordCommandHandler> logger)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.Users.UserManager.FindByEmailAsync(request.ResetPassword.Email);
                if (user is null)
                    return ResponseHandler.NotFound<string>(message: "User Not Found");

                var token = await _unitOfWork.Users.UserManager.GeneratePasswordResetTokenAsync(user);
                IdentityResult result = await _unitOfWork.Users.UserManager.ResetPasswordAsync(user, token, request.ResetPassword.Password);
                if (!result.Succeeded)
                    return ResponseHandler.Conflict<string>(errors: "Password change failed.");
                return ResponseHandler.Success<string>(message: "Password changed successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during user reset password.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
