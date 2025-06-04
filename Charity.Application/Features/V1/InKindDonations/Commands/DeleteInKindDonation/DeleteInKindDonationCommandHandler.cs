using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.InKindDonations.Commands.DeleteInKindDonation
{
    public class DeleteInKindDonationCommandHandler : IRequestHandler<DeleteInKindDonationCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<DeleteInKindDonationCommandHandler> _logger;

        public DeleteInKindDonationCommandHandler(IUnitOfWork unitOfWork,
            IUnitOfService unitOfService,
            ILogger<DeleteInKindDonationCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _unitOfService = unitOfService;
            _logger = logger;
        }

        public async Task<Response<string>> Handle(DeleteInKindDonationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var inKindDonation = await _unitOfWork.InKindDonations.GetByAsync(
                    ik => ik.Id.Equals(request.Id),
                    cancellationToken: cancellationToken
                );

                if (inKindDonation is null)
                    return ResponseHandler.NotFound<string>(message: "The in-kind donation not found.");

                if (inKindDonation.ImageUrls is not null)
                {
                    foreach (var imageUrl in inKindDonation.ImageUrls)
                    {
                        await _unitOfService.FileServices.DeleteImageAsync("InKindDonationImages", imageUrl);
                    }
                }

                await _unitOfWork.InKindDonations.DeleteAsync(inKindDonation, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResponseHandler.NoContent<string>(message: "The in-kind donation has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete in-kind donation.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
