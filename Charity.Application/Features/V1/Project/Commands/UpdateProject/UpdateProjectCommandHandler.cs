using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Project.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Response<string>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProjectCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(IUnitOfService unitOfService,
            IUnitOfWork unitOfWork,
            ILogger<UpdateProjectCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetByAsync(p => p.Id.Equals(request.ProjectModel.Id), cancellationToken: cancellationToken);
                if (project is null)
                    return ResponseHandler.NotFound<string>(message: "Project not found.");

                if (request.ProjectModel.Image is null && request.ProjectModel.ImageUrl is null && project.ImageUrl is not null)
                {
                    await _unitOfService.FileServices.DeleteImageAsync("ProjectImages", project.ImageUrl);
                    request.ProjectModel.ImageUrl = null;
                }
                else if (request.ProjectModel.Image is not null)
                {
                    if (!string.IsNullOrEmpty(project.ImageUrl))
                    {
                        await _unitOfService.FileServices.DeleteImageAsync("ProjectImages", project.ImageUrl);
                    }
                    var imageUrl = await _unitOfService.FileServices.UploadImageAsync("ProjectImages", request.ProjectModel.Image);
                    request.ProjectModel.ImageUrl = imageUrl;
                }

                _mapper.Map(request.ProjectModel, project);

                await _unitOfWork.Projects.UpdateAsync(project, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.NoContent<string>(message: "The project has been updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during update project.");
                return ResponseHandler.Conflict<string>(errors: ex.Message);
            }
        }
    }
}
