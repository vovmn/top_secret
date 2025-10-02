using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Delivery.Domain.Entities
{
    public sealed class DeliveryDocument
    {
        [Required]
        public Guid Id { get; private set; }

        [Required]
        public string DocumentNumber { get; private set; }

        [Required]
        public string CargoType { get; private set; }

        [Required]
        public decimal CargoVolume { get; private set; }

        [Required]
        public string VolumeUnit { get; private set; }

        [Required]
        public DateTime ShippedAt { get; private set; }

        /// <summary>
        /// Уникальный идентификатор файла в системе File Storage Service (например, "uploads/acts/abc123.pdf").
        /// Используется для хранения ссылок на PDF или изображения, как указано в ТЗ.
        /// </summary>
        [Required]
        public string PasportId { get; private set; }
    }

}
