using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Enum;
using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.User.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<IEnumerable<UserModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllUsersQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<UserModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _unitOfWork.CharityUsers.GetAllAsync(orderBy: u => u.FirstName!,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!users.Any())
                    return ResponseHandler.NotFound<IEnumerable<UserModel>>(message: "Not Found Users.");

                var result = _mapper.Map<IEnumerable<UserModel>>(users);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users data.");
                return ResponseHandler.BadRequest<IEnumerable<UserModel>>(errors: ex.Message);
            }
        }
    }
}
