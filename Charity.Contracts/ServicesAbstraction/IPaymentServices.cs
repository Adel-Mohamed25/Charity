using Microsoft.AspNetCore.Http;

namespace Charity.Contracts.ServicesAbstraction
{
    public interface IPaymentServices
    {
        Task<string> CreatePaymentIntentAsync(decimal amount, string donorId, string? projectId);
        Task HandleStripeWebhookAsync(HttpRequest request, CancellationToken cancellationToken);
    }
}
