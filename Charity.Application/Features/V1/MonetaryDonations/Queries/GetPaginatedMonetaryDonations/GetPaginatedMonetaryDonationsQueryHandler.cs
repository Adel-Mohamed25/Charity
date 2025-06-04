using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.MonetaryDonation;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.MonetaryDonations.Queries.GetPaginatedMonetaryDonations
{
    public class GetPaginatedMonetaryDonationsQueryHandler
        : IRequestHandler<GetPaginatedMonetaryDonationsQuery,
            Response<IEnumerable<MonetaryDonationModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedMonetaryDonationsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedMonetaryDonationsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedMonetaryDonationsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<MonetaryDonationModel>>> Handle(GetPaginatedMonetaryDonationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monetaryDonations = await _unitOfWork.MonetaryDonations.GetAllAsync(orderBy: md => md.Id,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!monetaryDonations.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<MonetaryDonationModel>>(message: "MonetaryDonation not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.MonetaryDonations.CountAsync(cancellationToken: cancellationToken));

                var data = await monetaryDonations.ProjectTo<MonetaryDonationModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.MonetaryDonations.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving monetary donations.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<MonetaryDonationModel>>(errors: ex.Message);
            }
        }
    }
}
