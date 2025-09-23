using AIM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIM.API.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(co => co.Id);
            builder.Property(co => co.Id).HasColumnName("id").IsRequired();

            builder.Property(co => co.UserName).HasColumnName("username").HasMaxLength(50).IsRequired();
            builder.Property(co => co.EMail).HasColumnName("email").HasMaxLength(100);
            builder.Property(co => co.PhoneNumber).HasColumnName("phone").HasMaxLength(13);

            builder.Property(co => co.Password).HasColumnName("password").IsRequired();

            builder.Property(co => co.Name).HasColumnName("name").HasMaxLength(20);
            builder.Property(co => co.Sername).HasColumnName("surname").HasMaxLength(20);
            builder.Property(co => co.Fathername).HasColumnName("fathernamee").HasMaxLength(20);

            builder.Property(co => co.Messangers).HasColumnName("messangers").HasMaxLength(200);

            builder.Property(co => co.Priveleges).HasColumnName("priveleges").IsRequired();

        }
    }
}
