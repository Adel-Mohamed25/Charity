using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Project.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Response<string>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProjectCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteProjectCommandHandler(IUnitOfService unitOfService,
            IUnitOfWork unitOfWork,
            ILogger<DeleteProjectCommandHandler> logger,
            IMapper mapper)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _unitOfWork.Projects.GetByAsync(p => p.Id.Equals(request.Id), cancellationToken: cancellationToken);
                if (project is null)
                    return ResponseHandler.NotFound<string>(message: "Project not found.");

                if (project.ImageUrl is not null)
                    await _unitOfService.FileServices.DeleteImageAsync("ProjectImages", project.ImageUrl);

                await _unitOfWork.Projects.DeleteAsync(project, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return ResponseHandler.NoContent<string>(message: "The project has been deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during delete project.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
