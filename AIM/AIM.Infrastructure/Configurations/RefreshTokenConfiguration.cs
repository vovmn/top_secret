using AIM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIM.Infrastructure.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");

            // Primary Key
            builder.HasKey(rt => rt.Token);
            builder.Property(rt => rt.Token).HasColumnName("token").IsRequired();

            // Св-ва
            builder.Property(rt => rt.Expires).HasColumnName("expires_at").IsRequired();


            // Внешний ключ для связи с User
            builder.Property(rt => rt.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            // Настройка связи (У User может быть много RefreshToken'ов)
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade) // Каскадное удаление токенов
                .IsRequired();
        }
    }
}
