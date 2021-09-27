using System;
using Google.Cloud.Firestore;

namespace CopyPasta.Api.Firestore
{
    public class GuidConverter : IFirestoreConverter<Guid>
    {
        public object ToFirestore(Guid value) => value.ToString("N");

        public Guid FromFirestore(object value)
        {
            switch (value)
            {
                case string guid: return Guid.Parse(guid);
                case null: throw new ArgumentNullException(nameof(value));
                default: throw new ArgumentException($"Unexpected data: {value.GetType()}");
            }
        }
    }
}