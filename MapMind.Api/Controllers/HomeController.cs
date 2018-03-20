using Microsoft.AspNetCore.Mvc;

namespace MapMind.Api.Controllers
{
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index() => this.RedirectPermanent("/swagger");
    }
}