using WSS.API.Infrastructure.Interfaces;
using WSS.API.Infrastructure.Repositories;
using WSS.API.Integration.Clients;
using WSS.API.Integration.Interfaces;
using WSS.API.Services;

namespace WSS.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<WorkScheduleService>();
            services.AddScoped<ChangeRequestService>();
            services.AddScoped<VerificationService>();
            return services;
        }

        public static IServiceCollection AddIntegrationClients(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IObjectManagementClient, ObjectManagementClient>(client =>
            {
                client.BaseAddress = new Uri(config["ExternalServices:ObjectManagementUrl"] ?? "http://object-management-service");
            });

            services.AddHttpClient<INotificationClient, NotificationClient>(client =>
            {
                client.BaseAddress = new Uri(config["ExternalServices:NotificationUrl"] ?? "http://notification-service");
            });

            return services;
        }
    }
}
