using Charity.Models.ResponseModels;
using Charity.Models.User;
using MediatR;

namespace Charity.Application.Features.V1.User.Queries.GetPaginatedUsers
{
    public record GetPaginatedUsersQuery(PaginationModel Pagination) : IRequest<ResponsePagination<IEnumerable<UserModel>>>;
}
