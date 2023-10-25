using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsReader.Domain.DTO.WebsiteCategoryDTO;
using NewsReader.Domain.DTO.WebsiteDTO;
using NewsReader.Domain.Helpers;
using NewsReader.Features.WebsiteCategories.Commands;
using NewsReader.Features.WebsiteCategories.Queries;
using NewsReader.Features.Websites.Commands;

namespace NewsReader.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class WebsiteCategoryController : Controller
    {
        private readonly IMediator _mediator;
        public WebsiteCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<WebsiteCategoryDTO>))]
        public async Task<IActionResult> Get([FromQuery] GetWebsiteCategoryQuery command)
        {
            var result = await _mediator.Send(command);
            return new Response<WebsiteCategoryDTO>().ResponseSending(result);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<WebsiteCategoryDTO>>))]
        public async Task<IActionResult> GetAll([FromQuery] GetAllWebsiteCategoriesQuery command)
        {
            var result = await _mediator.Send(command);
            return new Response<IEnumerable<WebsiteCategoryDTO>>().ResponseSending(result);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<WebsiteCategoryDTO>))]
        public async Task<IActionResult> Create([FromBody] CreateWebsiteCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return new Response<WebsiteCategoryDTO>().ResponseSending(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<WebsiteCategoryDTO>))]
        public async Task<IActionResult> Update([FromBody] UpdateWebsiteCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return new Response<WebsiteCategoryDTO>().ResponseSending(result);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> Delete([FromBody] DeleteWebsiteCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return new Response<bool>().ResponseSending(result);
        }
    }
}
