using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.InKindDonations.Commands.UpdateInKindDonation
{
    public class UpdateInKindDonationCommandHandler : IRequestHandler<UpdateInKindDonationCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<UpdateInKindDonationCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateInKindDonationCommandHandler(IUnitOfWork unitOfWork,
            IUnitOfService unitOfService,
            ILogger<UpdateInKindDonationCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _unitOfService = unitOfService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateInKindDonationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var inKindDonation = await _unitOfWork.InKindDonations.GetByAsync(
                     ik => ik.Id.Equals(request.UpdateInKindDonation.Id),
                     cancellationToken: cancellationToken
                );

                if (inKindDonation is null)
                    return ResponseHandler.NotFound<string>(message: "In-kind donation not found.");

                var existingImages = inKindDonation.ImageUrls ?? new List<string>();
                var updatedImageUrls = request.UpdateInKindDonation.ImageUrls ?? new List<string>();

                if (request.UpdateInKindDonation.Images is null && request.UpdateInKindDonation.ImageUrls is null && inKindDonation.ImageUrls is not null)
                {
                    foreach (var image in inKindDonation.ImageUrls)
                    {
                        await _unitOfService.FileServices.DeleteImageAsync("InKindDonationImages", image);
                    }
                    updatedImageUrls = new List<string>();
                }
                else if (request.UpdateInKindDonation.Images is not null)
                {
                    foreach (var image in request.UpdateInKindDonation.Images)
                    {
                        var uploadedImageUrl = await _unitOfService.FileServices.UploadImageAsync("InKindDonationImages", image);

                        updatedImageUrls ??= new List<string>();
                        updatedImageUrls.Add(uploadedImageUrl);
                    }

                    var removedImages = existingImages.Except(updatedImageUrls).ToList();
                    foreach (var imageUrl in removedImages)
                    {
                        await _unitOfService.FileServices.DeleteImageAsync("InKindDonationImages", imageUrl);
                    }
                }

                request.UpdateInKindDonation.ImageUrls = updatedImageUrls;
                _mapper.Map(request.UpdateInKindDonation, inKindDonation);


                await _unitOfWork.InKindDonations.UpdateAsync(inKindDonation, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return ResponseHandler.NoContent<string>(message: "The in-kind donation has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during update in-kind donation.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
