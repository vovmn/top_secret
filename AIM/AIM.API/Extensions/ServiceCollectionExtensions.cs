using AIM.Application.Interfaces;
using AIM.Application.Services;
using AIM.Infrastructure.Data;
using AIM.Infrastructure.Data.Interfaces;
using AIM.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AIM.API.Extensions
{
    /// <summary>
    /// Расширения для IServiceCollection, упрощающие регистрацию сервисов и настроек.
    /// Централизует всю логику внедрения зависимостей для данного микросервиса.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Регистрирует все зависимости, связанные с управлением объектами благоустройства.
        /// </summary>
        public static IServiceCollection AddJWTandUserThings(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    npgOptions => npgOptions.EnableRetryOnFailure()));

            // 2. Репозитории
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // 3. Сервисы
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();

            // 4. JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidAudience = configuration["JwtConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAuthorization();

            return services;
        }
    }
}
