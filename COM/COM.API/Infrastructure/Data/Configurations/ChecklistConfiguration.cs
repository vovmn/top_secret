using COM.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COM.API.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Конфигурация маппинга для сущности Checklist.
    /// </summary>
    public class ChecklistConfiguration : IEntityTypeConfiguration<Checklist>
    {
        public void Configure(EntityTypeBuilder<Checklist> builder)
        {
            builder.ToTable("checklists");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").IsRequired();

            builder.Property(c => c.ConstructionObjectId).HasColumnName("construction_object_id").IsRequired();
            builder.Property(c => c.Type).HasColumnName("type").IsRequired();
            builder.Property(c => c.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(c => c.FileId).HasColumnName("file_id").HasMaxLength(1000);
            builder.Property(c => c.Content).HasColumnName("content").HasColumnType("JSONB"); // PostgreSQL JSONB тип

            // Внешний ключ к ConstructionObject
            builder.HasOne<ConstructionObject>()
                   .WithMany(co => co.Checklists)
                   .HasForeignKey(c => c.ConstructionObjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
