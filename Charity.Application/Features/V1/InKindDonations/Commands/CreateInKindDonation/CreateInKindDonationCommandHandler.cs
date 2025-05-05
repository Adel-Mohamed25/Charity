using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.InKindDonations.Commands.CreateInKindDonation
{
    public class CreateInKindDonationCommandHandler : IRequestHandler<CreateInKindDonationCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<CreateInKindDonationCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public CreateInKindDonationCommandHandler(IUnitOfWork unitOfWork,
            IUnitOfService unitOfService,
            ILogger<CreateInKindDonationCommandHandler> logger,
            IMapper mapper,
            IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _unitOfService = unitOfService;
            _logger = logger;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<Response<string>> Handle(CreateInKindDonationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var imageUrls = new List<string>();

                if (request.InKindDonationModel.Images is not null)
                {
                    try
                    {
                        var uploadTasks = request.InKindDonationModel.Images
                            .Select(image => _unitOfService.FileServices.UploadImageAsync("InKindDonationImages", image));
                        imageUrls = (await Task.WhenAll(uploadTasks)).ToList();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error occurred while uploading images.");
                        return ResponseHandler.Conflict<string>("An error occurred while uploading images. Please try again.");
                    }
                }

                var inKindDonation = _mapper.Map<InKindDonation>(request.InKindDonationModel);
                inKindDonation.ImageUrls = imageUrls;
                await _unitOfWork.InKindDonations.CreateAsync(inKindDonation, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Success<string>(message: "The inkinddonation has been created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create inkinddonation.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
