﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Models.ResponseModels;
using Charity.Models.VolunteerActivity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.VolunteerActivities.Queries.GetPaginatedVolunteerActivities
{
    public class GetPaginatedVolunteerActivitiesQueryHandler :
        IRequestHandler<GetPaginatedVolunteerActivitiesQuery,
            Response<IEnumerable<VolunteerActivityModel>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetPaginatedVolunteerActivitiesQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetPaginatedVolunteerActivitiesQueryHandler(IUnitOfWork unitOfWork,
            ILogger<GetPaginatedVolunteerActivitiesQueryHandler> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<VolunteerActivityModel>>> Handle(GetPaginatedVolunteerActivitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var volunteerActivities = await _unitOfWork.VolunteerActivities.GetAllAsync(orderBy: va => va.Name,
                paginationOn: true,
                orderByDirection: request.Pagination.OrderByDirection,
                pageNumber: request.Pagination.PageNumber,
                pageSize: request.Pagination.PageSize,
                cancellationToken: cancellationToken);

                if (!volunteerActivities.Any())
                    return ResponsePaginationHandler.NotFound<IEnumerable<VolunteerActivityModel>>(message: "Volunteer activities not found.",
                       pageNumbre: request.Pagination.PageNumber,
                       pageSize: request.Pagination.PageSize,
                       totalCount: await _unitOfWork.VolunteerActivities.CountAsync(cancellationToken: cancellationToken));

                var data = await volunteerActivities.ProjectTo<VolunteerActivityModel>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return ResponsePaginationHandler.Success(data: data.AsEnumerable(),
                    pageNumber: request.Pagination.PageNumber,
                    pageSize: request.Pagination.PageSize,
                    totalCount: await _unitOfWork.VolunteerActivities.CountAsync(cancellationToken: cancellationToken));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving volunteer activities data.");
                return ResponsePaginationHandler.BadRequest<IEnumerable<VolunteerActivityModel>>(errors: ex.Message);
            }
        }
    }
}
