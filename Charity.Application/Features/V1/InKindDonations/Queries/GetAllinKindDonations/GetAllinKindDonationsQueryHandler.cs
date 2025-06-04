using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Enum;
using Charity.Models.InKindDonation;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.InKindDonations.Queries.GetAllinKindDonations
{
    public class GetAllinKindDonationsQueryHandler :
        IRequestHandler<GetAllinKindDonationsQuery, Response<IEnumerable<InKindDonationModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllinKindDonationsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllinKindDonationsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllinKindDonationsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<InKindDonationModel>>> Handle(GetAllinKindDonationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inKindDonations = await _unitOfWork.InKindDonations.GetAllAsync(
                    orderBy: ik => ik.Id,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken
                );

                if (!inKindDonations.Any())
                    return ResponseHandler.NotFound<IEnumerable<InKindDonationModel>>(message: "in-kind donations not found.");

                var result = await inKindDonations.ProjectTo<InKindDonationModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponseHandler.Success(data: result.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving in-kind donations data.");
                return ResponseHandler.BadRequest<IEnumerable<InKindDonationModel>>(errors: ex.Message);
            }
        }
    }
}
