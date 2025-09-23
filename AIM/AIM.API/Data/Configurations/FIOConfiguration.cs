using AIM.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIM.API.Data.Configurations
{
    public class FIOConfiguration : IEntityTypeConfiguration<FIO>
    {
        public void Configure(EntityTypeBuilder<FIO> builder)
        {
            builder.ToTable("fio");

            builder.HasKey(fio => fio.UserId);
            builder.Property(fio => fio.UserId).HasColumnName("userid").IsRequired();

            builder.Property(fio => fio.Name).HasColumnName("name").HasMaxLength(20);
            builder.Property(fio => fio.Sername).HasColumnName("surname").HasMaxLength(20);
            builder.Property(fio => fio.Fathername).HasColumnName("fathername").HasMaxLength(20);

            // Внешний ключ к User 
            builder.HasOne<User>()
                .WithOne(u => u.FIO)
                .HasForeignKey(fio => fio.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
