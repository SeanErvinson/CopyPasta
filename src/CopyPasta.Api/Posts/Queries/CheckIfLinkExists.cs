using System.Threading;
using System.Threading.Tasks;
using CopyPasta.Api.Interfaces;
using MediatR;

namespace CopyPasta.Api.Posts.Queries
{
    public record CheckIfLinkExists(string link) : IRequest<bool>
    {
        public class Handler : IRequestHandler<CheckIfLinkExists, bool>
        {
            private readonly IPostRepository _postRepository;

            public Handler(IPostRepository postRepository)
            {
                _postRepository = postRepository;
            }

            public async Task<bool> Handle(CheckIfLinkExists request, CancellationToken cancellationToken)
            {
                var post = await _postRepository.GetPostAsync(request.link);
                if (post is not null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}