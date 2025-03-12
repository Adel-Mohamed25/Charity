using Charity.Contracts.ServicesAbstractions;
using Charity.Infrastructure.Settings;
using Charity.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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


            services.AddScoped<IUnitOfService, UnitOfService>();
            services.AddScoped<IUnitOfServices, AuthServices>();
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IEmailServices, EmailServices>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
