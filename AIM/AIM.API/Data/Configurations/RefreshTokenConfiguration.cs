using AIM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIM.API.Data.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refreshtokens");

            builder.HasKey(co => co.Id);
            builder.Property(co => co.Id).HasColumnName("id").IsRequired();
            builder.Property(co => co.Token).HasColumnName("token").IsRequired();
            builder.Property(co => co.Expires).HasColumnName("expires").IsRequired();

        }
    }
}
