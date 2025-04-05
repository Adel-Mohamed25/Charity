using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authentication.Commands.LockAccount
{
    public class LockAccountCommandHandler : IRequestHandler<LockAccountCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LockAccountCommandHandler> _logger;

        public LockAccountCommandHandler(IUnitOfWork unitOfWork, ILogger<LockAccountCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(LockAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.CharityUsers.UserManager.FindByEmailAsync(request.UserEmail.Email);
                if (user == null)
                    return ResponseHandler.NotFound<string>(errors: "User not found.");
                await _unitOfWork.CharityUsers.UserManager.SetLockoutEndDateAsync(user, DateTime.MaxValue);
                return ResponseHandler.Success<string>(message: "User account has been successfully locked.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during lock account.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
