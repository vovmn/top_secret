using AIM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIM.API.Data.Configurations
{
    public class LoginInfoConfiguration : IEntityTypeConfiguration<LoginInfo>
    {
        public void Configure(EntityTypeBuilder<LoginInfo> builder)
        {
            builder.ToTable("login_info");

            builder.HasKey(li => li.UserId);
            builder.Property(li => li.UserId).HasColumnName("userid").IsRequired();

            builder.Property(li => li.Username).HasColumnName("username").HasMaxLength(50).IsRequired();
            builder.Property(li => li.Email).HasColumnName("email").HasMaxLength(100);
            builder.Property(li => li.PhoneNumber).HasColumnName("phone").HasMaxLength(13);

            // Внешний ключ к User 
            builder.HasOne<User>()
                .WithOne(u => u.LoginInfo)
                .HasForeignKey(li => li.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
