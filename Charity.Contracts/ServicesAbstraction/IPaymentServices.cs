using Charity.Models.MonetaryDonation;
using Microsoft.AspNetCore.Http;

namespace Charity.Contracts.ServicesAbstraction
{
    public interface IPaymentServices
    {
        Task<string> CreatePaymentIntentAsync(CreatePaymentModel paymentModel);
        Task HandleStripeWebhookAsync(HttpRequest request, CancellationToken cancellationToken);
    }
}
