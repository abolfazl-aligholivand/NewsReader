using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsReader.Domain.DTO.WebsiteDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Features.Websites.Commands;
using NewsReader.Features.Websites.Queries;

namespace NewsReader.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class WebsiteController : Controller
    {
        private readonly IMediator _mediator;
        public WebsiteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<WebsiteDTO>))]
        public async Task<IActionResult> Get([FromQuery] GetWebsiteQuery command)
        {
            var result = await _mediator.Send(command);
            return new Response<WebsiteDTO>().ResponseSending(result);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<WebsiteDTO>>))]
        public async Task<IActionResult> GetAll([FromQuery] GetAllWebsitesQuery command)
        {
            var result = await _mediator.Send(command);
            return new Response<IEnumerable<WebsiteDTO>>().ResponseSending(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<WebsiteDTO>))]
        public async Task<IActionResult> Create([FromBody] CreateWebsiteCommand command)
        {
            var result = await _mediator.Send(command);
            return new Response<WebsiteDTO>().ResponseSending(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<WebsiteDTO>))]
        public async Task<IActionResult> Update([FromBody] UpdateWebsiteCommand command)
        {
            var result = await _mediator.Send(command);
            return new Response<WebsiteDTO>().ResponseSending(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> Delete([FromBody] DeleteWebsiteCommand command)
        {
            var result = await _mediator.Send(command);
            return new Response<bool>().ResponseSending(result);
        }
    }
}
