using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Charity.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Stripe;

namespace Charity.Services.ServicesImplementation
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StripeSettings _stripeSettings;

        public PaymentServices(IUnitOfWork unitOfWork, IOptions<StripeSettings> stripeSettings)
        {
            _unitOfWork = unitOfWork;
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<string> CreatePaymentIntentAsync(decimal amount, string donorId, string? projectId)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100),
                Currency = "EGP",
                Metadata = new Dictionary<string, string>
                {
                    {"donorId", donorId},
                    {"amount", amount.ToString()},
                    {"projectId", projectId ?? ""}
                }
            };

            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);
            return paymentIntent.ClientSecret;
        }

        public async Task HandleStripeWebhookAsync(HttpRequest request, CancellationToken cancellationToken)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();
            var webhookSecret = _stripeSettings.WebhookSecret;
            var stripeEvent = EventUtility.ConstructEvent(json, request.Headers["Stripe-Signature"], webhookSecret);

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                var donorId = paymentIntent.Metadata["donorId"];
                var amount = decimal.Parse(paymentIntent.Metadata["amount"]);
                var projectId = string.IsNullOrEmpty(paymentIntent.Metadata["projectId"]) ? null : paymentIntent.Metadata["projectId"];

                using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
                try
                {
                    var donation = new MonetaryDonation
                    {
                        DonorId = donorId,
                        Amount = amount,
                        PaymentMethod = PaymentMethods.CreditCard,
                        IsAllocated = !string.IsNullOrEmpty(projectId),
                        ProjectId = projectId,
                        IsPaymentConfirmed = true
                    };

                    await _unitOfWork.MonetaryDonations.CreateAsync(donation, cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }

    }
}
