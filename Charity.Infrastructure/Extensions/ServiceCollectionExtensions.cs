using Charity.Contracts.Repositories;
using Charity.Contracts.RepositoryAbstractions.IdentityRepositories;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Infrastructure.Implementations.IdentityRepository;
using Charity.Infrastructure.Repositories;
using Charity.Infrastructure.Settings;
using Charity.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Charity.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ICharityDbContext, CharityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CharityConnection")));

            #region Verification code validity settings
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromMinutes(5);
            });
            #endregion

            #region Identity Settings 
            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 8;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

            }).AddEntityFrameworkStores<CharityDbContext>()
            .AddDefaultTokenProviders();
            #endregion

            #region DI Settings
            services.AddScoped<ICharityDbContext, CharityDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenRepository, JwtTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<SignInManager<User>>();
            services.AddScoped<RoleManager<Role>>();
            #endregion

            #region Configuration Settings
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.Configure<JWTSettings>(configuration.GetSection(nameof(JWTSettings)));
            #endregion

            return services;
        }
    }
}
