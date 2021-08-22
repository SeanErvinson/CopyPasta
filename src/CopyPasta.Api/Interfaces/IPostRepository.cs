using System.Threading;
using System.Threading.Tasks;
using CopyPasta.Api.Domains;

namespace CopyPasta.Api.Interfaces
{
    public interface IPostRepository
    {
        Task CreatePostAsync(Post newPost, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<Post?> GetPostAsync(string link, CancellationToken cancellationToken = default);
    }
}