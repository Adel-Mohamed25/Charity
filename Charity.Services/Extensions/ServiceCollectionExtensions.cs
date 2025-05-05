using Charity.Contracts.ServicesAbstraction;
using Charity.Infrastructure.Settings;
using Charity.Services.ServicesImplementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Charity.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region JwtAuthenticationSettings
            var jwtSection = configuration.GetSection($"{nameof(JWTSettings)}");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,

                    ValidIssuer = jwtSection.GetValue<string>($"{nameof(JWTSettings.Issuer)}"),
                    ValidateAudience = true,
                    ValidAudience = jwtSection.GetValue<string>($"{nameof(JWTSettings.Audience)}"),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwtSection.GetValue<string>($"{nameof(JWTSettings.Secret)}") ?? throw new InvalidOperationException("Secret key is missing")))
                };
            });
            #endregion

            #region Add Google Authentication 
            var googleSection = configuration.GetSection($"Authentication:{nameof(GoogleSettings)}");
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = googleSection.GetValue<string>($"{nameof(GoogleSettings.ClientId)}")
                ?? throw new InvalidOperationException("ClientId is missing");

                options.ClientSecret = googleSection.GetValue<string>($"{nameof(GoogleSettings.ClientSecret)}")
                ?? throw new InvalidOperationException("ClientSecret is missing");

                options.CallbackPath = new PathString("/api/v1/Account/GoogleLoginCallback");
            });
            #endregion

            #region Add Facebook Authentication 
            //var facebookSection = configuration.GetSection($"Authentication:{nameof(FacebookSettings)}");
            //services.AddAuthentication().AddFacebook(options =>
            //{
            //    options.AppId = facebookSection.GetValue<string>($"{nameof(FacebookSettings.AppId)}")
            //        ?? throw new InvalidOperationException("AppId is missing");

            //    options.AppSecret = facebookSection.GetValue<string>($"{nameof(FacebookSettings.AppSecret)}")
            //        ?? throw new InvalidOperationException("AppSecret is missing");

            //    options.CallbackPath = "/signin-facebook";
            //});
            #endregion

            #region Configure Swagger with JWT Authentication and able to read version correctly
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Charity API", Version = "v1" });

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your token",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type =  ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme,
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            #endregion


            #region DI Settings
            services.AddScoped<IUnitOfService, UnitOfService>();
            services.AddScoped<IUnitOfServices, AuthServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<INotificationServices, NotificationServices>();
            services.AddHttpContextAccessor();
            #endregion

            return services;
        }
    }
}
