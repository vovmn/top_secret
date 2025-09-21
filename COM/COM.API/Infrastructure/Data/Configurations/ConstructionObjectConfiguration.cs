using COM.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace COM.API.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Конфигурация маппинга для сущности ConstructionObject.
    /// Настраивает таблицу, столбцы, первичный ключ и индексы.
    /// </summary>
    public class ConstructionObjectConfiguration : IEntityTypeConfiguration<ConstructionObject>
    {
        public void Configure(EntityTypeBuilder<ConstructionObject> builder)
        {
            builder.ToTable("construction_objects");

            builder.HasKey(co => co.Id);
            builder.Property(co => co.Id).HasColumnName("id").IsRequired();

            builder.Property(co => co.Name).HasColumnName("name").HasMaxLength(500).IsRequired();
            builder.Property(co => co.Address).HasColumnName("address").HasMaxLength(1000);
            builder.Property(co => co.Status).HasColumnName("status").IsRequired();
            builder.Property(co => co.StartDate).HasColumnName("start_date");
            builder.Property(co => co.EndDate).HasColumnName("end_date");

            // Для полигона мы будем хранить его как текст (GeoJSON или WKT) в PostgreSQL.
            // В продакшене лучше использовать тип geometry, но для упрощения демо — TEXT.
            builder.Property(co => co.Polygon)
                   .HasColumnName("polygon_geojson")
                   .HasColumnType("geometry(Polygon, 4326)") 
                   .IsRequired();

            // Индекс для быстрого поиска по статусу
            builder.HasIndex(co => co.Status);
        }
    }
}
