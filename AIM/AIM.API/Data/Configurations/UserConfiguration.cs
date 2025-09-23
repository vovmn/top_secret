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

            builder.Property(co => co.Password).HasColumnName("password").IsRequired();

            builder.Property(co => co.Priveleges).HasColumnName("priveleges").IsRequired();

        }
    }
}
