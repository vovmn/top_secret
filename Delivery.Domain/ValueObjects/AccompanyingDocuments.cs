using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.ValueObjects
{
    public sealed record AccompanyingDocuments : IEnumerable<string>
    {
        private readonly IReadOnlyCollection<string> _documentIds;

        public IEnumerable<string> DocumentIds => _documentIds;

        public AccompanyingDocuments(IEnumerable<string> documentIds)
        {
            ArgumentNullException.ThrowIfNull(documentIds);

            _documentIds = [.. documentIds];

            // Валидация данных
            if (_documentIds.Count == 0)
                throw new InvalidOperationException("DocumentIds collection cannot be empty");

            foreach (var id in _documentIds)
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentException("DocumentId cannot be null or empty");
        }

        // Реализация IEnumerable
        public IEnumerator<string> GetEnumerator() => _documentIds.GetEnumerator();
        

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 

        private AccompanyingDocuments() { }
    }
}
