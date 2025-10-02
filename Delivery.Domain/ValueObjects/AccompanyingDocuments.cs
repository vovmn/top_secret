using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.ValueObjects
{
    sealed record AccompanyingDocuments : IEnumerable<string>, IEquatable<AccompanyingDocuments>
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

        // Реализация IEquatable
        public bool Equals(AccompanyingDocuments other)
        {
            if (other == null) return false;
            return _documentIds.SequenceEqual(other._documentIds);
        }

        public override bool Equals(object obj)
        {
            if (obj is AccompanyingDocuments other)
                return Equals(other);
            return false;
        }

        public override int GetHashCode()
        {
            return _documentIds.Aggregate(0, (hash, id) => hash ^ id.GetHashCode());
        }

        private AccompanyingDocuments() { }
    }
}
