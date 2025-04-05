using Charity.Application.Helper.ConfirmEmailMessage;
using Charity.Contracts.ServicesAbstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, IActionResult>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly ILogger<ConfirmEmailCommandHandler> _logger;

        public ConfirmEmailCommandHandler(IUnitOfService unitOfService,
            ILogger<ConfirmEmailCommandHandler> logger)
        {
            _unitOfService = unitOfService;
            _logger = logger;
        }
        public async Task<IActionResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _unitOfService.AuthServices.ConfirmEmailAsync(request.EmailConfirmation);

                if (response.IsConfirmed)
                {
                    if (response.Message == "Email has been confirmed in advance")
                    {
                        string htmlContent = ConfirmEmailFormat.GenerateHtmlContent("ℹ️ البريد الإلكتروني مؤكد مسبقًا. يمكنك تسجيل الدخول.",
                            "blue"
                        );

                        return new ContentResult
                        {
                            Content = htmlContent,
                            ContentType = "text/html",
                            StatusCode = 200
                        };
                    }

                    string successHtml = ConfirmEmailFormat.GenerateHtmlContent(
                        "✅ تم تأكيد البريد الإلكتروني بنجاح!",
                        "#2ecc71"
                    );
                    return new ContentResult
                    {
                        Content = successHtml,
                        ContentType = "text/html",
                        StatusCode = 200
                    };

                }

                string failureHtml = ConfirmEmailFormat.GenerateHtmlContent("❌ فشل تأكيد البريد الإلكتروني. الرجاء المحاولة لاحقًا.", "red");
                return new ContentResult
                {
                    Content = failureHtml,
                    ContentType = "text/html",
                    StatusCode = 400
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during confirm email.");
                return new ContentResult
                {
                    Content = $"<h2 style='text-align:center;color:red;'>❌ خطأ: {ex.Message}</h2>",
                    ContentType = "text/html",
                    StatusCode = 400
                };
            }

        }
    }
}
