using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("")]
    public class HomeController : BaseApiController
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Identity!");
        }
    }
}