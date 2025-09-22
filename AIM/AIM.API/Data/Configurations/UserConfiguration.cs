using AIM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIM.API.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(co => co.Id);
            builder.Property(co => co.Id).HasColumnName("id").IsRequired();
            builder.Property(co => co.UserName).HasColumnName("username").HasMaxLength(500).IsRequired();
            builder.Property(co => co.Password).HasColumnName("password");
            builder.Property(co => co.EMail).HasColumnName("email").HasMaxLength(1000);
            builder.Property(co => co.PhoneNumber).HasColumnName("phone").IsRequired();
            builder.Property(co => co.Name).HasColumnName("name");
            builder.Property(co => co.Sername).HasColumnName("surname");
            builder.Property(co => co.Fathername).HasColumnName("fathernamee");
          
        }
    }
}
