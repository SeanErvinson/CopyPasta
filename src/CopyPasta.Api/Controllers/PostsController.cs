using System.Threading.Tasks;
using CopyPasta.Api.Posts.Commands;
using CopyPasta.Api.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CopyPasta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("{link}/info")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostInformationDto))]
        public async Task<IActionResult> GetPostInformation([FromRoute] GetPostInformationQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("{link}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostDto))]
        public async Task<IActionResult> GetPost([FromRoute] string link, [FromBody] GetPostDto requestDto)
        {
            var result = await _mediator.Send(new GetPostQuery(link, requestDto.Password));
            return Ok(result);
        }

        [HttpGet("{link}/exists")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> CheckIfLinkExists([FromRoute] CheckIfLinkExists request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        public record GetPostDto(string? Password);
    }
}
