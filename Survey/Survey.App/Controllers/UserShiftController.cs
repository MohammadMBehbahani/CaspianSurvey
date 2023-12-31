using Microsoft.AspNetCore.Mvc;

namespace Survey.App.Controllers
{
    [ApiController]
    [Route("Controller")]
    public class UserShiftController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {//s
            return Ok("s");
        }
    }
}
