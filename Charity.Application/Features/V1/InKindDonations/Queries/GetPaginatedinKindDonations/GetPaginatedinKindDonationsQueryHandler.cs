using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.InKindDonation;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.InKindDonations.Queries.GetPaginatedinKindDonations
{
    public class GetPaginatedinKindDonationsQueryHandler
        : IRequestHandler<GetPaginatedinKindDonationsQuery,
            ResponsePagination<IEnumerable<InKindDonationModel>>>

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedinKindDonationsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedinKindDonationsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedinKindDonationsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponsePagination<IEnumerable<InKindDonationModel>>> Handle(GetPaginatedinKindDonationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inKindDonations = await _unitOfWork.InKindDonations.GetAllAsync(orderBy: ik => ik.Id,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!inKindDonations.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<InKindDonationModel>>(message: "in-kind donations not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.InKindDonations.CountAsync(cancellationToken: cancellationToken));

                var data = _mapper.Map<IEnumerable<InKindDonationModel>>(inKindDonations);
                return ResponsePaginationHandler.Success(data: data,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.InKindDonations.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving in-kind donations data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<InKindDonationModel>>(errors: ex.Message);
            }
        }
    }
}
