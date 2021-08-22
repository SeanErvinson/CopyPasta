using System.Threading.Tasks;
using CopyPasta.Api.Interfaces;
using Google.Cloud.Firestore;
using CopyPasta.Api.Domains;
using System.Linq;
using System.Threading;
using System.Dynamic;
using Newtonsoft.Json;

namespace CopyPasta.Api.Services
{
    public class FirestorePostRepository : IPostRepository
    {
        private const string CollectionName = "posts";
        private readonly FirestoreDb _database;
        public FirestorePostRepository()
        {
            _database = FirestoreDb.Create("copypasta-project");
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
            var data = snapshot.Documents.FirstOrDefault()?.ConvertTo<object>();
            if (data is null)
                return null;
            var jsonData = JsonConvert.SerializeObject(data);
            return JsonConvert.DeserializeObject<Post>(jsonData);
        }
    }
}