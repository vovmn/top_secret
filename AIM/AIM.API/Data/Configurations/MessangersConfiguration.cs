using AIM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIM.API.Data.Configurations
{
    public class MessangersConfiguration : IEntityTypeConfiguration<Messangers>
    {
        public void Configure(EntityTypeBuilder<Messangers> builder)
        {
            builder.ToTable("messangers");

            builder.HasKey(m => m.UserId);
            builder.Property(m => m.UserId).HasColumnName("userid").IsRequired();

            builder.Property(m => m.WhatsApp).HasColumnName("whatsapp").HasMaxLength(100);
            builder.Property(m => m.VK).HasColumnName("vk").HasMaxLength(100);
            builder.Property(m => m.Max).HasColumnName("max").HasMaxLength(100);
            builder.Property(m => m.Telegram).HasColumnName("telegram").HasMaxLength(100);
            builder.Property(m => m.Other).HasColumnName("other").HasMaxLength(100);

            // Внешний ключ к User 
            builder.HasOne<User>()
                .WithOne(u => u.Messangers)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
