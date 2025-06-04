using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities;
using Charity.Infrastructure.Settings;
using Charity.Models.MonetaryDonation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Stripe;

namespace Charity.Services.ServicesImplementation
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PaymentServices> _logger;
        private readonly StripeSettings _stripeSettings;

        public PaymentServices(IUnitOfWork unitOfWork,
            IOptions<StripeSettings> stripeSettings,
            ILogger<PaymentServices> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<string> CreatePaymentIntentAsync(CreatePaymentModel paymentModel)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(paymentModel.Amount * 100),
                Currency = "EGP",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true
                },
                Metadata = new Dictionary<string, string>
                {
                    { "donorId", paymentModel.DonorId },
                    { "amount", paymentModel.Amount.ToString() },
                    { "projectId", paymentModel.ProjectId ?? "" }
                }
            };


            var service = new PaymentIntentService();
            var paymentIntent = await service.CreateAsync(options);



            return paymentIntent.ClientSecret;
        }


        public async Task HandleStripeWebhookAsync(HttpRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.EnableBuffering();
                request.Body.Position = 0;
                var json = await new StreamReader(request.Body).ReadToEndAsync();

                var webhookSecret = _stripeSettings.WebhookSecret;

                _logger.LogInformation("Webhook Raw Body: {Json}", json);

                if (!request.Headers.TryGetValue("Stripe-Signature", out var stripeSignature))
                {
                    _logger.LogWarning("Missing Stripe-Signature header.");
                    return;
                }

                var stripeEvent = EventUtility.ConstructEvent(json, request.Headers["Stripe-Signature"], webhookSecret);

                if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    var donorId = paymentIntent.Metadata["donorId"];
                    var amount = decimal.Parse(paymentIntent.Metadata["amount"]);
                    var projectId = string.IsNullOrEmpty(paymentIntent.Metadata["projectId"]) ? null : paymentIntent.Metadata["projectId"];
                    var paymentMethodType = paymentIntent.PaymentMethodTypes?.FirstOrDefault() ?? "unknown";

                    using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
                    try
                    {
                        var donation = await _unitOfWork.MonetaryDonations.GetByAsync(d => d.PaymentIntentId == paymentIntent.Id);

                        if (donation == null)
                        {
                            donation = new MonetaryDonation
                            {
                                DonorId = donorId,
                                Amount = amount,
                                ProjectId = projectId,
                                IsPaymentConfirmed = true,
                                PaymentIntentId = paymentIntent.Id,
                                PaymentMethod = paymentMethodType
                            };

                            await _unitOfWork.MonetaryDonations.CreateAsync(donation, cancellationToken);
                        }
                        else
                        {
                            donation.IsPaymentConfirmed = true;
                            donation.PaymentMethod = paymentMethodType;

                            await _unitOfWork.MonetaryDonations.UpdateAsync(donation, cancellationToken);
                        }

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during webhock.");
            }
        }


    }
}
