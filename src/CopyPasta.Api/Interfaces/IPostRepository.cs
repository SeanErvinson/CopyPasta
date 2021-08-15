using System.Threading;
using System.Threading.Tasks;
using CopyPasta.Api.Domains;
using CopyPasta.Api.Dtos;

namespace CopyPasta.Api.Interfaces
{
    public interface IPostRepository
    {
        Task CreatePostAsync(Post newPost, CancellationToken cancellationToken = default);
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<PostDto?> GetPostAsync(string link, CancellationToken cancellationToken = default);
    }
}