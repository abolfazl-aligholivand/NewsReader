using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsReader.Domain.Helpers;

namespace NewsReader.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<GetAllStatesQuery>>))]
        public ActionResult Index()
        {
            return View();
        }
    }
}
