using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Models.Email;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Charity.Application.Features.V1.Authentication.Commands.GenerateVerifyCode
{
    public class SendVerifyCodeCommandHandler : IRequestHandler<SendVerifyCodeCommand, Response<string>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SendVerifyCodeCommandHandler> _logger;

        public SendVerifyCodeCommandHandler(IUnitOfService unitOfService,
            IUnitOfWork unitOfWork,
            ILogger<SendVerifyCodeCommandHandler> logger)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Response<string>> Handle(SendVerifyCodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.CharityUsers.UserManager.FindByEmailAsync(request.UserEmail.Email);

                if (user is null)
                    return ResponseHandler.NotFound<string>(message: "User Not Found");

                var code = await _unitOfService.AuthServices.GenerateVerificationCodeAsync(user);
                var emailModel = new SendEmailRequest
                {
                    To = request.UserEmail.Email,
                    Subject = "رمز التحقق الخاص بك من جمعية يد العطاء",
                    Body = $@"
                           <div style='font-family: Arial, sans-serif; direction: rtl; text-align: right;'>
                               <p style='font-size: 16px;'>عزيزي العميل،</p>
                           
                               <p style='font-size: 18px; font-weight: bold;'>
                                   رمز التحقق الخاص بك هو: 
                                   <span style='color: blue; font-size: 20px;'>{code}</span>
                               </p>
                           
                               <p style='font-size: 16px; color: #555;'>
                                   يرجى الحفاظ على سرية هذا الرمز وعدم مشاركته مع أي شخص. 
                                   لن نطلب منك أبدًا هذا الرمز.
                               </p>
                           
                               <p style='font-size: 16px; font-weight: bold; color: red;'>
                                   هذا الرمز صالح لمدة 5 دقائق فقط.
                               </p>
                           
                               <p style='font-size: 16px; font-weight: bold;'>
                                   مع خالص التحيات،
                                   <br>
                                   <span style='font-size: 18px; color: #000;'>جمعية يد العطاء</span>
                               </p>
                           </div>"
                };

                var emaiModel = await _unitOfService.EmailServices.SendEmailAsync(emailModel);
                if (emaiModel.IsSuccess)
                    return ResponseHandler.NoContent<string>(message: "Verification code sent successfully.");
                return ResponseHandler.Conflict<string>(message: "Failed to send confirmation email");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during send verification code.");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
