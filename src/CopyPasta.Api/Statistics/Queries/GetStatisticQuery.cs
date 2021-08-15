using System.Threading;
using System.Threading.Tasks;
using CopyPasta.Api.Interfaces;
using MediatR;

namespace CopyPasta.Api.Statistic.Queries
{
    public record GetStatisticQuery : IRequest<StatisticDto>
    {
        public class Handler : IRequestHandler<GetStatisticQuery, StatisticDto>
        {
            private readonly IPostRepository _postRepository;

            public Handler(IPostRepository postRepository)
            {
                _postRepository = postRepository;
            }

            public async Task<StatisticDto> Handle(GetStatisticQuery request, CancellationToken cancellationToken)
            {
                var postCount = await _postRepository.GetCountAsync();

                return new StatisticDto(postCount);
            }
        }
    }

    public record StatisticDto(int postCount);
}