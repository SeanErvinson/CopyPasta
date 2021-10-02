using System;
using System.Threading;
using System.Threading.Tasks;
using CopyPasta.Api.Exceptions;
using CopyPasta.Api.Interfaces;
using CopyPasta.Api.Options;
using CopyPasta.Api.Utils;
using MediatR;
using Microsoft.Extensions.Options;

namespace CopyPasta.Api.Posts.Queries
{
    public record GetPostQuery(string Link, string? Password) : IRequest<PostDto>
    {
        public class Handler : IRequestHandler<GetPostQuery, PostDto>
        {
            private readonly IPostRepository _postRepository;
            private readonly SecurityOptions _securityOptions;

            public Handler(IPostRepository postRepository, IOptions<SecurityOptions> options)
            {
                _postRepository = postRepository;
                _securityOptions = options.Value;
            }

            public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
            {
                var post = await _postRepository.GetPostAsync(request.Link);
                if (post is null)
                {
                    throw new NotFoundException("Post could not be found");
                }
                if (post.Password is not null)
                {
                    if (request.Password is null)
                    {
                        throw new BadRequestException("Password must be provided");
                    }
                    var hashedPassword = await PasswordHashUtility.GenerateHashAsync(request.Password, _securityOptions.Salt);
                    if (post.Password != hashedPassword)
                    {
                        throw new BadRequestException("Password is incorrect");
                    }
                }
                if (post.ExpiresIn < DateTime.UtcNow)
                    throw new BadRequestException("Post has expired");
                return new PostDto(post.Content, post.ExpiresIn, post.CreatedOn);
            }
        }
    }
    public record PostDto(string content, DateTime? expiresIn, DateTime CreatedOn, string? CreatedBy = null);
}