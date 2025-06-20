﻿using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authentication.Commands.VerifyCode
{
    public class VerifyCodeCommandHandler : IRequestHandler<VerifyCodeCommand, Response<string>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VerifyCodeCommandHandler> _logger;

        public VerifyCodeCommandHandler(IUnitOfService unitOfService, IUnitOfWork unitOfWork,
            ILogger<VerifyCodeCommandHandler> logger)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.CharityUsers.UserManager.FindByEmailAsync(request.VerifyCode.Email);
                if (user == null)
                    return ResponseHandler.NotFound<string>(message: "User email not found.");

                var isvaild = await _unitOfService.AuthServices.VerifyCodeAsync(user, request.VerifyCode.Code);
                if (!isvaild)
                    return ResponseHandler.BadRequest<string>(message: "Verification code not vaild.");
                return ResponseHandler.NoContent<string>(message: "Verification code has been successfully verified.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during change password.");
                return ResponseHandler.BadRequest<String>(errors: ex.Message, message: "Error input data to confirm verification code.");
            }
        }
    }
}
