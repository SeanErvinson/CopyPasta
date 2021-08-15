using System;
using System.Threading;
using System.Threading.Tasks;
using CopyPasta.Api.Domains;
using CopyPasta.Api.Exceptions;
using CopyPasta.Api.Interfaces;
using CopyPasta.Api.Options;
using CopyPasta.Api.Posts.Queries;
using CopyPasta.Api.Utils;
using MediatR;
using Microsoft.Extensions.Options;

namespace CopyPasta.Api.Posts.Commands
{
    public record CreatePostCommand : IRequest
    {
        public string Content { get; init; } = string.Empty;
        public string? Password { get; init; }
        public double? Expiration { get; init; }
        public string? CustomLink { get; init; }

        public class Handler : IRequestHandler<CreatePostCommand>
        {
            private const int MaxRetries = 1000;
            private readonly IPostRepository _postRepository;
            private readonly SecurityOptions _securityOptions;
            private readonly IMediator _mediator;

            public Handler(
                IPostRepository postRepository,
                IOptions<SecurityOptions> options,
                IMediator mediator)
            {
                _postRepository = postRepository;
                _securityOptions = options.Value;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(CreatePostCommand request, CancellationToken cancellationToken)
            {
                var postLink = request.CustomLink;
                if (!string.IsNullOrEmpty(postLink))
                {
                    if (await _mediator.Send(new CheckIfLinkExists(postLink)))
                    {
                        throw new BadRequestException("Link already exists");
                    }
                }
                else
                {
                    int retries = 0;
                    do
                    {
                        postLink = LinkGeneratorUtility.GetLink(_securityOptions.LinkLength);
                    } while (await _postRepository.GetPostAsync(postLink) is not null && retries++ < MaxRetries);
                    if (retries >= MaxRetries)
                    {
                        throw new BadRequestException("Could not generate a link");
                    }
                }
                var newPost = new Post(postLink, request.Content);
                if (!string.IsNullOrEmpty(request.Password))
                {
                    await newPost.SetPassword(request.Password, _securityOptions.Salt);
                }
                if (request.Expiration is not null)
                {
                    newPost.ExpiresIn = DateTime.UtcNow.AddMinutes(request.Expiration ?? 0);
                }

                await _postRepository.CreatePostAsync(newPost, cancellationToken);
                return Unit.Value;
            }
        }
    }
}