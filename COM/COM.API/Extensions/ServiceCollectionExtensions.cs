using COM.API.Application.Interfaces;
using COM.API.Application.Services;
using COM.API.Infrastructure.Data;
using COM.API.Infrastructure.Integration;
using COM.API.Infrastructure.Interfaces;
using COM.API.Infrastructure.Repositories;
using COM.API.Profiles;
using Microsoft.EntityFrameworkCore;


namespace COM.API.Extensions
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
        public static IServiceCollection AddConstructionObjectManagement(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // 2. Репозитории
            services.AddScoped<IObjectRepository, ObjectRepository>();
            services.AddScoped<IChecklistRepository, ChecklistRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // ← не забудьте зарегистрировать UnitOfWork!

            // 3. Application Services
            services.AddScoped<IObjectService, ObjectService>();
            services.AddScoped<IChecklistService, ChecklistService>();

            // 4. Infrastructure Services
            services.AddScoped<IGeospatialService, GeospatialService>();

            // Вариант B: RabbitMQ (если хотите показать интеграцию)
             services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();

            // 5. File Storage Client — регистрируем через интерфейс!
            services.AddHttpClient<IFileStorageClient, FileStorageClient>((sp, client) =>
            {
                var baseUrl = sp.GetRequiredService<IConfiguration>()
                    .GetValue<string>("FileStorageService:BaseUrl")
                    ?? throw new InvalidOperationException("FileStorageService:BaseUrl не настроен.");
                client.BaseAddress = new Uri(baseUrl);
            });

            // 6. AutoMapper
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile).Assembly));

            return services;
        }
    }
}
