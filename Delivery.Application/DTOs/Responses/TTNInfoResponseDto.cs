using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.DTOs.Responses
{
    public class TTNInfoResponseDto
    {
        public Guid Id { get; set; }

        public string? DocumentNumber { get; set; }

        public string? CargoType { get; set; }

        public decimal CargoVolume { get; set; }

        public string? VolumeUnit { get; set; }

        public DateTime ShippedAt { get; set; }

        public string? PasportId { get; set; }
    }
}
