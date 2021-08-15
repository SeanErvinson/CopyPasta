using System.Threading.Tasks;
using CopyPasta.Api.Statistic.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CopyPasta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StatisticDto))]
        public async Task<IActionResult> GetStatistic()
        {
            var result = await _mediator.Send(new GetStatisticQuery());
            return Ok(result);
        }
    }
}
