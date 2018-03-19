namespace Sokudo.Api.Controllers
{
    using Sokudo.Api.Constants;
    using Microsoft.AspNetCore.Mvc;

    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Index() => this.RedirectPermanent("/swagger");
    }
}
