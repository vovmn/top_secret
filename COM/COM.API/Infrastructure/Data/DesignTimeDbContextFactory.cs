using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace COM.API.Infrastructure.Data
{
    /// <summary>
    /// Фабрика для создания ApplicationDbContext в режиме дизайн-тайма (например, при запуске команд dotnet ef).
    /// Позволяет EF Core Tools создать контекст БД для генерации миграций.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Создаем построитель конфигурации для чтения appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Получаем строку подключения из конфигурации
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Настраиваем опции для DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(connectionString); // Используем PostgreSQL

            // Возвращаем сконфигурированный контекст
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
