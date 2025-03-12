using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstractions;
using Charity.Models.Authentication;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Response<AuthModel>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;

        public RefreshTokenCommandHandler(IUnitOfService unitOfService,
            IUnitOfWork unitOfWork,
            ILogger<RefreshTokenCommandHandler> logger)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Response<AuthModel>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var jwtToken = await _unitOfWork.JwtTokens.GetByAsync(jt =>
                           jt.JWT == request.RefreshJwtModel.Jwt
                           && jt.RefreshJWT == request.RefreshJwtModel.RefreshJwt
                           && jt.IsRefreshJWTUsed
                           , includes: "User");

                if (jwtToken == null)
                    return ResponseHandler.NotFound<AuthModel>(message: "Token and RefreshToken Not Found");

                var jwtSecurityToken = await _unitOfService.AuthServices.ReadTokenAsync(request.RefreshJwtModel.Jwt);
                if (jwtSecurityToken == null)
                    return ResponseHandler.Unauthorized<AuthModel>(message: "Invalid JWT Token");

                bool isValid = await _unitOfService.AuthServices.IsTokenValidAsync(request.RefreshJwtModel.Jwt, jwtSecurityToken);

                if (!isValid)
                    return ResponseHandler.Unauthorized<AuthModel>(message: "Token and RefreshToken Not valid");

                var refreshToken = await _unitOfService.AuthServices.GetRefreshTokenAsync(jwtToken.User);
                return ResponseHandler.Success(refreshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during user refresh token.");
                return ResponseHandler.BadRequest<AuthModel>(errors: ex.Message);
            }
        }
    }
}
