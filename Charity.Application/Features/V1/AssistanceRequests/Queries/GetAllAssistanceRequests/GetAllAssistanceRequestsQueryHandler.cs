using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Domain.Enum;
using Charity.Models.AssistanceRequest;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.AssistanceRequests.Queries.GetAllAssistanceRequests
{
    public class GetAllAssistanceRequestsQueryHandler
        : IRequestHandler<GetAllAssistanceRequestsQuery, Response<IEnumerable<AssistanceRequestModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllAssistanceRequestsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllAssistanceRequestsQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetAllAssistanceRequestsQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<AssistanceRequestModel>>> Handle(GetAllAssistanceRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var assistanceRequests = await _unitOfWork.AssistanceRequests.GetAllAsync(orderBy: ar => ar.Id,
                    orderByDirection: OrderByDirection.Ascending,
                    cancellationToken: cancellationToken);

                if (!assistanceRequests.Any())
                    return ResponseHandler.NotFound<IEnumerable<AssistanceRequestModel>>(message: "Assistance requests not found.");


                var result = await assistanceRequests
                    .ProjectTo<AssistanceRequestModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponseHandler.Success(data: result.AsEnumerable());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving assistance requests data.");
                return ResponseHandler.BadRequest<IEnumerable<AssistanceRequestModel>>(errors: ex.Message);
            }
        }
    }
}
