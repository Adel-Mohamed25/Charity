using AutoMapper;
using Charity.Application.Helper.ResponseServices;
using Charity.Contracts.Repositories;
using Charity.Contracts.ServicesAbstraction;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Domain.Enum;
using Charity.Models.Email;
using Charity.Models.ResponseModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace Charity.Application.Features.V1.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
    {
        private readonly IUnitOfService _unitOfService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterCommand> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RegisterCommandHandler(IUnitOfService unitOfService,
            IUnitOfWork unitOfWork,
            ILogger<RegisterCommand> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfService = unitOfService;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region Get IP address from HttpContext
                var remoteIpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress;

                if (remoteIpAddress != null && remoteIpAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    _logger.LogInformation($"Client IPv4 Address: {remoteIpAddress}");
                }
                else
                {
                    _logger.LogWarning("IPv4 Address Not Found.");
                }
                #endregion

                #region Get host details
                var hostName = Dns.GetHostName();
                var hostEntry = await Dns.GetHostEntryAsync(hostName);
                _logger.LogInformation($"Host Name: {hostEntry.HostName}");
                #endregion

                var user = _mapper.Map<CharityUser>(request.CreateUser);
                user.UserName = request.CreateUser.Email;
                user.CreatedDate = DateTime.UtcNow;
                IdentityResult result = await _unitOfWork.CharityUsers.UserManager.CreateAsync(user, request.CreateUser.Password);

                if (!result.Succeeded)
                    return ResponseHandler.Conflict<string>(message: "Failed to create user.");

                await _unitOfWork.CharityUsers.UserManager.AddToRoleAsync(user, nameof(UserRole.Beneficiary));
                var token = await _unitOfWork.CharityUsers.UserManager.GenerateEmailConfirmationTokenAsync(user);

                var url = $"{_httpContextAccessor.HttpContext.Request.Scheme.Trim().ToLower()}://{_httpContextAccessor.HttpContext.Request.Host.ToUriComponent().Trim().ToLower()}/api/v1/Account/ConfirmEmail";

                var parameters = new Dictionary<string, string>
                {
                    {"Token", token },
                    {"UserId", user.Id}
                };

                var confirmationLink = new Uri(QueryHelpers.AddQueryString(url, parameters));
                var sendEmailModel = new SendEmailRequest
                {
                    To = user.Email!,
                    Subject = "التحقق من البريد الإلكتروني الخاص بك  من  جمعية يد العطاء",
                    Body = $@"
                           <div style='font-family: Arial, sans-serif; direction: rtl; text-align: center;'>
                               <h2 style='color: #333;'>التحقق من البريد الإلكتروني الخاص بك</h2>
                               
                               <p style='font-size: 16px; color: #555;'>
                                   شكرًا لانضمامك إلى <strong>جمعية يد العطاء</strong>!  
                                   لقد اقتربت من الوصول، فقط انقر على الزر أدناه للتحقق من بريدك الإلكتروني.
                               </p>
                           
                               <a href='{confirmationLink}' 
                                  style='display: inline-block; padding: 12px 24px; font-size: 16px; 
                                         color: #fff; background-color: #1a73e8; text-decoration: none;
                                         border-radius: 10px; font-weight: bold; margin-top: 10px;'>
                                   تأكيد البريد الإلكتروني
                               </a>
                           
                               <p style='font-size: 14px; color: #555; font-weight: bold;'>
                                   مع خالص التحيات، <br>
                                   <strong>جمعية يد العطاء</strong>
                               </p>
                           </div>"

                };

                var emaiModel = await _unitOfService.EmailServices.SendEmailAsync(sendEmailModel);
                if (emaiModel.IsSuccess)
                    return ResponseHandler.NoContent<string>(message: "The account has been registered successfully.", data: $"{user.EmailConfirmed}");
                return ResponseHandler.BadRequest<string>(message: "Failed to send confirmation email");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during user register");
                return ResponseHandler.BadRequest<string>(errors: ex.Message);
            }
        }
    }
}
