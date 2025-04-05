using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.User.Queries.GetPaginatedUsers
{
    public class GetPaginatedUsersQueryHandler :
        IRequestHandler<GetPaginatedUsersQuery,
            ResponsePagination<IEnumerable<UserModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedUsersQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedUsersQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedUsersQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ResponsePagination<IEnumerable<UserModel>>> Handle(GetPaginatedUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _unitOfWork.CharityUsers.GetAllAsync(orderBy: u => u.FirstName!,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!users.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<UserModel>>(message: "Users not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.CharityUsers.CountAsync(cancellationToken: cancellationToken));

                var data = _mapper.Map<IEnumerable<UserModel>>(users);
                return ResponsePaginationHandler.Success(data: data,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.CharityUsers.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<UserModel>>(errors: ex.Message);
            }
        }
    }
}
