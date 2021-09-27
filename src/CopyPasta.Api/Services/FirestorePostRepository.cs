using System.Threading.Tasks;
using CopyPasta.Api.Interfaces;
using Google.Cloud.Firestore;
using CopyPasta.Api.Domains;
using System.Linq;
using System.Threading;
using System.Dynamic;
using Newtonsoft.Json;
using CopyPasta.Api.Firestore;

namespace CopyPasta.Api.Services
{
    public class FirestorePostRepository : IPostRepository
    {
        private const string CollectionName = "posts";
        private readonly FirestoreDb _database;
        
        public FirestorePostRepository()
        {
            _database = new FirestoreDbBuilder
            {
                ProjectId = "copypasta-project",
                ConverterRegistry = new ConverterRegistry
                {
                    new GuidConverter()
                }
            }.Build();
        }
        public async Task CreatePostAsync(Post post, CancellationToken cancellationToken = default)
        {
            var jsonData = JsonConvert.SerializeObject(post);
            var data = JsonConvert.DeserializeObject<ExpandoObject>(jsonData);
            var collection = _database.Collection(CollectionName);
            var document = await collection.AddAsync(data, cancellationToken);
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            var collection = _database.Collection(CollectionName);
            var snapshot = await collection.GetSnapshotAsync(cancellationToken);
            return snapshot.Count;
        }

        public async Task<Post?> GetPostAsync(string link, CancellationToken cancellationToken = default)
        {
            var collection = _database.Collection(CollectionName);
            var query = collection.WhereEqualTo("Link", link).Limit(1);
            var snapshot = await query.GetSnapshotAsync(cancellationToken);
            var data = snapshot.Documents.FirstOrDefault()?.ConvertTo<Post>();
            return data;
        }
    }
}