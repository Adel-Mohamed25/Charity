using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Project.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Response<string>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProjectCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IUnitOfService unitOfService,
            IUnitOfWork unitOfWork,
            ILogger<CreateProjectCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var project = _mapper.Map<CharityProject>(request.ProjectModel);
                if (request.ProjectModel.Image is not null)
                {
                    var imageUrl = await _unitOfService.FileServices.UploadImageAsync("ProjectImages", request.ProjectModel.Image);
                    project.ImageUrl = imageUrl;
                }
                await _unitOfWork.Projects.CreateAsync(project, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.Created<string>(message: "The project has been created successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during create project.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
