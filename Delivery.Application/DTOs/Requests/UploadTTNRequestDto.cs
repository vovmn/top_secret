using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Application.DTOs.Requests
{
    public class UploadTTNRequestDto
    {
        public Guid ImageId { get; set; }
        public Stream? File { get; set; }
        public string? FileName { get; set; }
        public string? Content { get; set; } // JSON-содержимое
    }
}
