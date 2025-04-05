using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.User.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetUserByIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<UserModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.CharityUsers.GetByAsync(mandatoryFilter: u => u.Id == request.Id,
                    cancellationToken: cancellationToken);
                if (user == null)
                    return ResponseHandler.NotFound<UserModel>(message: "Not Found User.");

                var result = _mapper.Map<UserModel>(user);
                return ResponseHandler.Success(data: result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving user data.");
                return ResponseHandler.BadRequest<UserModel>(errors: ex.Message);
            }
        }
    }
}
