using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsReader.Domain.DTO.NewsDTO;
using NewsReader.Domain.DTO.WebsiteCategoryDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Features.Newses.Commands;
using NewsReader.Features.Newses.Queries;
using NewsReader.Features.ReadNews.Commands;
using NewsReader.Features.WebsiteCategories.Queries;
using ReadNews.Features.NewsReader.Commands;

namespace NewsReader.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class NewsController : Controller
    {
        private readonly IMediator _mediator;
        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<NewsDTO>))]
        public async Task<IActionResult> Get([FromQuery] GetNewsQuery command)
        {
            var result = await _mediator.Send(command);
            return new Response<NewsDTO>().ResponseSending(result);
        }

        [HttpPost("ReadNews")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<NewsDTO>>))]
        public async Task<IActionResult> ReadNews([FromBody] ReadNewsFromWebsiteCommand command)
        {
            var result = await _mediator.Send(command);
            return new Response<IEnumerable<NewsDTO>>().ResponseSending(result);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<NewsDTO>>))]
        public async Task<IActionResult> GetAll([FromQuery] GetAllNewsesQuery command)
        {
            var result = await _mediator.Send(command);
            return new Response<IEnumerable<NewsDTO>>().ResponseSending(result);
        }

        [HttpGet("Translate")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> Translate([FromQuery] TranslateNewsCommand command)
        {
            var result = await _mediator.Send(command);
            return new Response<string>().ResponseSending(result);
        }

        [HttpPost("SendViaBale")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> Send([FromBody] SendNewsViaBaleMessengerCommand command)
        {
            var result = await _mediator.Send(command);
            return new Response<bool>().ResponseSending(result);
        }
    }
}
