
using Delivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delivery.Infrastructure.Configurations
{
    public class DeliveryDocumentConfiguration : IEntityTypeConfiguration<DeliveryDocument>
    {
        public void Configure(EntityTypeBuilder<DeliveryDocument> builder)
        {
            builder.ToTable("documents");

            // Primary Key
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id").IsRequired();

            // Св-ва
            builder.Property(u => u.DocumentNumber).HasColumnName("documentnumber").IsRequired();
            builder.Property(u => u.CargoType).HasColumnName("cargotype").IsRequired(); 
            builder.Property(u => u.CargoVolume).HasColumnName("cargovolume").IsRequired(); 
            builder.Property(u => u.VolumeUnit).HasColumnName("volumeunit").IsRequired(); 
            builder.Property(u => u.ShippedAt).HasColumnName("shippedat").IsRequired(); 
            builder.Property(u => u.PasportId).HasColumnName("pasportid").IsRequired();

            // Св-ва хранящиеся в виде ValueObjectов
            builder.OwnsOne(u => u.AccompanyingDocuments, ad =>
            {
                ad.Property(accd => accd.DocumentIds)
                    .HasColumnName("accompanyingdocumentsids"); 
            });
 
        }
    }
}
