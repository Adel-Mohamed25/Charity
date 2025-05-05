using Charity.Models.ResponseModels;
using MediatR;

namespace Charity.Application.Features.V1.Notifications.Queries.GetCountMessagesById
{
    public record GetCountMessagesByIdCommand(string ReceiveId) : IRequest<Response<string>>;
}
