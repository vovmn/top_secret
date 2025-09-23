using COM.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace COM.API.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// DbSet для сущности ConstructionObject.
        /// </summary>
        public DbSet<ConstructionObject> ConstructionObjects { get; set; } = null!;

        /// <summary>
        /// DbSet для сущности ObjectResponsible.
        /// </summary>
        public DbSet<ObjectResponsible> ObjectResponsibles { get; set; } = null!;

        /// <summary>
        /// DbSet для сущности Checklist.
        /// </summary>
        public DbSet<Checklist> Checklists { get; set; } = null!;

        /// <summary>
        /// Настраивает маппинг сущностей на таблицы с помощью Fluent API.
        /// Вызывается EF Core при инициализации модели.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Применяем конфигурации из папки Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }


    }
}
