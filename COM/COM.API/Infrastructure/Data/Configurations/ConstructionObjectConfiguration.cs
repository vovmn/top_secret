using COM.API.Domain.Entities;
using COM.API.Infrastructure.Data.Converters;
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

            // Храним полигон как TEXT (WKT), а не geometry
            builder.Property(co => co.Polygon)
                   .HasColumnName("polygon")
                   .HasColumnType("TEXT")
                   .HasConversion(new GeoPolygonConverter())
                   .IsRequired();

            builder.HasIndex(co => co.Status);

            // Настройка доступа к приватным коллекциям через backing fields
            var responsiblesNav = builder.Metadata.FindNavigation(nameof(ConstructionObject.Responsibles));
            responsiblesNav?.SetPropertyAccessMode(PropertyAccessMode.Field);

            var checklistsNav = builder.Metadata.FindNavigation(nameof(ConstructionObject.Checklists));
            checklistsNav?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
