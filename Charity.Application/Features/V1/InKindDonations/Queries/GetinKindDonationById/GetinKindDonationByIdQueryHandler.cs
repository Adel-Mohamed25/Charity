using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.InKindDonation;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.InKindDonations.Queries.GetinKindDonationById
{
    public class GetinKindDonationByIdQueryHandler :
        IRequestHandler<GetinKindDonationByIdQuery, Response<InKindDonationModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetinKindDonationByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetinKindDonationByIdQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetinKindDonationByIdQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }


        public async Task<Response<InKindDonationModel>> Handle(GetinKindDonationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inKindDonation = await _unitOfWork.InKindDonations.GetByAsync(u => u.Id.Equals(request.Id),
                    cancellationToken: cancellationToken);
                if (inKindDonation is null)
                    return ResponseHandler.NotFound<InKindDonationModel>(message: "in-kind donation not found.");

                var result = _mapper.Map<InKindDonationModel>(inKindDonation);
                return ResponseHandler.Success(data: result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving in-kind donation data.");
                return ResponseHandler.BadRequest<InKindDonationModel>(errors: ex.Message);
            }
        }
    }
}
