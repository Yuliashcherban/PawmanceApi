using Microsoft.AspNetCore.Mvc;

namespace Pawmence.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Login()
        {
            return Ok("Test login");
        }
    }
}
