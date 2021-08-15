using System;
using System.Threading;
using System.Threading.Tasks;
using CopyPasta.Api.Exceptions;
using CopyPasta.Api.Interfaces;
using MediatR;

namespace CopyPasta.Api.Posts.Queries
{
    public record GetPostInformationQuery(string link) : IRequest<PostInformationDto>
    {
        public class Handler : IRequestHandler<GetPostInformationQuery, PostInformationDto>
        {
            private readonly IPostRepository _postRepository;

            public Handler(IPostRepository postRepository)
            {
                _postRepository = postRepository;
            }

            public async Task<PostInformationDto> Handle(GetPostInformationQuery request, CancellationToken cancellationToken)
            {
                var post = await _postRepository.GetPostAsync(request.link);
                if (post is null)
                {
                    throw new NotFoundException("Post could not be found");
                }
                if (post.ExpiresIn < DateTime.UtcNow)
                {
                    throw new BadRequestException("Post has expired");
                }
                return new PostInformationDto(post.Id, post.Password != null, post.ExpiresIn);
            }
        }
    }
    public record PostInformationDto(Guid Id, bool IsPasswordProtected, DateTime? expiresIn);
}