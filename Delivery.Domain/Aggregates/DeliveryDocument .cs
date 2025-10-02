using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Domain.Aggregates
{
    internal class DeliveryDocument : AggregateRoot
    {
        public DocumentId Id { get; }
        public DocumentNumber Number { get; private set; }
        public CargoType Type { get; private set; }
        public Volume Quantity { get; private set; }
        public ProcessingStatus Status { get; private set; }
        public UserId Initiator { get; }
        public DateTime CreatedAt { get; }
        public ProcessingMethod Method { get; }
        public ValidationResult Validation { get; private set; }

        // Методы для изменения состояния
        public void ProcessAutomatically(FileContent image) { ... }
        public void SubmitManualData(ManualInputData data) { ... }
        public void Validate() { ... }
    }
}
}
