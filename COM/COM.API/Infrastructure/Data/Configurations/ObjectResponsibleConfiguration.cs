using COM.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace COM.API.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Конфигурация маппинга для сущности ObjectResponsible.
    /// Настраивает составной первичный ключ и внешние ключи.
    /// </summary>
    public class ObjectResponsibleConfiguration : IEntityTypeConfiguration<ObjectResponsible>
    {
        public void Configure(EntityTypeBuilder<ObjectResponsible> builder)
        {
            builder.ToTable("object_responsibles");

            // Составной первичный ключ
            builder.HasKey(or => new { or.ConstructionObjectId, or.UserId, or.Role });

            builder.Property(or => or.ConstructionObjectId).HasColumnName("construction_object_id").IsRequired();
            builder.Property(or => or.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(or => or.Role).HasColumnName("role").IsRequired();

            // Внешний ключ к ConstructionObject
            builder.HasOne<ConstructionObject>()
                   .WithMany(co => co.Responsibles) // Обратная навигация
                   .HasForeignKey(or => or.ConstructionObjectId)
                   .OnDelete(DeleteBehavior.Cascade); // При удалении объекта — удаляем всех ответственных
        }
    }
}
