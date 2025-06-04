using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.MonetaryDonation;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.MonetaryDonations.Queries.GetPaginatedMonetaryDonationsByDonorId
{
    public class GetPaginatedMonetaryDonationsByDonorIdQueryHandler
        : IRequestHandler<GetPaginatedMonetaryDonationsByDonorIdQuery,
            Response<IEnumerable<MonetaryDonationModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedMonetaryDonationsByDonorIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedMonetaryDonationsByDonorIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedMonetaryDonationsByDonorIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<MonetaryDonationModel>>> Handle(GetPaginatedMonetaryDonationsByDonorIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monetaryDonations = await _unitOfWork.MonetaryDonations.GetAllAsync(firstFilter: md => md.DonorId.Equals(request.DonorId),
                    orderBy: md => md.Id,
                    paginationOn: true,
                    orderByDirection: request.Pagination.OrderByDirection,
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    cancellationToken: cancellationToken);

                if (!monetaryDonations.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<MonetaryDonationModel>>(message: "MonetaryDonation not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.MonetaryDonations.CountAsync(md => md.DonorId.Equals(request.DonorId), cancellationToken: cancellationToken));

                var data = await monetaryDonations.ProjectTo<MonetaryDonationModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.MonetaryDonations.CountAsync(md => md.DonorId.Equals(request.DonorId), cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving monetary donations for Donor.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<MonetaryDonationModel>>(errors: ex.Message);
            }
        }
    }
}
