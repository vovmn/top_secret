using IAM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IAM.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            // Primary Key
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id").IsRequired();

            // Св-ва
            builder.Property(u => u.Password).HasColumnName("password").IsRequired();
            builder.Property(u => u.Priveleges).HasColumnName("priveleges").IsRequired();

            // Св-ва хранящиеся в виде ValueObjectов
            builder.OwnsOne(u => u.FIO, f =>
            {
                f.Property(fio => fio.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                f.Property(fio => fio.Sername)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("sername");

                f.Property(fio => fio.Fathername)
                    .HasMaxLength(50)
                    .HasColumnName("fathername");
            });

            builder.OwnsOne(u => u.LoginInfo, l =>
            {
                l.Property(li => li.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("username");

                l.Property(li => li.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                l.Property(li => li.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phonenumber");
            });

            builder.OwnsOne(u => u.Messengers, m =>
            {
                m.Property(mes => mes.WhatsApp)
                    .HasMaxLength(100)
                    .HasColumnName("whatsapp");

                m.Property(mes => mes.VK)
                    .HasMaxLength(100)
                    .HasColumnName("vk");

                m.Property(mes => mes.Max)
                    .HasMaxLength(100)
                    .HasColumnName("max");

                m.Property(mes => mes.Telegram)
                    .HasMaxLength(100)
                    .HasColumnName("telegram");

                m.Property(mes => mes.Other)
                    .HasMaxLength(100)
                    .HasColumnName("othermessenger");
            });
        }
    }
}
