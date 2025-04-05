using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authentication.Commands.UnLockAccount
{
    public class UnLockAccountCommandHandler : IRequestHandler<UnLockAccountCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UnLockAccountCommandHandler> _logger;

        public UnLockAccountCommandHandler(IUnitOfWork unitOfWork, ILogger<UnLockAccountCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(UnLockAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.CharityUsers.UserManager.FindByEmailAsync(request.UserEmail.Email);
                if (user == null)
                    return ResponseHandler.NotFound<string>(errors: "User not found.");
                await _unitOfWork.CharityUsers.UserManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);
                await _unitOfWork.CharityUsers.UserManager.ResetAccessFailedCountAsync(user);
                return ResponseHandler.Success<string>(message: "User account has been successfully unlocked.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during unlock account.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
