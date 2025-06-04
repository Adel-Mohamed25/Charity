using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.MonetaryDonation;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.MonetaryDonations.Queries.GetMonetaryDonationById
{
    public class GetMonetaryDonationByIdQueryHandler
        : IRequestHandler<GetMonetaryDonationByIdQuery,
            Response<MonetaryDonationModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetMonetaryDonationByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetMonetaryDonationByIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetMonetaryDonationByIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<MonetaryDonationModel>> Handle(GetMonetaryDonationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var monetaryDonation = await _unitOfWork.MonetaryDonations.GetByAsync(u => u.Id.Equals(request.Id),
                    cancellationToken: cancellationToken);
                if (monetaryDonation is null)
                    return ResponseHandler.NotFound<MonetaryDonationModel>(message: "MonetaryDonation not found.");

                var result = _mapper.Map<MonetaryDonationModel>(monetaryDonation);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving monetary donation data.");
                return ResponseHandler.BadRequest<MonetaryDonationModel>(errors: ex.Message);
            }
        }
    }
}
